using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewToolManagerM1 : MonoBehaviour
{
    public GameObject calipers;
    public GameObject tapeMeasure;
    public GameObject container;
    public GameObject clipboard;
    public GameObject[] arrows;
   
    private bool caliperStart = false;
   
   // Old Progression markers
   /* public SpriteRenderer Container;
    * public SpriteRenderer Clipboard;
    * private bool toolTaskDone = false;
    * private bool containerDone = false;
    * private bool clipboardDone = false;
    * private Color arrowGreen = new Color(0, 1, 0, 1);
   */
      
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
    }
    public void FinishToolTable()
    {
        arrows[1].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
    }
}
