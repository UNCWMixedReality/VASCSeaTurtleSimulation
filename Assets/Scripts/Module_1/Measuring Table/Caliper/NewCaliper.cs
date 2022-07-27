using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewCaliper : MonoBehaviour
{
    public GameObject caliperPlaceholder;
    public GameObject movingPart;

    public Text displayText;
    public Text floatingText;

    public CaliperMeasure rightCollider;
    public CaliperMeasure leftCollider;

    public GameObject button;
    public Text buttonText;

    public NewTaskManagerM1 taskMan;
    public int taskNum;

    private Vector3 movingPartStartingPos;

    private bool firstAPress;
    private bool firstBPress;
    private bool pickedUp;

    [HideInInspector]
    public float length = 0;

    void Start()
    {
        movingPartStartingPos = movingPart.transform.localPosition;
    }

    public void FixedUpdate()
    {
        displayText.text = ((int)length).ToString() + "cm";
        floatingText.text = ((int)length).ToString() + "cm";
        if (pickedUp) {
            if (OVRInput.Get(OVRInput.RawButton.A) || OVRInput.Get(OVRInput.RawButton.X))
            {
                if (!firstAPress)
                {
                    firstAPress = true;
                    buttonText.text = "B";
                }
                extend();
            }
            else if (OVRInput.Get(OVRInput.RawButton.B) || OVRInput.Get(OVRInput.RawButton.Y))
            {
                if (firstAPress)
                {
                    taskMan.MarkTaskCompletion(2);
                    HideButton();
                }
                retract();
            }
            checkMeasure();
        }
    }
    private void extend()
    {
        if (length < 15) { length = length + 0.1f; }
        movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x - (length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
    }

    private void retract()
    {
        if (length > 0) { length = length - 0.1f; }
        movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x - (length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
    }

    public void checkMeasure()
    {
        if (rightCollider.collided && leftCollider.collided)
        {
            Debug.Log("tubeMeasured");
            taskMan.MarkTaskCompletion(taskNum);
        } 
    }

    public void togglePickedUp()
    {
        pickedUp = !pickedUp;
        if (!pickedUp)
        {
            gameObject.transform.position = caliperPlaceholder.transform.position;
            gameObject.transform.rotation = caliperPlaceholder.transform.rotation;
        }
    }

    public void ResetCaliper()
    {
        movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x, movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
        length = 0;
    }

    public void Showbutton()
    {
        button.SetActive(true);
        buttonText.text = "A";
    }
    public void HideButton()
    {
        button.SetActive(false);
    }

}
