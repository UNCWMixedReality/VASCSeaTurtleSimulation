using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using DataCollection;
using UltimateXR.Manipulation;

public class NewToolManagerM1 : MonoBehaviour
{
    public GameObject calipers;
    public GameObject tapeMeasure;
    public GameObject container;
    public GameObject clipboard;
    public GameObject[] arrows;
    public GameObject tmIndicators;
    public CompassManager compMan;

         
    public void PrepareCaliper()
    {
        compMan.EnableCompass(calipers);
        calipers.SetActive(true);
        //calipers.GetComponent<NewCaliper>().ShowButton();
    }

    public void PrepareJar() // Prepares Jar for measuring
    {
        compMan.EnableCompass(container);
        container.SetActive(true);
        arrows[0].SetActive(true);
    }
   
    public void PrepareTM() // First Tape Measure PickUp
    {
        compMan.EnableCompass(tapeMeasure);
        arrows[0].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        tapeMeasure.SetActive(true);
    }

    public void PrepareClipboard() // Prepares Clipboard for measuring
    {
        compMan.DisableCompass();
        tmIndicators.SetActive(true);
        clipboard.SetActive(true);
        arrows[0].SetActive(false);
        arrows[1].SetActive(true);
        calipers.GetComponent<UxrGrabbableObject>().IsGrabbable = false;
    }
    public void FinishToolTable()
    {
        arrows[1].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
    }



}
