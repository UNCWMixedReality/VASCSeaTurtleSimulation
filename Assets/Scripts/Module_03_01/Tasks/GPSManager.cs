using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class GPSManager : MonoBehaviour
{
    public GameObject GPS;
    public GameObject GPSCollider;



    public void PrepareGPS()
    {
        Debug.Log("Cloth Prepared");
        GPS.GetComponent<DcGrabInteractable>().enabled = true;
        GPS.GetComponent<Outline>().enabled = true;
    }

    public void PrepareGPSPlace()
    {
        GPS.GetComponent<Outline>().enabled = false;
        GPSCollider.SetActive(true);
    }

    public void DisableGPS()
    {
        GPS.GetComponent<Outline>().enabled = false;
        GPS.GetComponent<DcGrabInteractable>().enabled = false;

    }
}
