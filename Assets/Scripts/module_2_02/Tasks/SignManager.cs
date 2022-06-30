using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class SignManager : MonoBehaviour
{
    public GameObject sign;
    public GameObject signCollider;

    public void PrepareSign()
    {
        //outline and enable grabbing the sign
        sign.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = true;
        sign.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = true;

        sign.GetComponent<DcGrabInteractable>().enabled = true;
    }

    public void DisableSignHighlight()
    {
        //stop outling the sign
        sign.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        sign.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = false;
    }

    public void PreparePlacement()
    {
        //activate the placement collider
        signCollider.SetActive(true);
    }

    public void EndPlacement()
    {
        sign.SetActive(false);
    }
}
