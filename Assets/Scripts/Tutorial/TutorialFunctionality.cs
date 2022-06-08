using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TutorialFunctionality : MonoBehaviour
{

    public GameObject firstDoor;
    public GameObject secondDoor;
    public AudioSource doorA;
    public AudioSource doorB;
    public GameObject teleportInstructions;
    public GameObject snapInstructions;
    public GameObject MoveRooms;
    public AudioFeedback audiofeedback;
    public AudioConvoTutorial ACT;
    public IMTutorial IMT;
    public PlayableDirector director;
    public PlayableDirector directorRm2;
    


    private int stage = 0;
    private Vector2 touchPadL = new Vector2();
    private Vector2 touchPadR = new Vector2();


    
    void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
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
            Debug.Log("First Teleport");
            Debug.Log($"Stage: {stage}");
        }
    }

    public void firstSnapTurn()
    {
        if(stage == 1)
        {
            Destroy(firstDoor);
            //doorA.Play();
            ACT.playSound();
            IMT.changePanel(3);
            displayMoveRoom2();

            stage++;
            
            audiofeedback.playGood();
            Debug.Log("First Snap Turn");
            Debug.Log($"Stage: {stage}");
            
            
        }
        
    }

    public void firstObjectGrab()
    {
        
        audiofeedback.playSelection();
        StopFirstTimeline();
        if (stage == 2)
        {
            
            Destroy(secondDoor);
            StartSecondTimeline();
            //doorB.Play();
            ACT.playSound();
            IMT.changePanel(5);
            stage++;
            audiofeedback.playGood();
            Debug.Log("First Object Grab");
            Debug.Log($"Stage: {stage}");
        }
    }

    public void firstButtonClick()
    {
        StopSecondTimeline();
        if (stage == 3)
        {
            audiofeedback.playGood();
            SceneManager.LoadScene("Main");
            Debug.Log("First Button Click");
            Debug.Log($"Stage: {stage}");
        }

    }

    void displaySnapTurn()
    {
        teleportInstructions.SetActive(false);
        snapInstructions.SetActive(true);
        ACT.playSound();
        IMT.changePanel(2);
    }
    void displayMoveRoom2()
    {
        
        teleportInstructions.SetActive(false);
        snapInstructions.SetActive(false);
        MoveRooms.SetActive(true);
        StartFirstTimeline();

    }
    public void StartFirstTimeline()
    {
        director.Play();
    }
    public void StartSecondTimeline()
    {
        directorRm2.Play();
    }
    public void StopFirstTimeline()
    {
        director.Stop();
    }
    public void StopSecondTimeline()
    {
        directorRm2.Stop();
    }


}
