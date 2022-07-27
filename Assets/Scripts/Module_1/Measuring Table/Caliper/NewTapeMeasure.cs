using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewTapeMeasure : MonoBehaviour
{
    public GameObject tapeMeasure;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject tmPlaceholder;
    public GameObject headCamera;

    public GameObject tapePoint;
    public GameObject tapeEndCol;

    public Canvas measureCanvas;
    public Text measureText;

    public TMMeasure rightCollider;
    public TMMeasure leftCollider;

    [HideInInspector]
    public string measureLength;
    bool pickedUp = false;
    private bool measurePaused = false;
    private LineRenderer line;
    private string activeHand;

    public NewTaskManagerM1 taskMan;

    public int taskNum;


    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>(); //Initialize line renderer to not be visible.
        line.SetWidth(0.0F, 0.0F);
        line.SetVertexCount(2);
    }

    IEnumerator Pause()
    {
        //Pauses the measure text on the exact measurements to make sure the user knows what they are.
        measurePaused = true;
        measureText.color = Color.green;
        yield return new WaitForSeconds(3);

        measurePaused = false;
        measureText.color = Color.white;
    }

    IEnumerator GetHand()
    {
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(this.transform.position, leftHand.transform.position) < Vector3.Distance(this.transform.position, rightHand.transform.position))
        {
            activeHand = "left";
        }
        else
        {
            activeHand = "right";
        }
        Debug.Log(activeHand);
    }

    public void FixedUpdate()
    {
        if (pickedUp)
        {
            line.SetWidth(0.02F, 0.02F);
            if (activeHand == "left") //Draw line between tape measure and right hand.
            {
                line.SetPosition(0, tapePoint.transform.position);
                line.SetPosition(1, rightHand.transform.position);
                tapeEndCol.transform.position = rightHand.transform.position;
            }
            else if (activeHand == "right") //Draw line between tape measure and left hand.
            {
                line.SetPosition(0, tapePoint.transform.position);
                line.SetPosition(1, leftHand.transform.position);
                tapeEndCol.transform.position = leftHand.transform.position;
            }

            //Find length of tape measure.
            float length = Mathf.Round(Mathf.Sqrt(Mathf.Pow(leftHand.transform.position.x - rightHand.transform.position.x, 2f) +
                                              Mathf.Pow(leftHand.transform.position.y - rightHand.transform.position.y, 2f) +
                                              Mathf.Pow(leftHand.transform.position.z - rightHand.transform.position.z, 2f)) * 100);
            //Find center of tape measure to place text.
            Vector3 center = new Vector3((leftHand.transform.position.x + rightHand.transform.position.x) / 2,
                                         (leftHand.transform.position.y + rightHand.transform.position.y) / 2,
                                         (leftHand.transform.position.z + rightHand.transform.position.z) / 2);

            measureCanvas.transform.position = center;
            measureCanvas.transform.LookAt(headCamera.transform);

            if (measurePaused == false)
                measureText.text = length.ToString() + " cm"; //Tape measure text output.

            checkMeasure();
        }
        else
        {
            line.SetWidth(0.0F, 0.0F); //Clear line if tape measure is not picked up.
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));
            measureText.text = "";
        }
    }

    public void checkMeasure()
    {
        if (rightCollider.collided && leftCollider.collided)
        {
            Debug.Log("TapeMeasuere Measured");
            measureText.text = measureLength;
            StopAllCoroutines();
            taskMan.MarkTaskCompletion(taskNum);
            StartCoroutine(Pause());
        }
    }

    public void togglePickedUp()
    {
        pickedUp = !pickedUp;
        if (pickedUp) //Determine which hand is holding the tape measure.
        {
            StartCoroutine(GetHand());
        } else
        {
            tapeMeasure.transform.position = tmPlaceholder.transform.position;
            tapeMeasure.transform.rotation = tmPlaceholder.transform.rotation;
            tapeMeasure.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
