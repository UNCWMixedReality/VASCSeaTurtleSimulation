using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSlide : MonoBehaviour
{
    public float totalPages;
    private float pageNumber;
    public GameObject page1;
    public GameObject page2;
    private bool page1Enabled;

    private void Start()
    {
        pageNumber = 1f;
        page1Enabled = true;
    }
    void Update()
    {
        
    }

    public void ScreenSlider()
    {
      
            page2.SetActive(true);
            page1.SetActive(false);
            page1Enabled = false;
       

    }
}
