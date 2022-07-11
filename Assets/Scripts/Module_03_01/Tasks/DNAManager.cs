using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class DNAManager : MonoBehaviour
{
    public GameObject syringe;
    public GameObject turtleCollider;
    public GameObject tubeCollider;
    public GameObject tubeOutline;
    public GameObject[] syringeOutlines;



    public void PrepareSyringe()
    {
        syringe.GetComponent<DcGrabInteractable>().enabled = true;
        DisableSyringeOutline(true);
    }

    public void PrepareDNA()
    {
        DisableSyringeOutline(false);
        turtleCollider.SetActive(true);
    }

    public void PrepareTube()
    {
        tubeCollider.SetActive(true);
        tubeOutline.GetComponent<Outline>().enabled = true;
    }

    // Disable all colliders and outlines for the DNA activity
    public void DisableDNA()
    {
        syringe.GetComponent<DcGrabInteractable>().enabled = false;
        DisableSyringeOutline(false);
        tubeCollider.SetActive(false);
        turtleCollider.SetActive(false);
        tubeOutline.GetComponent<Outline>().enabled = false;
    }

    private void DisableSyringeOutline(bool disabled)
    {
        foreach (GameObject outline in syringeOutlines)
        {
            outline.GetComponent<Outline>().enabled = disabled;
        }
    }
}
