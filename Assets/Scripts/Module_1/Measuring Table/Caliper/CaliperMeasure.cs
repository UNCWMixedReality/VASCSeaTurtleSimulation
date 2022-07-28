using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;



public class CaliperMeasure : MonoBehaviour
{
    [HideInInspector]
    public bool collided;

    private void Start()
    {
        collided = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "CalipColliderL" || col.name == "CalipColliderR")
        {
            collided = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        collided = false;
    }
}

