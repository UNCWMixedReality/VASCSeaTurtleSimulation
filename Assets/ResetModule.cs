using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataCollection;
using DataCollection.Models;


public class ResetModule : MonoBehaviour
{
    /* This script allows us to escape a module if there are any issues.
       Could probably be made better at a later date                    */
    
    private bool start_press; // Initialization press for reset
    private DateTime start;  // Start time for reset 
    
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.Start)) // If the oculus button on left controller is held down
        {
            if (!start_press) 
            {
                start_press = true;   // Set start press to true
                start = DateTime.Now; // Grab the time the button is first pressed
            }
            if ((DateTime.Now - start).Seconds == 3)  // Measure time difference between frames, execute code after 3s
            {
                DcDataLogging.LogActivity(new Activity(
                    DateTime.Now,
                    "Module Escape -- Back to Level Select"
                    ));
                
                DcDataLogging.EndSession();            // End the data logging session
                SceneManager.LoadScene("JustModule"); // Load back to the Module Select Screen
            }
        }
        else
        {
            start_press = false; // Set start_press to false (resets process)
        }
    }
}        


