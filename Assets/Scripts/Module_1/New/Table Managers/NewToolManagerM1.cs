using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using DataCollection;

public class NewToolManagerM1 : MonoBehaviour
{
    public GameObject calipers;
    public GameObject tapeMeasure;
    public GameObject container;
    public GameObject clipboard;
    public GameObject[] arrows;
    public GameObject calipButton;
    public GameObject clipButtonText;
         
    public void PrepareCaliper()
    {
        calipers.SetActive(true);
    }

    public void PrepareJar() // Prepares Jar for measuring
    {
        container.SetActive(true);
        arrows[0].SetActive(true);
    }
   
    public void PrepareTM() // First Tape Measure PickUp
    {
        arrows[0].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        tapeMeasure.SetActive(true);
    }

    public void PrepareClipboard() // Prepares Clipboard for measuring
    {
        clipboard.SetActive(true);
        arrows[0].SetActive(false);
        arrows[1].SetActive(true);
        tapeMeasure.GetComponent<NewTapeMeasure>().measureLength = "70 cm";
        calipers.GetComponent<DcGrabInteractable>().enabled = false;
    }
    public void FinishToolTable()
    {
        arrows[1].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
    }



}
