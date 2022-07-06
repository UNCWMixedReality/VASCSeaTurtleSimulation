using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class PasteManager : MonoBehaviour
{
    public GameObject shovel;
    public GameObject pasteCollider;



    public void PrepareShovel()
    {
        //now we need to enable the shovel
        shovel.GetComponent<DcGrabInteractable>().enabled = true;

        //outline the shovel
        shovel.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = true;
        shovel.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = true;
        shovel.transform.GetChild(2).gameObject.GetComponent<Outline>().enabled = true;
        shovel.transform.GetChild(5).gameObject.GetComponent<Outline>().enabled = true;

    }

    public void PreparePaste()
    {
        DisableShovelHighlight();
        pasteCollider.SetActive(true);

    }

    public void DisableShovelHighlight()
    {
        //stop outlining the shovel
        shovel.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(2).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(5).gameObject.GetComponent<Outline>().enabled = false;
    }
}
