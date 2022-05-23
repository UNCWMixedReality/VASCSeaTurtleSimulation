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
    private GameObject[] pArray = new GameObject[7];

    void Start()
    {
        pArray[0] = P1;
        pArray[1] = P2;
        pArray[2] = P3;
        pArray[3] = P4;
        pArray[4] = P5;
        pArray[5] = P6;
        pArray[6] = P7;
    }

    public void TickProgressBar()
    {
        //activate the next sphere on the progress indicator, is independent of order events are completed in
        pArray[current].SetActive(true);
        current++;
    }
}
