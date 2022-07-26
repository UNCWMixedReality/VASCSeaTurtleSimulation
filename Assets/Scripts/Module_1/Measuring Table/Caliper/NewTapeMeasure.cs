using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewTapeMeasure : MonoBehaviour
{
    public GameObject tmPlaceholder;

    public Text floatingText;

    public CaliperMeasure rightCollider;
    public CaliperMeasure leftCollider;

    public NewTaskManagerM1 taskMan;
    public int taskNum;


    private bool pickedUp;

    [HideInInspector]
    public float length = 0;


    void Start()
    {
    }

    public void Update()
    {
        floatingText.text = ((int)length).ToString() + "cm";
        if (pickedUp)
        {
            if (OVRInput.Get(OVRInput.RawButton.A) || OVRInput.Get(OVRInput.RawButton.X))
            {

                Debug.Log("extending");
                //extend();
            }

            else if (OVRInput.Get(OVRInput.RawButton.B) || OVRInput.Get(OVRInput.RawButton.Y))
            {

                Debug.Log("retracting");
                //retract();
            }

            checkMeasure();
        }


    }


    public void checkMeasure()
    {
        if (rightCollider.collided && leftCollider.collided)
        {
            Debug.Log("tm Measured");
            taskMan.MarkTaskCompletion(taskNum);
        }
    }

    public void togglePickedUp()
    {
        pickedUp = !pickedUp;
        if (!pickedUp)
        {
            gameObject.transform.position = tmPlaceholder.transform.position;
            gameObject.transform.rotation = tmPlaceholder.transform.rotation;
        }
    }

    public void ResetCaliper()
    {
        length = 0;
    }
}
