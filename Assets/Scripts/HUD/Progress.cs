using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    //this updates the progress bar on the handheld instruction panel
    //these are the images that will be activated as tasks are finished
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public GameObject P5;
    public GameObject P6;
    public GameObject P7;
    public GameObject P8;
    public GameObject P9;
    public GameObject P10;
    public GameObject P11;

    //tracks the current task
    private int current = 0;
    public GameObject[] pArray { get; set; }
    /*
    void Start()
    {
        //fill the array with the images
        pArray[0] = P1;
        pArray[1] = P2;
        pArray[2] = P3;
        pArray[3] = P4;
        pArray[4] = P5;
        pArray[5] = P6;
        pArray[6] = P7;
        pArray[7] = P8;
        pArray[8] = P9;
        pArray[9] = P10;
        pArray[10] = P11;
    }
    */
    public void TickProgressBar()
    {
        //activate the next sphere on the progress indicator, is independent of order events are completed in
        pArray[current].SetActive(true);
        current++;
    }
}
