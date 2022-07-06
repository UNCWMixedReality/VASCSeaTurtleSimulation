using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalipersDemo : MonoBehaviour
{
    public GameObject calipers;
    public GameObject movingPart;
    public GameObject NewMovingPart;
    public GameObject CalipersLeft;
    public GameObject CalipersRight;
    public BoxCollider TurtleLeft;
    public BoxCollider TurtleRight;

    public Text calipersText;
    public Text calipersTextBig;

    public SpriteRenderer Arrow;
    public float fadingSpeed = 0.05f;

    public Quaternion StartRot;
    //public Vector3 StartPos;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject placeholder;

    public AudioFeedback audiofeedback;

    private Color arrowYellow = new Color(1, 1, 0, 1);
    private Color arrowTransparentYellow = new Color(1, 1, 0, 0);
    private Color arrowGreen = new Color(0, 1, 0, 1);

    private bool pickedUp = false;
    private bool moved = false;
    private Vector3 movingPartStartingPos;
    private bool Measured = false;
    private bool measurePaused = false;
    private string activeHand;

    private float length = 0;

    CaliperCollisionDemo leftCalipScript;
    CaliperCollisionDemo rightCalipScript;

    public NewTaskManagerM1 taskMan;



    void Start()
    {
        movingPartStartingPos = NewMovingPart.transform.localPosition;
        leftCalipScript = CalipersLeft.GetComponent<CaliperCollisionDemo>();
        rightCalipScript = CalipersRight.GetComponent<CaliperCollisionDemo>();

        Arrow.color = arrowTransparentYellow;
    }

    IEnumerator Pause()
    {
        //Pauses the calipers text on the exact measurements to make sure the user knows what they are.
        measurePaused = true;
        calipersTextBig.color = Color.green;
        yield return new WaitForSeconds(2);

        measurePaused = false;
        calipersTextBig.color = Color.white;
    }

    IEnumerator FadeOut(SpriteRenderer obj) //Fades the arrows to transparent
    {
        for (float i = 1f; i >= 0; i -= 0.05f)
        {
            obj.color = new Color(1, 1, 0, i);
            yield return new WaitForSeconds(fadingSpeed);
        }
        obj.color = arrowTransparentYellow;
    }
    IEnumerator FadeIn(SpriteRenderer obj) //Fades the arrows to visible
    {
        for (float i = 0f; i <= 1; i += 0.05f)
        {
            obj.color = new Color(1, 1, 0, i);
            yield return new WaitForSeconds(fadingSpeed);
        }
        obj.color = arrowYellow;
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
        //Debug.Log(activeHand);
    }

    public void FixedUpdate()
    {
        
        if (leftCalipScript.Collided && rightCalipScript.Collided) //tube successfully measured
        {
            StopAllCoroutines();
            if (!Measured)
            {
                Measured = true;
                Arrow.color = arrowGreen;
                calipersTextBig.text = "10cm";
                StartCoroutine(Pause());
                audiofeedback.playGood();
                taskMan.MarkTaskCompletion(3);
            }
        }
        
        if (pickedUp)
        {
            //Fades the arrow in and out
            if (Measured == false)
                if (Arrow.color == arrowYellow)
                    StartCoroutine(FadeOut(Arrow));
                else if (Arrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(Arrow));
            //get control stick movement
            Vector2 touchPad = new Vector2();
            //get button input
            bool extend = false;
            bool retract = false;

            if (activeHand == "right")
            {
                //touchPad = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
                extend = OVRInput.Get(OVRInput.RawButton.B);
                retract = OVRInput.Get(OVRInput.RawButton.A);
            }
            if (activeHand == "left")
            {
                //touchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                extend = OVRInput.Get(OVRInput.RawButton.Y);
                retract = OVRInput.Get(OVRInput.RawButton.X);
            }

            //move based on button inputs
            if (extend)
            {
                if (moved == false)
                {
                    //marks completion of controlling the calipers
                    taskMan.MarkTaskCompletion(2);
                }

                //print("extend");
                if(length < 12)
                {
                    length++;
                }
                //print(length);
                movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (-length/100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
                calipersText.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";
                
                if (measurePaused == false)//Big Text Output
                {
                    calipersTextBig.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";
                }
            }
            if (retract)
            {
                //print("retract");
                if(length > -2)
                {
                    length--;
                }
                //print(length);
                movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (-length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
                calipersText.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";

                if (measurePaused == false)//Big Text Output
                {
                    calipersTextBig.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";
                }

            }

            //move moving part accordingly
            if (touchPad != Vector2.zero)
            {
                //print(touchPad.y);
                //moves calipers based on control stick input
                movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (touchPad.y / 4), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
                calipersText.text = (-(movingPart.transform.localPosition.x * 100) + 4 + 5.2).ToString("F1") + "cm";

                if (measurePaused == false)//Big Text Output
                {
                    calipersTextBig.text = (-(movingPart.transform.localPosition.x * 100) + 4 + 5.2).ToString("F1") + "cm";
                }
            }
        }
    }

    public void togglePickedUp()
    {
        pickedUp = !pickedUp;

        if (pickedUp) //Enable button hints
        {
            //ControllerButtonHints.ShowTextHint(Player.instance.rightHand, TouchPosition, "Click and drag down on trackpad to move calipers.");
            //ControllerButtonHints.ShowButtonHint(Player.instance.rightHand, TouchPosition);
            StartCoroutine(GetHand());

            if (Measured == false)
                Arrow.color = arrowYellow;
        }
        else //Disable button hints
        {
            /*foreach (Hand hand in Player.instance.hands)
            {
                //ControllerButtonHints.HideTextHint(hand, TouchPosition);
                //ControllerButtonHints.HideButtonHint(hand, TouchPosition);
            }*/

            StopAllCoroutines(); //Stop any fading
            if (Measured == false)
                Arrow.color = arrowTransparentYellow;
            calipersTextBig.color = new Color(0, 0, 0, 0);

            calipers.transform.position = placeholder.transform.position;
            calipers.transform.rotation = StartRot;
            calipers.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
