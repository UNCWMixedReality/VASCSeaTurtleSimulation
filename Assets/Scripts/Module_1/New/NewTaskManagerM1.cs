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

        //true when user completes first task by entering the scene
        if(taskCount == 1)
        {
            PrepareScene();
        }

        //true when user completes second task by grabbing the calipers
        else if (taskCount == 2)
        {
            toolTable.FirstCalPickUp();
        }

        //true when user completes third task by testing the calipers
        else if (taskCount == 3)
        {
            toolTable.PrepareJar();
        }

        //true when user completes fourth task by measuring the jar
        else if (taskCount == 4)
        {
            toolTable.PrepareTM();
        }

        //true when user completes fifth task by picking up the tape measurer
        else if (taskCount == 5)
        {
            toolTable.PrepareClipboard();
        }

        //true when user completes sixth task by measuring the clipboard
        else if (taskCount == 6)
        {
            activityMan.MarkActivityCompletion();
            PrepareWaypoint();
        }

        //true when user completes seventh task by entering the waypoint
        else if (taskCount == 7)
        {
            measureTable.PrepareFrontFin();
        }

        //true when user completes eighth task by measuring the front fin
        else if (taskCount == 8)
        {
            measureTable.PrepareBackFin();
        }

        //true when user completes ninth task by measuring the back fin
        else if (taskCount == 9)
        {
            measureTable.PrepareShellLength();
        }

        //true when user completes tenth task by measuring the shell length
        else if (taskCount == 10)
        {
            measureTable.PrepareShellWidth();
        }

        //true when user completes 11th task by measuring shell width
        else if (taskCount == 11)
        {
            measureTable.FinishTable();
            activityMan.MarkActivityCompletion();
            PrepareWaypoint();
        }

        //true when user completes 12th task by entering the waypoint
        else if (taskCount == 12)
        {
            //identifyTable.PrepareBegin();
        }

        //true when user completes 13th task by selecting begin
        else if (taskCount == 13)
        {
            //identifyTable.PrepareQuestion1();
        }

        //true when user completes 14th task by answering the first question
        else if (taskCount == 14)
        {
            //identifyTable.PrepareQuestion2();
        }

        //true when user completes 15th task by answering the second question
        else if (taskCount == 15)
        {
            //identifyTable.PrepareQuestion3();
        }

        //true when user completes 16th task by answering the third question
        else if (taskCount == 16)
        {
            activityMan.MarkActivityCompletion();
            PrepareWaypoint();
        }

        //true when user completes 17th task by entering the waypoint
        else if (taskCount == 17)
        {
            //trackTable.PrepareBegin();
        }

        //true when user completes 18th task by selecting begin
        else if (taskCount == 18)
        {
            //trackTable.PrepareQuestion1();
        }

        //true when user completes the 19th task by answering the first question
        else if (taskCount == 19)
        {
            //trackTable.PrepareQuestion2();
        }

        //true when user completes the 20th task by answering the second question
        else if (taskCount == 20)
        {
            //trackTable.PrepareQuestion3();
        }

        //true when user completes the 21st task by answering the third question
        else if (taskCount == 21)
        {
            activityMan.MarkActivityCompletion();
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
        waypoint.SetActive(true);
    }

    public void TeleportationCompleted()
    {
        //determine when teleporting is an act of completing a task and complete the corresponding task.
        if (taskCount == 6)
        {
            MarkTaskCompletion(6);
        }
        else if (taskCount == 11)
        {
            MarkTaskCompletion(11);
        }
        else if (taskCount == 16)
        {
            MarkTaskCompletion(16);
        }
    }
}
