using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressM3 : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public GameObject P5;


    private int current = 0;
    public GameObject[] pArray { get; set; }

    public void TickProgressBar()
    {
        //activate the next sphere on the progress indicator, is independent of order events are completed in
        pArray[current].SetActive(true);
        current++;
    }
}
