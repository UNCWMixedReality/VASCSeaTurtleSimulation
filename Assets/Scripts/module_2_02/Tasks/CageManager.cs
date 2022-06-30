using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class CageManager : MonoBehaviour
{
    public GameObject cage;
    public GameObject nestCageCollider;
        
    public void PrepareCage()
    {
        //outline and enable grabbing
        cage.GetComponent<Outline>().enabled = true;
        cage.GetComponent<DcGrabInteractable>().enabled = true;
    }

    public void DisableCageHighlight()
    {
        //stop outlining the cage
        cage.GetComponent<Outline>().enabled = false;
    }

    public void PrepareCovering()
    {
        nestCageCollider.SetActive(true);

    }

    public void EndCovering()
    {
        //stop grabbing the cage 
        cage.GetComponent<DcGrabInteractable>().enabled = false;
    }
}

