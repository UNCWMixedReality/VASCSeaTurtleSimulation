using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VASC_Waypoint : MonoBehaviour
{

    public Transform position;
    public Action<Collision> onEnterCallback = null;
    public Action<Collision> onExitCallback = null;

    public void Start()
    {
        position = gameObject.transform;
    }

    public void setEnterCallback(Action<Collision> callback)
    {
        onEnterCallback = callback;
    }

    public void setExitCallback(Action<Collision> callback)
    {
        onExitCallback = callback;
    }

    public void OnCollisionEnter(Collision collision)
    {
        onEnterCallback?.Invoke(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        onExitCallback?.Invoke(collision);
    }

}
