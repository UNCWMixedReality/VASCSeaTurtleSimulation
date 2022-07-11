using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;


public class CleanManager : MonoBehaviour
{
    public GameObject cloth;
    public GameObject shellCollider;



    public void PrepareCloth()
    {
        Debug.Log("Cloth Prepared");
        cloth.GetComponent<DcGrabInteractable>().enabled = true;
        cloth.GetComponent<Outline>().enabled = true;
    }

    public void PrepareClean()
    {
        cloth.GetComponent<Outline>().enabled = false;
    }

    public void DisableCloth()
    {
        cloth.GetComponent<Outline>().enabled = false;
        cloth.GetComponent<DcGrabInteractable>().enabled = false;
    }
}
