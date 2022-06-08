using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
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
            firstDoor.GetComponent<Animator>().SetTrigger("trigger_start");
            //doorA.Play();
            ACT.playSound();
            IMT.changePanel(3);
            stage++;
            audiofeedback.playGood();
            Debug.Log("First Snap Turn");
            Debug.Log($"Stage: {stage}");
        }
    }

    public void firstObjectGrab()
    {
        audiofeedback.playSelection();
        if (stage == 2)
        {
            firstDoor.GetComponent<Animator>().SetTrigger("trigger_start");
            secondDoor.GetComponent<Animator>().SetTrigger("trigger_start");

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
}
