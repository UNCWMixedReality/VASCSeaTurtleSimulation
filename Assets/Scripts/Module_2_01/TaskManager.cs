using System;
using System.Collections;
using System.Collections.Generic;
using DataCollection;
using DataCollection.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    /*
     * There are 5 tasks in module 2, this script is used to mark tasks as complete and to setup the scene for the next task
     * tasks: enter scene, teleport to nest, put on gloves, dig up nest, sort eggs
     * 
     * So for instance, when the player puts on gloves, the function MarkTaskCompleted() is called which increments the task count and calls PrepareDigging()
     * so the player can start the next task of digging up the egg nest.
     * 
     */


    //reference to activity manager script and instruction updater
    public New_Activity_Manager activityManager;
    public InstructionUpdater instrUpdater;
    public CompassManager compMan;

    //number of tasks completed
    public int taskCount { get; set; }
    //time each task was completed
    public float[] taskTimes { get; set; }

    //game objects used in tasks
    //public GameObject excavationWaypoint;
    public GameObject gloveL;
    public GameObject gloveR;
    public GameObject eggSorting;


    public void MarkTaskCompletion()
    {

        /*
         * This is the primary function for this class, every time a tasks gets completed this function is called
         * 
         * We log the time the task was completed, increment the number of tasks done, and determine what setup, if any, needs to be performed for the next task.
         *     and we run the instructions for the next task.
         */


        //mark completion time
        taskTimes[taskCount] = Time.time;
        //update number of completed tasks
        taskCount += 1;
        //Debug.Log("The task count is, " + (taskCount));



        //this is true if the player has completed the first task by entering the scene
        if (taskCount == 1)
        {
            //set everything up for the second task.
            PrepareExcavationStart();
            //compMan.EnableCompass(excavationWaypoint);
            instrUpdater.RunInstructions();
            //        }

            //this is true if the player has completed the second task by entering the excavation waypoint
            //        else if (taskCount == 2)
            //        {
            //set everything up for the third task
            taskCount += 1;
            PrepareGloves();
            compMan.EnableCompass(gloveL);
            instrUpdater.RunInstructions();
            //Logs activity for entering waypoint
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Entered excavation waypoint"
                ));
        }

        //this is true if the player has completed the third task by putting on the gloves
        else if (taskCount == 3)
        {
            //sets everything up for the fourth task
            instrUpdater.RunInstructions();
            compMan.EnableCompass(eggSorting);
            //Logs activity for putting on gloves
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Put on Gloves"
            ));
        }
        
        //this is true if the player hsa completed the fourth task by digging up the nest
        else if (taskCount == 4)
        {
            activityManager.MarkActivityCompletion();
            PrepareSorting();
            compMan.DisableCompass();
            instrUpdater.RunInstructions();
            //Logs activity for digging up nest
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Dug up the nest"
            ));
        }
        
        //true if the fifth and final task is completed
        else if (taskCount == 5)
        {
            instrUpdater.RunInstructions();
            activityManager.MarkActivityCompletion();
            //Logs an activity for finishing egg sorting
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Finished sorting eggs"
            ));
        }
    }

    public void PrepareExcavationStart()
    {
        //so the player knows where to teleport
        //excavationWaypoint.SetActive(true);
    }

    public void PrepareGloves()
    {
        //turn off waypoint and set gloves active so player can put them on
        //excavationWaypoint.SetActive(false);
        gloveL.SetActive(true);
        gloveR.SetActive(true);
    }


    public void PrepareSorting()
    {
        eggSorting.SetActive(true);
    }
}
