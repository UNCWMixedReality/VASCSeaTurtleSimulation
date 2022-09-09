using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public float timerRemaining;
    public TextMeshProUGUI timer;
    public bool timerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (timerRemaining > 0)
            {
                timerRemaining -= Time.deltaTime;
                timer.text = Math.Round(timerRemaining).ToString();
                if (timerRemaining < 10)
                {
                    
                }
            }
            else
            {
                timerRunning = false;
                timerRemaining = 0;
                Debug.Log("Time Done");
            }
        }
        else
        {
            timer.text = "Timer done";
        }

    }
}
