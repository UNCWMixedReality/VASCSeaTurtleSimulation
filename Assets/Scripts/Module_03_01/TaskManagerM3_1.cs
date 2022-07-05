using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using DataCollection;
using DataCollection.Models;
using UnityEngine.SceneManagement;

public class TaskManagerM3_1 : MonoBehaviour
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
    public NewActivityManM3_1 activityManager;
    public InstructionUpdaterM3_1 instrUpdater;

    //number of tasks completed and when each task was completee
    public int taskCount { get; set; }
    public float[] taskTimes { get; set; }

    //game objects used in tasks
    public CleanManager cleanMan;
    public GPSManager GPSMan;
    public PasteManager pasteMan;

    public GameObject TableWaypoint;
    public GameObject TurtleWaypoint;
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
        }

        //true when the user completes the second task by entering the relocation waypoint
        else if (taskCount == 2)
        {
            Debug.Log("Entered table waypoint");
            //LogTask("Entered table waypoint");
            TableWaypoint.SetActive(false);
            cleanMan.PrepareCloth();
            activityManager.MarkActivityCompletion();

        }

        //true when the user completes the third task by successfully picking up cloth
        else if (taskCount == 3)
        {
            Debug.Log("Picked up cloth");
            cleanMan.PrepareClean();
            //LogTask("Successfully picked up cloth");
        }

        //true when the user completes the fourth task by cleaning shell
        else if (taskCount == 4)
        {
            //LogTask("Cleaned shell");
            Debug.Log("Cleaned shell");

        }

        //true when the user completes the fifth task by picking up the GPS
        else if (taskCount == 5)
        {
            //LogTask("Picked up GPS");
        }

        //true when the user completes the sixth task by placing the GPS
        else if (taskCount == 6)
        {
            //LogTask("Placed GPS");
        }

        //true when the user completes the seventh task by picking up shovel
        else if(taskCount == 7)
        {
            //LogTask("Picked up shovel");

        }

        //true when the user completes the eighth task by pasting GPS on back using shovel
        else if (taskCount == 8)
        {
            //LogTask("Pasted GPS on shell");
            PrepareEnd();
            activityManager.MarkActivityCompletion();
        }
        #endregion

        //Run the next set of instructions
        instrUpdater.RunInstructions();
    }

    private void PrepareScene()
    {

        // Activate table waypoint and disable all objects not used at first
        TableWaypoint.SetActive(true);
        cleanMan.DisableCloth();

    }

    private void LogTask1(string message)
    {
        /*
         * logs when a task is completed with the appropriate information
         */
        DcDataLogging.LogActivity(new Activity(
                DateTime.Now,
                SceneManager.GetActiveScene().name,
                message
                ));
    }

    private void PrepareEnd()
    {
    }
}