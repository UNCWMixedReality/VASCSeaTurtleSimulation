using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaypointPointers : MonoBehaviour
{
    public GameObject waypointArrow;
    public TaskManagerM3_1 taskMan;
    private Quaternion arrowStartRot;
    private Vector3 arrowPos;
    private Vector3 waypointLocation;


    public void Start()
    {
    }

    public void FixedUpdate()
    {
        waypointLocation = taskMan.activeWaypoint.transform.position;
        arrowPos = waypointArrow.transform.position;

        float anglez = MathF.Atan2(arrowPos.y, waypointLocation.y) * Mathf.Rad2Deg;
        float angley = MathF.Atan2(arrowPos.z, waypointLocation.z) * Mathf.Rad2Deg;

        waypointArrow.transform.rotation = Quaternion.Euler(0, angley, anglez);

    }
}
