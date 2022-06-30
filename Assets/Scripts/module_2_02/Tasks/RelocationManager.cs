using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class RelocationManager : MonoBehaviour
{
    public GameObject goodEgg1;
    public GameObject goodEgg2;
    public GameObject goodEgg3;
    public GameObject goodEgg4;
    public GameObject goodEgg5;
    public GameObject goodEgg6;

    public GameObject relocationWaypoint;


    public void PrepareRelocation()
    {
        //removes the waypoint
        relocationWaypoint.SetActive(false);

        //outlines eggs so they can be seen and enables the grab functionality
        GameObject[] eggList = { goodEgg1, goodEgg2, goodEgg3, goodEgg4, goodEgg5, goodEgg6 };
        for (int x = 0; x < eggList.Length; x++)
        {
            eggList[x].SetActive(true);
            eggList[x].GetComponent<Outline>().enabled = true;
            eggList[x].GetComponent<DcGrabInteractable>().enabled = true;
        }
    }

    public void EndRelocation()
    {
        //we no longer need to outline/grab the eggs, so turn those components off
        GameObject[] eggList = { goodEgg1, goodEgg2, goodEgg3, goodEgg4, goodEgg5, goodEgg6 };
        for (int x = 0; x < eggList.Length; x++)
        {
            eggList[x].GetComponent<Outline>().enabled = false;
            eggList[x].GetComponent<DcGrabInteractable>().enabled = false;
        }
    }
    
}
