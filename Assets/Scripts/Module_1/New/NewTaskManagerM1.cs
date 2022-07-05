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
        else if (taskCount == 2)
        {
            toolTable.FirstCalPickUp();
        }
        else if (taskCount == 3)
        {
            toolTable.PrepareJar();
        }
        else if (taskCount == 4)
        {
            //toolTable.PrepareTM();
        }
        else if (taskCount == 5)
        {
            //toolTable.PrepareClipboard();
        }
        else if (taskCount == 6)
        {
            PrepareWaypoint();
        }
        else if (taskCount == 7)
        {
            //measureTable.PrepareFrontFin();
        }
        else if (taskCount == 8)
        {
            //measureTable.PrepareBackFin();
        }
        else if (taskCount == 9)
        {
            //measureTable.PrepareShellLength();
        }
        else if (taskCount == 10)
        {
            //measureTable.PrepareShellWidth();
        }
        else if (taskCount == 11)
        {
            PrepareWaypoint();
        }
        else if (taskCount == 12)
        {
            //identifyTable.PrepareBegin();
        }
        else if (taskCount == 13)
        {
            //identifyTable.PrepareQuestion1();
        }
        else if (taskCount == 14)
        {
            //identifyTable.PrepareQuestion2();
        }
        else if (taskCount == 15)
        {
            //identifyTable.PrepareQuestion3();
        }
        else if (taskCount == 16)
        {
            PrepareWaypoint();
        }
        else if (taskCount == 17)
        {
            //trackTable.PrepareBegin();
        }
        else if (taskCount == 18)
        {
            //trackTable.PrepareQuestion1();
        }
        else if (taskCount == 19)
        {
            //trackTable.PrepareQuestion2();
        }
        else if (taskCount == 20)
        {
            //trackTable.PrepareQuestion3();
        }
        else if (taskCount == 21)
        {
            EndScene();
        }

        #endregion
        //run the next set of instructions
        //instrUpdater.RunInstructions();


    }

    private void PrepareScene()
    {
        toolCalipers.SetActive(true);
    }

    private void PrepareWaypoint()
    {
        //
    }

    private void EndScene()
    {
        //
    }
}
