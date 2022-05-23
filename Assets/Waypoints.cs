using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFind;
using System.Linq;
using Grid = PathFind.Grid;
using System;
using Object = UnityEngine.Object;
using System.Threading;
using System.Threading.Tasks;

public class Waypoints : CancellableMonobehaviour
{
    Terrain terrain;
    int minX = 0;
    int minZ = 0;
    int scaleFactor = 10;
    Grid grid;
    bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        terrain = Object.FindObjectOfType<Terrain>();
        if (terrain == null)
            return;

        BoxCollider collider = GetComponent<BoxCollider>();
        var worldBounds = collider.bounds;
        //Debug.Log(terrain.terrainData.size);
        
        Vector3 terrainSize = terrain.terrainData.size;
        Vector3 worldMin = terrain.transform.TransformPoint(terrain.terrainData.bounds.min);
        Vector3 worldMax = worldMin + terrainSize;
        worldMax = new Vector3(Mathf.Min(worldMax.x, worldBounds.max.x), 0, Mathf.Min(worldMax.z, worldBounds.max.z));
        worldMin = new Vector3(Mathf.Max(worldMin.x, worldBounds.min.x), 0, Mathf.Max(worldMin.z, worldBounds.min.z));

        //Debug.DrawLine(worldMin, worldMax, Color.red, 10);

        int maxX = Mathf.CeilToInt(worldMax.x);
        int maxZ = Mathf.CeilToInt(worldMax.z);

        minX = Mathf.FloorToInt(worldMin.x);
        minZ = Mathf.FloorToInt(worldMin.z);

        int width = (maxX - minX)/scaleFactor;
        int depth = (maxZ - minZ)/scaleFactor;

        //Debug.Log(width + ", " + depth);

        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + new Vector3(15, 0, 35);

        //Debug.Log(posToPoint(startPos));
        //Debug.Log(posToPoint(endPos));
        var map = new bool[width, depth];
        for (int x = 0; x < width; x ++)
        {
            for (int z = 0; z < depth; z ++)
            {
                map[x, z] = true;
            }
        }
        grid = new PathFind.Grid(width, depth, map);
    }

    List<Waypoint> waypoints = new List<Waypoint>();

    public void SetWaypoints(Vector3 startPos, Vector3 endPos)
    {
        ClearWaypoints();
        isComplete = false;

        var path = Pathfinding.FindPath(grid, posToPoint(startPos), posToPoint(endPos));

        for (int i = 1; i < path.Count; i++)
        {
            var waypointGO = Instantiate(Resources.Load<GameObject>("Prefabs/Waypoint"), pointToPos(path[i]), Quaternion.identity);
            waypointGO.transform.SetParent(transform);
            var waypoint = waypointGO.GetComponent<Waypoint>();
            waypoint.OnTriggered += OnWaypointTriggered;
            if (i == path.Count - 1)
                waypoint.OnTriggered += x => OnComplete();
            waypoints.Add(waypoint);
            waypoint.SetVisibility(true);
            Debug.DrawLine(pointToPos(path[i - 1]), pointToPos(path[i]), Color.blue, 10);
        }
    }

    public void OnComplete()
    {
        isComplete = true;
    }

    public async System.Threading.Tasks.Task RunAsync(CancellationToken token)
    {
        while (!isComplete)
        {
            await System.Threading.Tasks.Task.Delay(100, LinkToken(token));
        }
    }

    public void OnWaypointTriggered(Waypoint waypoint)
    {
        for (int i = 0; i < waypoints.IndexOf(waypoint); i++)
        {
            waypoints[i].SetVisibility(false);
        }
    }

    public void ClearWaypoints()
    {
        foreach (var waypoint in waypoints)
        {
            Destroy(waypoint.gameObject);
        }
        waypoints.Clear();
    }


    Point posToPoint (Vector3 pos)
    {
        pos -= new Vector3(minX, 0, minZ);
        pos = pos / scaleFactor;
        return new Point((int)pos.x, (int)pos.z);
    }

    Vector3 pointToPos(Point point)
    {
        Vector3 pos = new Vector3(point.x, 0, point.y);
        pos *= scaleFactor;
        pos += new Vector3(minX, 0, minZ);
        return new Vector3(pos.x, GetHeight(pos.x, pos.z), pos.z);
    }

    float GetHeight(float x, float z)
    {
        return terrain.transform.TransformPoint(new Vector3(0,terrain.SampleHeight(new Vector3(x, 0, z)), 0)).y;
    }

    public void Create(Vector3 startPos, Vector3 endPos)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
