using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script By: Cameron Detig 06/30/2020
//Attached to colliders on seaturtle shell to detect collisions with hand colliders

public class MeasureColliders : MonoBehaviour
{
    [HideInInspector]
    public bool collided;

    private void Start()
    {
        collided = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "LeftHand" || col.gameObject.name == "RightHand")
        {
            collided = true;
            //print("Hand col");
        }

        if (col.CompareTag("TapeMeasure") || col.gameObject.name == "Tape End Collider")
        {
            collided = true;
            print("Measure col");
        }
    }

    void OnTriggerExit(Collider col)
    {
        collided = false;
    }
}
