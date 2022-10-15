using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log(col.name);
        }

    }

    void OnTriggerExit(Collider col)
    {
        collided = false;
    }
}

