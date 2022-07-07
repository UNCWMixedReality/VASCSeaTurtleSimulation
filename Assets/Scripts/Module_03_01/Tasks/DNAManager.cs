using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class GPSManager : MonoBehaviour
{
    public GameObject syringe;
    public GameObject syringeCollider;
    public GameObject tubeCollider;



    public void PrepareDNA()
    {
        Debug.Log("DNA Prepared");
        syringe.GetComponent<DcGrabInteractable>().enabled = true;
        syringe.GetComponent<Outline>().enabled = true;
    }

    public void PrepareDNAShellCollider()
    {
        syringe.GetComponent<Outline>().enabled = false;
        syringeCollider.SetActive(true);
        syringeCollider.GetComponent<Outline>().enabled = true;
    }

    public void PrepareDNATubeCollider()
    {
        syringe.GetComponent<Outline>().enabled = false;
        tubeCollider.SetActive(true);
        tubeCollider.GetComponent<Outline>().enabled = true;
    }

    public void DisableDNA()
    {
        GPS.GetComponent<Outline>().enabled = false;
        GPS.GetComponent<DcGrabInteractable>().enabled = false;

    }
}
