using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timerRemaining;
    public TextMeshProUGUI timer;
    public Color32 timerRed = new Color32(255, 0, 0, 255);
    public Color32 timerNormal = new Color32(255, 255, 255, 255);
    
    private bool timerRunning = false;
    private String secondsText;
    private String minuteText;
    private String timerText;

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

                minuteText = Convert.ToInt32(Math.Floor(timerRemaining / 60)).ToString("D2");
                secondsText = Convert.ToInt32(Math.Floor(timerRemaining % 60)).ToString("D2");

                timer.text = minuteText + " : " + secondsText;

                if (timerRemaining < 10)
                {
                    switch (Math.Round(timerRemaining) % 2)
                    {
                        case 0: timer.color = timerRed; break;
                            case 1: timer.color = timerNormal; break;
                    }
                }
                timerRemaining -= Time.deltaTime;

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
            SceneManager.LoadScene("DemoMain");
        }

    }
}
