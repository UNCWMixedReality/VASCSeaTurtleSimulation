using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using DataCollection;
using DataCollection.Models;
using UnityEngine.SceneManagement;
using VASCDC;

public class TaskManagerM3 : MonoBehaviour
{
    /*
     * There are 9 tasks in module 2 part 2, this script is used to mark tasks as complete and to setup the scene for the next task
     * tasks: enter scene, teleport to nest, place eggs, grab shovel, cover nest with sand, grab cage, cover nest with cage, grab sign, place sign
     * 
     * So for instance, when the player puts on gloves, the function MarkTaskCompleted() is called which increments the task count and calls PrepareDigging()
     * so the player can start the next task of digging up the egg nest.
     * 
     */

    //class variables
    #region
    //reference to activity manager script and instruction updater
    public NewActivityManM3 activityManager;
    public InstructionUpdaterM3 instrUpdater;

    //number of tasks completed and when each task was completee
    public int taskCount { get; set; }
    public float[] taskTimes { get; set; }

    //game objects used in tasks
    public RelocationManager relocMan;
    public DigManager digMan;
    public CageManager cageMan;
    public SignManager signMan;
    public CompassManager compMan;

    public GameObject relocationWaypoint;
    public GameObject nestEnclosure;
    #endregion

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
            LogAct("Entered " + SceneManager.GetActiveScene().name);
        }

        //true when the user completes the second task by entering the relocation waypoint
        else if (taskCount == 2)
        {
            LogAct("Entered relocation waypoint");
            relocMan.PrepareRelocation();            
        }

        //true when the user completes the third task by successfully placing the eggs
        else if (taskCount == 3)
        {
            LogAct("Successfully placed eggs in the new nest");
            relocMan.EndRelocation();
            digMan.PrepareShovel();
            compMan.EnableCompass(digMan.shovel);
            activityManager.MarkActivityCompletion();
        }

        //true when the user completes the fourth task by grabbing the shovel
        else if (taskCount == 4)
        {
            LogAct("Grabbed shovel");
            digMan.DisableShovelHighlight();
            compMan.DisableCompass();
            digMan.PrepareDigging();
        }

        //true when the user completes the fifth task by covering the nest with sand
        else if (taskCount == 5)
        {
            LogAct("Covered nest with sand");
            cageMan.PrepareCage();
            compMan.EnableCompass(cageMan.cage);
        }

        //true when the user completes the sixth task by grabbing the cage
        else if (taskCount == 6)
        {
            LogAct("Cage grabbed");
            cageMan.PrepareCovering();
            compMan.DisableCompass();
        }

        //true when the user completes the seventh task by placing the cage
        else if(taskCount == 7)
        {
            LogAct("Cage placed");
            cageMan.EndCovering();
            signMan.PrepareSign();
            compMan.EnableCompass(signMan.sign);
        }

        //true when the user completes the eighth task by grabbing the sign
        else if (taskCount == 8)
        {
            LogAct("Sign grabbed");
            compMan.DisableCompass();
            signMan.PreparePlacement();
        }

        //true when user completes the ninth task by placing the sign
        else if (taskCount == 9)
        {
            LogAct("Sign placed");
            signMan.EndPlacement();
            PrepareEnd();
            activityManager.MarkActivityCompletion();
        }
        #endregion

        //Run the next set of instructions
        instrUpdater.RunInstructions();
    }

    private void PrepareScene()
    {
        relocationWaypoint.SetActive(true);

        //stop outlining the shovel, cage, sign
        digMan.DisableShovelHighlight();
        cageMan.DisableCage();
        signMan.DisableSign();
    }

    private void LogAct(string message)
    {
        /*
         * logs when a task is completed with the appropriate information
         */
        VASCEventLog.logActivityEvent(message);
    }

    private void PrepareEnd()
    {
        nestEnclosure.SetActive(true);
    }
}