using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerM2_2 : MonoBehaviour
{
    /*
     * There are 9 tasks in module 2 part 2, this script is used to mark tasks as complete and to setup the scene for the next task
     * tasks: enter scene, teleport to nest, place eggs, grab shovel, cover nest with sand, grab cage, cover nest with cage, grab sign, place sign
     * 
     * So for instance, when the player puts on gloves, the function MarkTaskCompleted() is called which increments the task count and calls PrepareDigging()
     * so the player can start the next task of digging up the egg nest.
     * 
     */


    //reference to activity manager script and instruction updater
    public NewActivityManM2_2 activityManager;
    public InstructionUpdaterM2_2 instrUpdater;

    //number of tasks completed
    public int taskCount { get; set; }
    //time each task was completed
    public float[] taskTimes { get; set; }

    //game objects used in tasks
    public GameObject relocationWaypoint;


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

        //true when the user completes the first task by entering the scene
        if (taskCount == 1)
        {
            relocationWaypoint.SetActive(true);
        }

        //true when the user completes the second task by entering the relocation waypoint
        else if (taskCount == 2)
        {
            PrepareReplacement();
        }


        //Run the next set of instructions
        instrUpdater.RunInstructions();
    }

    public void PrepareReplacement()
    {
        //A
    }

}