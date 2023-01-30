using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using UltimateXR.Manipulation;
using cakeslice;

public class SignManager : MonoBehaviour
{
    public GameObject sign;
    public GameObject signOutline;
    public GameObject signAnchor;
       

    public void PrepareSign()
    {
        //outline and enable grabbing the sign
        signOutline.SetActive(true);
        sign.GetComponent<UxrGrabbableObject>().IsGrabbable = true;

    }

    public void DisableSign()
    {
        //stop outlining the cage
        signOutline.SetActive(false);
        sign.GetComponent<UxrGrabbableObject>().IsGrabbable = false;

    }

    public void PreparePlacement()
    {
        //outline and enable grabbing the sign
        signAnchor.SetActive(true);
    }

    public void EndPlacement()
    {
        sign.SetActive(false);
    }
}
