using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    //reference to activity manager script
    public New_Activity_Manager activityManager;

    //number of tasks completed
    public int taskCount;
    //time each task was completed
    private float[] taskTimes;

    public void Start()
    {
        taskCount = 0;
        taskTimes = new float[5];
    }

    public void MarkTaskCompletion()
    {
        //mark completion time
        taskTimes[taskCount] = Time.time;
        //update number of completed tasks
        taskCount += 1;
        // check if first or second activity has been completed based on taskCount
        if (taskCount == 4 || taskCount == 5)
        {
            activityManager.MarkActivityCompletion();
        }
    }
}
