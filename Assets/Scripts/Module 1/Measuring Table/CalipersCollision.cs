using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script By: Cameron Detig 02/24/2020
//Attached to colliders on calipers to detect collisions with turtle colliders.

public class CalipersCollision : MonoBehaviour
{
    [HideInInspector]
    public bool frontCollided;
    [HideInInspector]
    public bool backCollided;

    private void Start()
    {
        frontCollided = false;
        backCollided = false;
    }

    //tracks when the calipers have successfully measured a Thing
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Turtle Front Left Collider" || col.gameObject.name == "Turtle Front Right Collider")
        {
            frontCollided = true;
        }
        else if (col.gameObject.name == "Turtle Back Left Collider" || col.gameObject.name == "Turtle Back Right Collider")
        {
            backCollided = true;
        }
        //print("General Coll: " + col.gameObject.name + " turtle collision: " + collided);
    }

    void OnTriggerExit(Collider col)
    {
        frontCollided = false;
        backCollided = false;
    }
    
}
