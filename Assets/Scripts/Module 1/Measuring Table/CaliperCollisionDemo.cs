using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperCollisionDemo : MonoBehaviour
{
    [HideInInspector]
    public bool Collided;

    private void Start()
    {
        Collided = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "LeftCollider" || col.gameObject.name == "RightCollider")
        {
            Collided = true;
        }
        //print("General Coll: " + col.gameObject.name + " turtle collision: " + collided);
    }

    void OnTriggerExit(Collider col)
    {
        Collided = false;
    }

}
