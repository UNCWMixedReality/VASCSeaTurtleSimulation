using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using UltimateXR.Manipulation;
using cakeslice;

public class RelocationManager : MonoBehaviour
{
    public GameObject[] goodEggs;
    public GameObject relocationWaypoint;


    public void PrepareRelocation()
    {
        //removes the waypoint
        relocationWaypoint.SetActive(false);

        //outlines eggs so they can be seen and enables the grab functionality
        for (int x = 0; x < goodEggs.Length; x++)
        {
            goodEggs[x].SetActive(true);
            goodEggs[x].GetComponent<UxrGrabbableObject>().IsGrabbable = true;
        }
    }

    public void EndRelocation()
    {
        //we no longer need to outline/grab the eggs, so turn those components off
        for (int x = 0; x < goodEggs.Length; x++)
        {
            goodEggs[x].GetComponent<UxrGrabbableObject>().IsGrabbable = false;
        }
    }
    
}
