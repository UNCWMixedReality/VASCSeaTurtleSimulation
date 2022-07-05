using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTaskManagerM1 : MonoBehaviour
{
    //class variables
    #region
    public NewInstructionUpdaterM1 instrUpdater;
    public NewActivityManagerM1 activityMan;

    //number of tasks completed and when each task was completee
    public int taskCount { get; set; }
    public float[] taskTimes { get; set; }

    //Table Manager Scripts
    public NewToolManagerM1 toolTable;
    public NewMeasuringManagerM1 measureTable;
    public NewIdentificationManagerM1 identifyTable;
    public NewTrackManagerM1 trackTable;

    //waypoint
    public GameObject waypoint;

    //tool table objects
    public GameObject toolCalipers;
    #endregion

    public void MarkTaskCompletion(int taskID)
    {
        if(taskID != taskCount)
        {
            return;
        }

        //mark time and increment task count
        taskTimes[taskCount] = Time.time;
        taskCount += 1;
        Debug.Log("The task count is, " + (taskCount));

        //perform appropriate set up based on current task count
        #region
        if(taskCount == 1)
        {
            PrepareScene();
        }
        #endregion
        //run the next set of instructions
        //instrUpdater.RunInstructions();


    }

    private void PrepareScene()
    {
        toolCalipers.SetActive(true);
    }
}
