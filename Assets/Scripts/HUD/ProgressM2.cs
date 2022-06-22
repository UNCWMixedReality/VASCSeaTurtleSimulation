using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressM2 : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public GameObject P5;
    public GameObject P6;
    public GameObject P7;

    private int current = 0;
    public GameObject[] pArray = new GameObject[7];

    public void TickProgressBar()
    {
        //activate the next sphere on the progress indicator, is independent of order events are completed in
        pArray[current].SetActive(true);
        current++;
    }
}
