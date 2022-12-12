using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TapeMeasureSize : MonoBehaviour
{
    // Reference transforms of the tape meausre body and end of tape measure
    public Transform endPos;
    public Transform bodyPos;
    
    // Text and tape changed by this script
    public Text floatingText;
    public Transform text;
    public Transform playerhead;
    public GameObject line;

    // calculaion variables
    private float length;
    private Vector3 textLocation;
    private Vector3 tapeStartingScale;

    // variables for measuring and progression
    public NewTaskManagerM1 taskMan;
    public TMMeasure LCollider;
    public TMMeasure RCollider;
    public int taskNum;



    void Start()
    {
        tapeStartingScale = line.transform.localScale; // storing initial scale to scale tape up later
    }

    // LateUpdate is used so that it moves the tape and text after the physics calculations are done in update
    public void LateUpdate()
    {
        // Updating measuring tape text
        length = Vector3.Distance(endPos.position, bodyPos.position);
        floatingText.text = ((int)(length * 100)).ToString() + "cm";
        textLocation = Vector3.Lerp(bodyPos.position, endPos.position, 0.5f);
        text.position = new Vector3(textLocation.x, textLocation.y + .07f, textLocation.z);
        text.LookAt(playerhead);


        // Stretching tape between body and end
        line.transform.position = bodyPos.position;
        line.transform.LookAt(endPos);
        line.transform.localEulerAngles = new Vector3(line.transform.localEulerAngles.x, line.transform.localEulerAngles.y, 0);
        line.transform.localScale = new Vector3(tapeStartingScale.x, tapeStartingScale.y, tapeStartingScale.z + length*250);


        // checking if measuring colliders have been triggered on both sides
        if (LCollider is not null && RCollider is not null && taskMan is not null)
        {
            if (LCollider.collided && RCollider.collided)
            {
                Debug.Log("TM measured");
                taskMan.MarkTaskCompletion(taskNum);
                LCollider.enabled = false;
                RCollider.enabled = false;
            }

        }
    }
}
