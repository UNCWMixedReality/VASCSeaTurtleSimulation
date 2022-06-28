using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using DataCollection;

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
    public GameObject diggingWaypoint;

    public GameObject goodEgg1;
    public GameObject goodEgg2;
    public GameObject goodEgg3;
    public GameObject goodEgg4;
    public GameObject goodEgg5;
    public GameObject goodEgg6;

    public GameObject shovel;
    public GameObject nestSandCollider;
    public GameObject cage;

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

        //true when the user completes the third task by successfully placing the eggs
        else if (taskCount == 3)
        {
            PrepareShovel();
        }

        //true when the user completes the fourth task by grabbing the shovel
        else if (taskCount == 4)
        {
            PrepareDigging();
        }

        //true when the user completes the fifth task by covering the nest with sand
        else if (taskCount == 5)
        {
            PrepareCage();
        }

        //true when the user completes the sixth task by grabbing the cage
        else if (taskCount == 6)
        {
            PrepareCovering();
        }
        //Run the next set of instructions
        instrUpdater.RunInstructions();
    }

    private void PrepareReplacement()
    {
        //outlines eggs so they can be seen and enables the grab functionality
        GameObject[] eggList = { goodEgg1, goodEgg2, goodEgg3, goodEgg4, goodEgg5, goodEgg6 };
        for (int x = 0; x <= eggList.Length; x++)
        {
            eggList[x].GetComponent<Outline>().enabled = true;
            eggList[x].GetComponent<DcGrabInteractable>().enabled = true;
        }
    }

    private void PrepareShovel()
    {
        //we no longer need to outline/grab the eggs, so turn those components off
        GameObject[] eggList = { goodEgg1, goodEgg2, goodEgg3, goodEgg4, goodEgg5, goodEgg6 };
        for (int x = 0; x <= eggList.Length; x++)
        {
            eggList[x].GetComponent<Outline>().enabled = false;
            eggList[x].GetComponent<DcGrabInteractable>().enabled = false;
        }

        //now we need to enable the shovel
        shovel.GetComponent<DcGrabInteractable>().enabled = true;
    }

    private void PrepareDigging()
    {
        diggingWaypoint.SetActive(true);
        nestSandCollider.SetActive(true);
    }

    private void PrepareCage()
    {
        diggingWaypoint.SetActive(false);

        //outline and enable grabbing
        cage.GetComponent<Outline>().enabled = true;
        cage.GetComponent<DcGrabInteractable>().enabled = true;
    }

    private void PrepareCovering()
    {
        //a
    }
}