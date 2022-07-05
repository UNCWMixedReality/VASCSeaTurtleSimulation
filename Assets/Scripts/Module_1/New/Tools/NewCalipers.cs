using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NewCalipers : MonoBehaviour
{
    public XRRayInteractor leftHand;
    public XRRayInteractor rightHand;
    private string activeHand;

    public void PickedUp()
    {
        if leftHand.selectTarget.tag == "caliper";
    }
}
