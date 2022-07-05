using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class PasteManager : MonoBehaviour
{
    public GameObject shovel;
    public GameObject shellCollider;



    public void PrepareShovel()
    {
        //Debug.Log("Cloth Prepared");
        //cloth.GetComponent<DcGrabInteractable>().enabled = true;
        //cloth.GetComponent<Outline>().enabled = true;
    }

    public void PreparePaste()
    {
        //cloth.GetComponent<Outline>().enabled = false;
        //shellCollider.SetActive(true);
    }

    public void DisableShovel()
    {
        //stop outlining the shovel
        shovel.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(2).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(5).gameObject.GetComponent<Outline>().enabled = false;
    }
}
