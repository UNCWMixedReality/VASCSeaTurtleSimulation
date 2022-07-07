using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewActivityManagerM1 : MonoBehaviour
{
    /*
     * At the moment this just ends the scene when all activities are completed, this could be extended to provide more functionality fairly easily if needed.
     * 
     */


    //number of activities completed
    public int activityCount { get; set; }
    //stores the time when activities are completed
    public float[] activityTimes { get; set; }

    public AudioSource EndAudio;

    public void MarkActivityCompletion()
    {
        // save time of activity completion
        activityTimes[activityCount] = Time.time;

        // update activity count
        activityCount += 1;

        // if all activities are completed, then end the module
        if (activityCount == 4)
        {
            StartCoroutine(EndSimulation());
        }
    }

    private IEnumerator EndSimulation()
    {
        // just loads the main scene
        yield return new WaitForSecondsRealtime(5);
        EndAudio.Play();
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Main");
    }
}
