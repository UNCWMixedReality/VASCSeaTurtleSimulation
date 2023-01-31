using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    //this updates the progress bar on the handheld instruction panel
    //these are the images that will be activated as tasks are finished
    public GameObject[] points;

    //tracks the current task
    private int current = 0;
    //public GameObject[] pArray { get; set; }

    public void TickProgressBar()
    {
        //activate the next sphere on the progress indicator, is independent of order events are completed in
        points[current].SetActive(true);
        current++;
    }
}
