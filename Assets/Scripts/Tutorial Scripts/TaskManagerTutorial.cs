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

    //game objects used in tasks
    public GameObject[] buttonHighlights;
    public GameObject funThings;
    public GameObject button;
    public GameObject sphere;
    public GameObject teleport;
    public GameObject homePos;

    public AudioFeedback audiofeedback;

    //number of tasks completed and when each task was completee
    public int taskCount { get; set; }
    public float[] taskTimes { get; set; }

    #endregion

    IEnumerator ButtonCountdown()
    {
        yield return new WaitForSeconds(5);
        buttonHighlights[0].SetActive(true);
        buttonHighlights[1].SetActive(true);
    }

    IEnumerator SphereGrow()
    {
        sphere.GetComponent<Animator>().SetTrigger("down_start");
        yield return new WaitForSeconds(2);
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

        // press a/x
        else if (taskCount == 2)
        {
            EnableNextHighlights(0);
            StopAllCoroutines();
        }

        // press b/y
        else if (taskCount == 3)
        {
            EnableNextHighlights(2);
        }

        // press grip
        else if (taskCount == 4)
        {
            EnableNextHighlights(4);
            audiofeedback.playGood();
            StartCoroutine(SphereGrow());
        }

        // snap turn
        else if (taskCount == 5)
        {
            EnableNextHighlights(6);
            teleport.SetActive(true);
        }

        // teleport
        else if (taskCount == 6)
        {
            DisableControllers();
            audiofeedback.playGood();
            buttonHighlights[8].SetActive(false);
            buttonHighlights[9].SetActive(false);
            buttonHighlights[4].SetActive(true);
            buttonHighlights[5].SetActive(true);
            activityMan.MarkActivityCompletion();
            homePos.transform.position = teleport.transform.position;
        }

        // grab egg
        else if (taskCount == 7)
        {
            audiofeedback.playGood();
            button.SetActive(true);
            buttonHighlights[4].SetActive(false);
            buttonHighlights[5].SetActive(false);
        }

        // click UI
        else if (taskCount == 8)
        {
            audiofeedback.playGood();
            button.SetActive(false);
            funThings.SetActive(true);
        }

        else if (taskCount == 9)
        {
            audiofeedback.playCompletion();
            activityMan.MarkActivityCompletion();
        }

        #endregion

        //Run the next set of instructions
        instrUpdater.RunInstructions();
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
