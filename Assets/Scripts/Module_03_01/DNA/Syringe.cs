using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    public GameObject syringe;
    public GameObject syringePlunger;
    public GameObject syringeSample;
    public GameObject tubeSample;

    public DrawDNA turtleCollider;
    public DrawDNA tubeCollider;

    public TaskManagerM3_1 taskMan;

    private Vector3 plugerStartingPos;
    private Vector3 sampleStartingScale;
    private Vector3 tubeSampleStartingScale;

    [HideInInspector]
    public float length = 0;


    void Start()
    {
        // stores initial position of plunger and initial scales of sample and tube sample
        plugerStartingPos = syringePlunger.transform.localPosition;
        sampleStartingScale = syringeSample.transform.localScale;
        tubeSampleStartingScale = tubeSample.transform.localScale;
    }

    public void Update()
    {
        // Checks if the syringe is colliding with turtle collider and if the extending buttons (A and X) are being pressed
        // if so, calls extend until length > 5.5, then marks task complete
        if (turtleCollider.collided && (OVRInput.Get(OVRInput.RawButton.A) || OVRInput.Get(OVRInput.RawButton.X)))
        {
            Debug.Log("extending");
            extend();
            if (length > 5.5)
            {
                taskMan.MarkTaskCompletion(3);
            }
        }

        // Checks if the syringe is colliding with tube collider and if the retracting buttons (B and Y) are being pressed
        // if so, calls retract until length < 0.5, then marks task complete
        else if (tubeCollider.collided && (OVRInput.Get(OVRInput.RawButton.B) || OVRInput.Get(OVRInput.RawButton.Y)))
        {
            Debug.Log("retracting");
            retract();
            if (length < 0.5)
            {
                taskMan.MarkTaskCompletion(4);
            }
        }
    }

    // Extends the plunger of the syringe until length reaches 6.5
    // Scales DNA sample in syringe to portray the syringe filling up
    private void extend()
    { 
        if (length < 6.5) { length = length + 0.1f; }
        syringePlunger.transform.localPosition = new Vector3(syringePlunger.transform.localPosition.x, plugerStartingPos.y + (length / 100), syringePlunger.transform.localPosition.z);
        syringeSample.transform.localScale = new Vector3(syringeSample.transform.localScale.x, sampleStartingScale.y + length*10, syringeSample.transform.localScale.z);
    }

    // Retracts the plunger of the syringe until the length reaches 0 (back to fully in syringe)
    // Scales DNA sample both inside syringe and test tube to portray syringe emptying and test tube filling up
    private void retract()
    {
        if (length > 0) { length = length - 0.1f; }
        syringePlunger.transform.localPosition = new Vector3(syringePlunger.transform.localPosition.x, -(plugerStartingPos.y - (length / 100)), syringePlunger.transform.localPosition.z);
        syringeSample.transform.localScale = new Vector3(syringeSample.transform.localScale.x, -(sampleStartingScale.y - length * 10), syringeSample.transform.localScale.z);
        tubeSample.transform.localScale = new Vector3(tubeSample.transform.localScale.x, tubeSampleStartingScale.y + (6.5f-length) * 15, tubeSample.transform.localScale.z);
    }
}
