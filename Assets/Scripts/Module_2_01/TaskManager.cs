using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    //reference to activity manager script
    public New_Activity_Manager activityManager;
    public InstructionUpdater instrUpdater;

    //number of tasks completed
    public int taskCount;
    //time each task was completed
    public float[] taskTimes;

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
        //mark completion time
        taskTimes[taskCount] = Time.time;
        //update number of completed tasks
        taskCount += 1;
        Debug.Log("The task count is, " + (taskCount));
        //this is true if the player has completed the first task by entering the scene
        if (taskCount == 1)
        {
            //set everything up for the second task.
            PrepareExcavationStart();
            StartCoroutine(instrUpdater.RunInstructions());
        }

        //this is true if the player has completed the second task by entering the excavation waypoint
        if (taskCount == 2)
        {
            //set everything up for the third task
            PrepareGloves();
            StartCoroutine(instrUpdater.RunInstructions());
        }

        //this is true if the player has completed the third task by putting on the gloves
        if (taskCount == 3)
        {
            //sets everything up for the fourth task
            PrepareDigging();
            StartCoroutine(instrUpdater.RunInstructions());
        }
        
        //this is true if the player hsa completed the fourth task by digging up the nest
        if (taskCount == 4)
        {
            activityManager.MarkActivityCompletion();
            PrepareSorting();
            StartCoroutine(instrUpdater.RunInstructions());
        }
        //true if the fifth and final task is completed
        if (taskCount == 5)
        {
            activityManager.MarkActivityCompletion();
        }
    }

    public void PrepareExcavationStart()
    {
        excavationWaypoint.SetActive(true);
    }

    public void PrepareGloves()
    {
        excavationWaypoint.SetActive(false);
        gloveL.SetActive(true);
        gloveR.SetActive(true);
    }

    public void PrepareDigging()
    {
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
        eggCounterPanel.SetActive(true);
    }
}
