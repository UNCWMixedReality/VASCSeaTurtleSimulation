using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class New_Activity_Manager : MonoBehaviour
{

    //number of activities completed
    public int activityCount;
    //stores the time when activities are completed
    public float[] activityTimes;

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
