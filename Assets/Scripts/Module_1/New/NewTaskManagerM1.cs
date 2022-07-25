using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using DataCollection.Models;
using UnityEngine.SceneManagement;

public class NewTaskManagerM1 : MonoBehaviour
{
    //class variables
    #region
    public NewInstructionUpdaterM1 instrUpdater;
    public NewActivityManagerM1 activityMan;
    public RoomSwitch roomSwitch;
    public Fading fading;

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
    public AudioFeedback audiofeedback;
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
            audiofeedback.playGood();
            toolTable.FirstCalPickUp();
            LogAct("First Caliper Pick-Up");
        }

        //true when user completes third task by testing the calipers
        else if (taskCount == 3)
        {
            audiofeedback.playGood();
            toolTable.PrepareJar();
            LogAct("Completed caliper testing");
        }

        //true when user completes fourth task by measuring the jar
        else if (taskCount == 4)
        {
            audiofeedback.playGood();
            toolTable.PrepareTM();
            LogAct("Measured Jar with Calipers");
        }

        //true when user completes fifth task by picking up the tape measurer
        else if (taskCount == 5)
        {
            audiofeedback.playGood();
            toolTable.PrepareClipboard();
            LogAct("Tape Measure Picked-up");
        }

        //true when user completes sixth task by measuring the clipboard
        else if (taskCount == 6)
        {
            audiofeedback.playGood();
            activityMan.MarkActivityCompletion();
            PrepareWaypoint();
            LogAct("Measured Clipboard with Tape Measure");
        }

        //true when user completes seventh task by entering the waypoint
        else if (taskCount == 7)
        {
            DisableWaypoint();
            SetNextTable();
            measureTable.PrepareFrontFin();
        }

        //true when user completes eighth task by measuring the front fin
        else if (taskCount == 8)
        {
            measureTable.PrepareBackFin();
            LogAct("Successfully Measured the Front Fin");
        }

        //true when user completes ninth task by measuring the back fin
        else if (taskCount == 9)
        {
            measureTable.PrepareShellLength();
            LogAct("Successfully Measured the Back Fin");
        }

        //true when user completes tenth task by measuring the shell length
        else if (taskCount == 10)
        {
            measureTable.PrepareShellWidth();
            LogAct("Successfully Measured the Shell Length");
        }

        //true when user completes 11th task by measuring shell width
        else if (taskCount == 11)
        {
            measureTable.FinishTable();
            activityMan.MarkActivityCompletion();
            PrepareWaypoint();
            LogAct("Successfully measured the Shell Width");
        }

        //true when user completes 12th task by entering the waypoint
        else if (taskCount == 12)
        {
            DisableWaypoint();
            SetNextTable();
            identifyTable.PrepareTurtleIdentification();
        }

        //true when user completes 13th task by selecting begin
        else if (taskCount == 13)
        {
            identifyTable.PrepareQuestioning();
            identifyTable.SetNextQuestion();
        }

        //true when user completes 14th task by answering the first question
        else if (taskCount == 14)
        {
            identifyTable.SetNextQuestion();
        }

        //true when user completes 15th task by answering the second question
        else if (taskCount == 15)
        {
            identifyTable.SetNextQuestion();
        }

        //true when user completes 16th task by answering the third question
        else if (taskCount == 16)
        {
            activityMan.MarkActivityCompletion();
            PrepareWaypoint();
            LogAct("Completed Turtle Identification Table");
        }

        //true when user completes 17th task by entering the waypoint
        else if (taskCount == 17)
        {
            DisableWaypoint();
            SetNextTable();
            trackTable.PrepareTrackIdentification();
        }

        //true when user completes 18th task by selecting begin
        else if (taskCount == 18)
        {
            trackTable.PrepareQuestions();
            trackTable.SetNextQuestion();
        }

        //true when user completes the 19th task by answering the first question
        else if (taskCount == 19)
        {
            trackTable.SetNextQuestion();
        }

        //true when user completes the 20th task by answering the second question
        else if (taskCount == 20)
        {
            trackTable.SetNextQuestion();
        }

        //true when user completes the 21st task by answering the third question
        else if (taskCount == 21)
        {
            LogAct("Completed Track Identification Table");
            activityMan.MarkActivityCompletion();
        }

        #endregion
        //run the next set of instructions
        instrUpdater.RunInstructions();


    }

    private void PrepareScene()
    {
        toolCalipers.SetActive(true);
    }

    private void PrepareWaypoint()
    {
        waypoint.SetActive(true);
    }

    private void DisableWaypoint()
    {
        waypoint.SetActive(false);
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

    public void SetNextTable()
    {
        roomSwitch.switchRoom();
        fading.Fade(false, true);
    }
    
    private void LogAct(string message)
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
}
