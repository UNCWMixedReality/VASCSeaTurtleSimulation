using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using DataCollection.Models;
using UnityEngine.SceneManagement;
using UltimateXR.Avatar;


public class TaskManagerTutorial : MonoBehaviour
{
    /*
     * There are x tasks in the tutorial 
     */

    //class variables
    #region
    //reference to activity manager script and instruction updater
    public ActivityManagerTutorial activityMan;
    public InstructionUpdaterTutorial instrUpdater;
    public UxrAvatar avatar;
    public ControlManager contMan;

    public GameObject[] buttonHighlights;
    public GameObject sphere;



    //number of tasks completed and when each task was completee
    public int taskCount { get; set; }
    public float[] taskTimes { get; set; }

    //game objects used in tasks

    #endregion

    IEnumerator ButtonCountdown()
    {
        yield return new WaitForSeconds(8);
        buttonHighlights[0].SetActive(true);
        buttonHighlights[1].SetActive(true);
    }

    IEnumerator SphereGrow()
    {
        sphere.GetComponent<Animator>().SetTrigger("trigger_start");
        yield return new WaitForSeconds(5);
        sphere.SetActive(false);
    }


    public void MarkTaskCompletion(int taskID)
    {

        /*
         * This is the primary function for this class, every time a tasks gets completed this function is called
         * 
         * We log the time the task was completed, increment the number of tasks done, and determine what setup, if any, needs to be performed for the next task.
         *     and we run the instructions for the next task.
         */


        //this will check if a task is completed that should not be completed yet or has already been completed
        if (taskID != taskCount)
        {
            return;
        }


        //mark completion time, update task count
        taskTimes[taskCount] = Time.time;
        taskCount += 1;
        Debug.Log("The task count is, " + (taskCount));

        //perform approrpriate setup based on which task was completed
        #region
        //true when the user completes the first task by entering the scene
        if (taskCount == 1)
        {
            PrepareScene();
            StartCoroutine(ButtonCountdown());
        }

        //true when the user completes the second task by entering the relocation waypoint
        else if (taskCount == 2)
        {
            EnableNextHighlights(0);
            StopAllCoroutines();
        }

        else if (taskCount == 3)
        {
            EnableNextHighlights(2);
        }

        else if (taskCount == 4)
        {
            EnableNextHighlights(4);
        }

        else if (taskCount == 5)
        {
            EnableNextHighlights(6);
        }

        else if (taskCount == 6)
        {
            buttonHighlights[8].SetActive(false);
            buttonHighlights[9].SetActive(false);
            StartCoroutine(SphereGrow());
        }

        //true when the user completes the second task by entering the relocation waypoint
        else if (taskCount == 7)
        {
            DisableControllers();
        }

        #endregion

        //Run the next set of instructions
        //instrUpdater.RunInstructions();
    }

    private void PrepareScene()
    {
    }

    //private void LogTask(string message)
    //{
    //    /*
    //    * logs when a task is completed with the appropriate information
    //    */
    //    DcDataLogging.LogActivity(new Activity(
    //            DateTime.Now,
    //            SceneManager.GetActiveScene().name,
    //            message
    //            ));
    //}

    private void PrepareEnd()
    {
    }

    private void DisableControllers()
    {
        avatar.RenderMode = UxrAvatarRenderModes.Avatar;
    }



    private void EnableNextHighlights(int index)
    {
        for (int i = 0; i < 2; i++)
        {
            buttonHighlights[index+i].SetActive(false);
            buttonHighlights[index+2+i].SetActive(true);
        }
    }

}
