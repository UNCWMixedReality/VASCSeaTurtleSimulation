using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    public GameObject pickupL;
    public GameObject pickupR;
    public GameObject controllerL;
    public GameObject controllerR;
    public GameObject handL;
    public GameObject handR;

    //changes the users hands to blue gloves.
    public void wearGloves()
    {
        //turn off the gloves that we picked up
        pickupL.SetActive(false);
        pickupR.SetActive(false);
        //turn off the controllers
        controllerL.SetActive(false);
        controllerR.SetActive(false);
        //turn on the gloves attached to the controllers
        handL.SetActive(true);
        handR.SetActive(true);
    }
}
