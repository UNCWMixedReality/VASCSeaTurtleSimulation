using System;
using System.Collections;
using System.Collections.Generic;
using DataCollection;
using DataCollection.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFunctionality : MonoBehaviour
{
    public GameObject firstDoor;
    public GameObject secondDoor;
    public AudioSource doorA;
    public AudioSource doorB;
    public GameObject teleportInstructions;
    public GameObject snapInstructions;
    public AudioFeedback audiofeedback;
    public AudioConvoTutorial ACT;
    public IMTutorial IMT;
    public CompassManager compMan;
    public GameObject[] waypoints;

    private int stage = 0;
    private Vector2 touchPadL = new Vector2();
    private Vector2 touchPadR = new Vector2();

    void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            firstSnapTurn();
        }
    }

    public void firstTeleport()
    {
        if (stage == 0)
        {
            
            stage++;
            audiofeedback.playGood();
            displaySnapTurn();
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now,
                "Completed first Teleport"
            ));
        }
    }

    public void firstSnapTurn()
    {
        if(stage == 1)
        {
            firstDoor.GetComponent<Animator>().SetTrigger("trigger_start");
            waypoints[0].SetActive(true);
            compMan.EnableCompass(waypoints[0]);
            ACT.playSound();
            IMT.changePanel(3);
            stage++;
            audiofeedback.playGood();
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now,
                "Completed first Snap Turn"
            ));
        }
    }

    public void firstObjectGrab()
    {
        audiofeedback.playSelection();
        if (stage == 2)
        {
            firstDoor.GetComponent<Animator>().SetTrigger("trigger_close");
            secondDoor.GetComponent<Animator>().SetTrigger("trigger_start");
            waypoints[0].SetActive(false);
            waypoints[1].SetActive(true);
            compMan.EnableCompass(waypoints[1]);

            ACT.playSound();
            IMT.changePanel(5);
            stage++;
            audiofeedback.playGood();
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now,
                "Completed first Object Grab"
                ));
        }
    }

    public void firstButtonClick()
    {
        if (stage == 3)
        {
            audiofeedback.playGood();
            DcDataLogging.LogActivity(new Activity(
                DateTime.Now,
                "Completed first Button Click"
                ));
            DcDataLogging.EndSession();
            SceneManager.LoadScene("JustModule");
            
        }

    }

    void displaySnapTurn()
    {
        teleportInstructions.SetActive(false);
        snapInstructions.SetActive(true);
        ACT.playSound();
        IMT.changePanel(2);
    }
}
