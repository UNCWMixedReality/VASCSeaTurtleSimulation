using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using UltimateXR.Manipulation;

public class DigManager : MonoBehaviour
{
    public GameObject shovel;
    public GameObject shovelOutline;

    public GameObject nestSandCollider;

    public void PrepareShovel()
    {
        //now we need to enable the shovel
        shovel.GetComponent<UxrGrabbableObject>().IsGrabbable = true;
        //outline the shovel
        shovelOutline.SetActive(true);
    }

    public void DisableShovelHighlight()
    {
        //stop outlining the shovel
        shovelOutline.SetActive(false);
    }

    public void PrepareDigging()
    {
        nestSandCollider.SetActive(true);
    }

}
