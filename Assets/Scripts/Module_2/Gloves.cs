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

    public ProgressM2 PM2;

    //snap the blue gloves to the players hands
    public void wearGloves()
    {
        pickupL.SetActive(false);
        pickupR.SetActive(false);
        controllerL.SetActive(false);
        controllerR.SetActive(false);

        handL.SetActive(true);
        handR.SetActive(true);

        PM2.TickProgressBar();
    }
}
