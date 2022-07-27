using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMMeasure : MonoBehaviour
{
    [HideInInspector]
    public bool collided;

    private void Start()
    {
        collided = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "TapeBody" || col.name == "TapeHand")
        {
            collided = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        collided = false;
    }
}
