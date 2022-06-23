using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //number of tasks completed
    public int taskCount { get; set; }
    //time each task was completed
    public float[] taskTimes { get; set; }

    //game objects used in tasks
    public GameObject excavationWaypoint;
    public GameObject gloveL;
    public GameObject gloveR;
    public GameObject egg1;
    public GameObject egg2;
    public GameObject egg3;
    public GameObject egg4;
    public GameObject egg5;
    public GameObject egg6;
    public GameObject crackedEgg1;
    public GameObject crackedEgg2;
    public GameObject halfEgg1;
    public GameObject halfEgg2;
    public GameObject eggCounterPanel;

    public void MarkTaskCompletion()
    {

        /*
         * 
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
            instrUpdater.RunInstructions();
        }

        //this is true if the player has completed the second task by entering the excavation waypoint
        if (taskCount == 2)
        {
            //set everything up for the third task
            PrepareGloves();
            instrUpdater.RunInstructions();
        }

        //this is true if the player has completed the third task by putting on the gloves
        if (taskCount == 3)
        {
            //sets everything up for the fourth task
            PrepareDigging();
            instrUpdater.RunInstructions();
        }
        
        //this is true if the player hsa completed the fourth task by digging up the nest
        if (taskCount == 4)
        {
            activityManager.MarkActivityCompletion();
            PrepareSorting();
            instrUpdater.RunInstructions();
        }
        //true if the fifth and final task is completed
        if (taskCount == 5)
        {
            activityManager.MarkActivityCompletion();
        }
    }

    public void PrepareExcavationStart()
    {
        //so the player knows where to teleport
        excavationWaypoint.SetActive(true);
    }

    public void PrepareGloves()
    {
        //turn off waypoint and set gloves active so player can put them on
        excavationWaypoint.SetActive(false);
        gloveL.SetActive(true);
        gloveR.SetActive(true);
    }

    public void PrepareDigging()
    {
        //set eggs active so player can dig up nest
        egg1.SetActive(true);
        egg2.SetActive(true);
        egg3.SetActive(true);
        egg4.SetActive(true);
        egg5.SetActive(true);
        egg6.SetActive(true);
        crackedEgg1.SetActive(true);
        crackedEgg2.SetActive(true);
        halfEgg1.SetActive(true);
        halfEgg2.SetActive(true);
        
    }

    public void PrepareSorting()
    {
        //turns on the panel that shows how many eggs have been sorted
        eggCounterPanel.SetActive(true);
    }
}
