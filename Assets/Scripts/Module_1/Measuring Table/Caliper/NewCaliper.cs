using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewCaliper : MonoBehaviour
{
    public GameObject caliper;
    public GameObject movingPart;
    public GameObject calipColliderL;
    public GameObject calipColliderR;

    public Text displayText;
    public Text floatingText;

    public CaliperMeasure rightCollider;
    public CaliperMeasure leftCollider;

    public NewTaskManagerM1 taskMan;
    public int taskNum;

    private Vector3 movingPartStartingPos;
    private Vector3 caliperStartingPos;
    private bool firstAPress;
    private bool firstBPress;

    [HideInInspector]
    public float length = 0;


    void Start()
    {
        // stores initial position of plunger and initial scales of sample and tube sample
        movingPartStartingPos = movingPart.transform.localPosition;
        caliperStartingPos = caliper.transform.position;
    }

    public void Update()
    {
        displayText.text = Math.Round(length).ToString() + "cm";
        floatingText.text = Math.Round(length, 2).ToString() + "cm";

        // Checks if the syringe is colliding with turtle collider and if the extending buttons (A and X) are being pressed
        // if so, calls extend until length > 5.5, then marks task complete
        if (OVRInput.Get(OVRInput.RawButton.A) || OVRInput.Get(OVRInput.RawButton.X))
        {
            if (!firstAPress)
                firstAPress = true;

            Debug.Log("extending");
            extend();
        }

        // Checks if the syringe is colliding with tube collider and if the retracting buttons (B and Y) are being pressed
        // if so, calls retract until length < 0.5, then marks task complete
        else if (OVRInput.Get(OVRInput.RawButton.B) || OVRInput.Get(OVRInput.RawButton.Y))
        {
            if (firstAPress)
                taskMan.MarkTaskCompletion(2);

            Debug.Log("retracting");
            retract();
        }

        checkMeasure();

    }

    // Extends the plunger of the syringe until length reaches 6.5
    // Scales DNA sample in syringe to portray the syringe filling up
    private void extend()
    {
        if (length < 15) { length = length + 0.1f; }
        movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x - (length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);

    }

    // Retracts the plunger of the syringe until the length reaches 0 (back to fully in syringe)
    // Scales DNA sample both inside syringe and test tube to portray syringe emptying and test tube filling up
    private void retract()
    {
        if (length > 0) { length = length - 0.1f; }
        movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x - (length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
    }

    public void OnSelectEnter()
    {
        enabled = true;
    }
    public void OnSelectExit()
    {
        enabled = false;
        caliper.transform.position = caliperStartingPos;
    }

    public void checkMeasure()
    {
        if (rightCollider.collided && leftCollider.collided)
        {
            Debug.Log("tubeMeasured");
            taskMan.MarkTaskCompletion(taskNum);
        } 
    }
}
