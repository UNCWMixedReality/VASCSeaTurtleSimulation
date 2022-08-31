using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalipersTut : MonoBehaviour
{

    public GameObject calipers;
    public GameObject movingPart;
    public GameObject NewMovingPart;

    public Text calipersText;
    public Text calipersTextBig;

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject placeholder;

    private bool pickedUp = false;
    private Vector3 movingPartStartingPos;
    private string activeHand;

    private float length = 0;



    void Start()
    {
        movingPartStartingPos = NewMovingPart.transform.localPosition;
    }

 
    IEnumerator GetHand()
    {
        yield return new WaitForSeconds(1.0f);
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
            
            //get control stick movement
            Vector2 touchPad = new Vector2();
            //get button input
            bool extend = false;
            bool retract = false;

            if (activeHand == "right")
            {
             //   touchPad = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
                extend = OVRInput.Get(OVRInput.RawButton.B);
                retract = OVRInput.Get(OVRInput.RawButton.A);
            }
            if (activeHand == "left")
            {
             //   touchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                extend = OVRInput.Get(OVRInput.RawButton.Y);
                retract = OVRInput.Get(OVRInput.RawButton.X);
            }

            //move based on button inputs
            if (extend)
            {
                print("extend");
                if (length < 12)
                {
                    length++;
                }
                print(length);
                movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (-length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
                calipersText.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 7.3)).ToString("F1") + "cm";

                calipersTextBig.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 7.3)).ToString("F1") + "cm";
            }
            if (retract)
            {
                print("retract");
                if (length > -2)
                {
                    length--;
                }
                print(length);
                movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (-length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
                calipersText.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 7.3)).ToString("F1") + "cm";

                calipersTextBig.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 7.3)).ToString("F1") + "cm";
            }

            //move moving part accordingly
            if (touchPad != Vector2.zero)
            {
                print(touchPad.y);
                //moves calipers based on control stick input
                movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (touchPad.y / 14), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
                calipersText.text = (-(movingPart.transform.localPosition.x * 100) + 14).ToString("F1") + "cm";

                calipersTextBig.text = (-(movingPart.transform.localPosition.x * 100) + 14).ToString("F1") + "cm";
            }
        }
    }

    public void togglePickedUp()
    {
        pickedUp = !pickedUp;

        if (pickedUp) //Enable button hints
        {
            StartCoroutine(GetHand());
        }
        else
        {
            calipers.transform.position = placeholder.transform.position;
            calipers.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
