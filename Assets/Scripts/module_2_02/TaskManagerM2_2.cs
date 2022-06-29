using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using DataCollection;
using DataCollection.Models;
using UnityEngine.SceneManagement;

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
    public GameObject nestCageCollider;

    public GameObject sign;
    public GameObject signCollider;

    public GameObject nestEnclosure;

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
        Debug.Log("The task count is, " + (taskCount));

        //true when the user completes the first task by entering the scene
        if (taskCount == 1)
        {
            PrepareScene();
        }

        //true when the user completes the second task by entering the relocation waypoint
        else if (taskCount == 2)
        {
            PrepareReplacement();
            //Logs the player entering the relocation waypoint
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Entered relocation waypoint"
                ));
            
        }

        //true when the user completes the third task by successfully placing the eggs
        else if (taskCount == 3)
        {
            activityManager.MarkActivityCompletion();
            PrepareShovel();
            //Logs the player placing the eggs in the nest
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now,
                SceneManager.GetActiveScene().name,
                "Successfully placed eggs in the new nest"
                ));
        }

        //true when the user completes the fourth task by grabbing the shovel
        else if (taskCount == 4)
        {
            PrepareDigging();
            //Logs activity for grabbing the shovel
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Grabbed shovel"
                ));
        }

        //true when the user completes the fifth task by covering the nest with sand
        else if (taskCount == 5)
        {
            PrepareCage();
            //Logs activity for covering nest with sand
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Covered nest with sand"
                ));
        }

        //true when the user completes the sixth task by grabbing the cage
        else if (taskCount == 6)
        {
            PrepareCovering();
            //Logs activity for grabbing cage
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Cage grabbed"
                ));
        }

        //true when the user completes the seventh task by placing the cage
        else if(taskCount == 7)
        {
            PrepareSign();
            //Logs activity for placing the cage
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Cage Placed"
                ));
        }

        //true when the user completes the eighth task by grabbing the sign
        else if (taskCount == 8)
        {
            PrepareSignPlacement();
            //Logs activity for grabbing the sign
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Sign grabbed"
                ));
        }

        //true when user completes the ninth task by placing the sign
        else if (taskCount == 9)
        {
            activityManager.MarkActivityCompletion();
            PrepareEnd();
            //Logs activity for placing the sign
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now, 
                SceneManager.GetActiveScene().name,
                "Sign placed"
                ));
        }
        //Run the next set of instructions
        instrUpdater.RunInstructions();
    }

    private void PrepareScene()
    {
        relocationWaypoint.SetActive(true);

        //stop outlining the shovel
        shovel.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(2).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(5).gameObject.GetComponent<Outline>().enabled = false;

        //stop outlining the cage
        cage.GetComponent<Outline>().enabled = false;

        //stop outling the sign
        sign.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        sign.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = false;
    }
    private void PrepareReplacement()
    {
        //removes the waypoint
        relocationWaypoint.SetActive(false);

        //outlines eggs so they can be seen and enables the grab functionality
        GameObject[] eggList = { goodEgg1, goodEgg2, goodEgg3, goodEgg4, goodEgg5, goodEgg6 };
        for (int x = 0; x < eggList.Length; x++)
        {
            eggList[x].SetActive(true);
            eggList[x].GetComponent<Outline>().enabled = true;
            eggList[x].GetComponent<DcGrabInteractable>().enabled = true;
        }
    }

    private void PrepareShovel()
    {
        //we no longer need to outline/grab the eggs, so turn those components off
        GameObject[] eggList = { goodEgg1, goodEgg2, goodEgg3, goodEgg4, goodEgg5, goodEgg6 };
        for (int x = 0; x < eggList.Length; x++)
        {
            eggList[x].GetComponent<Outline>().enabled = false;
            eggList[x].GetComponent<DcGrabInteractable>().enabled = false;
        }

        //now we need to enable the shovel
        shovel.GetComponent<DcGrabInteractable>().enabled = true;

        //outline the shovel
        shovel.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = true;
        shovel.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = true;
        shovel.transform.GetChild(2).gameObject.GetComponent<Outline>().enabled = true;
        shovel.transform.GetChild(5).gameObject.GetComponent<Outline>().enabled = true;

    }

    private void PrepareDigging()
    {
        diggingWaypoint.SetActive(true);
        nestSandCollider.SetActive(true);

        //stop outlining the shovel
        shovel.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(2).gameObject.GetComponent<Outline>().enabled = false;
        shovel.transform.GetChild(5).gameObject.GetComponent<Outline>().enabled = false;
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
        //stop outlining the cage
        cage.GetComponent<Outline>().enabled = false;

        nestCageCollider.SetActive(true);
    }

    private void PrepareSign()
    {
        //stop grabbing the cage 
        cage.GetComponent<DcGrabInteractable>().enabled = false;

        //outline and enable grabbing the sign
        sign.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = true;
        sign.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = true;

        sign.GetComponent<DcGrabInteractable>().enabled = true;
    }

    private void PrepareSignPlacement()
    {
        //stop outling the sign
        sign.transform.GetChild(0).gameObject.GetComponent<Outline>().enabled = false;
        sign.transform.GetChild(1).gameObject.GetComponent<Outline>().enabled = false;

        //activate the placement collider
        signCollider.SetActive(true);
    }

    private void PrepareEnd()
    {
        nestEnclosure.SetActive(true);
    }
}