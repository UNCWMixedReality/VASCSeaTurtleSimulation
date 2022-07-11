using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    public GameObject syringe;
    public GameObject syringeBody;
    public GameObject syringePlunger;
    private Vector3 plugerStartingPos;

    private bool pickedUp = false;

    public DrawDNA turtleCollider;
    public DepositDNA tubeCollider;

    public TaskManagerM3_1 taskMan;



    public float length = 0;


    void Start()
    {
        plugerStartingPos = syringePlunger.transform.localPosition;
    }

    public void Update()
    {
        bool extended = false;
        bool retracted = false;

        if (turtleCollider.collided && (OVRInput.Get(OVRInput.RawButton.A) || OVRInput.Get(OVRInput.RawButton.X)))
        {
            Debug.Log("extending");
            extend();
            if (length == 6.5)
            {
                taskMan.MarkTaskCompletion(1);
            }
        }
        else if (tubeCollider.collided && (OVRInput.Get(OVRInput.RawButton.B) || OVRInput.Get(OVRInput.RawButton.Y)))
        {
            Debug.Log("retracting");
            retract();
            if (length == 0)
            {
                taskMan.MarkTaskCompletion(2);
            }
        }
    }

    private void extend()
    {
        if (length < 6.5) { length = length + 0.1f; }
        syringePlunger.transform.localPosition = new Vector3(syringePlunger.transform.localPosition.x, plugerStartingPos.y + (length / 100), syringePlunger.transform.localPosition.z);
    }
    private void retract()
    {
        if (length > 0) { length = length - 0.1f; }
        syringePlunger.transform.localPosition = new Vector3(syringePlunger.transform.localPosition.x, -(plugerStartingPos.y - (length / 100)), syringePlunger.transform.localPosition.z);
    }
}
