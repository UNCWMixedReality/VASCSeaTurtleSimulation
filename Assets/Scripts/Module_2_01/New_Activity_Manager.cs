using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class New_Activity_Manager : MonoBehaviour
{

    //number of activities completed
    public int activityCount;
    //stores the time when activities are completed
    private float[] activityTimes;

    public void Start()
    {
        activityCount = 0;
        activityTimes = new float[2];
    }
    public void MarkActivityCompletion()
    {
        // save time of activity completion
        activityTimes[activityCount] = Time.time;

        // update activity count
        activityCount += 1;

        // if all activities are completed, then end the module
        if (activityCount == 2)
        {
            EndSimulation();
        }
    }

    private void EndSimulation()
    {
        // just loads the main scene
        SceneManager.LoadScene("Main");
    }
}
