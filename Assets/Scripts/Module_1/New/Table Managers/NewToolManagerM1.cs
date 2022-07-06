using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewToolManagerM1 : MonoBehaviour
{
   public GameObject calipers;
   public GameObject tm;
   public GameObject container;
   public GameObject clipboard;
   
   private bool caliperStart = false;
   
   // Old Progression markers
   /* public SpriteRenderer Container;
    * public SpriteRenderer Clipboard;
    * private bool toolTaskDone = false;
    * private bool containerDone = false;
    * private bool clipboardDone = false;
    * private Color arrowGreen = new Color(0, 1, 0, 1);
   */
   
   public void FirstCalPickUp() // Need to attach to on select enter for Calipers
   {
      caliperStart = true;
   }
   
   public void PrepareJar() // Prepares Jar for measuring
   {
        container.SetActive(true);
   }
   
   public void PrepareTM() // First Tape Measure PickUp
   {
      tm.SetActive(true);
   }
   public void PrepareClipboard() // Prepares Clipboard for measuring
   {
        clipboard.SetActive(true);
   }
}
