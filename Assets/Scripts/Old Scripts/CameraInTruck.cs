using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInTruck : MonoBehaviour
{
    public GameObject truck;
    public GameObject player;
    public float heightOffset = 1;

    // Update is called once per frame
    void Update()
    {
        player.transform.position = new Vector3(truck.transform.position.x, truck.transform.position.y - heightOffset, truck.transform.position.z);

    }
}
