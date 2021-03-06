using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script By: Cameron Detig 02/24/2020
//Handles the interactions for using the calipers to measure the turtle.

public class Calipers : MonoBehaviour
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
    public Image upperImage;
    public Image lowerImage;
    public Image frontFinsX;
    public Image frontFinsCheck;
    public Image backFinsX;
    public Image backFinsCheck;

    public SpriteRenderer frontFinArrow;
    public SpriteRenderer backFinArrow;
    public float fadingSpeed = 0.05f;
	
	public Quaternion StartRot;
	//public Vector3 StartPos;
	
    public GameObject leftHand;
    public GameObject rightHand;
	public GameObject placeholder;

    public AudioFeedback audiofeedback;
    public Progress prog;

    private Color arrowYellow = new Color(1, 1, 0, 1);
    private Color arrowTransparentYellow = new Color(1, 1, 0, 0);
    private Color arrowGreen = new Color(0, 1, 0, 1);

    private bool pickedUp = false;
    private Vector3 movingPartStartingPos;
    private bool frontMeasured = false;
    private bool backMeasured = false;
    private bool measurePaused = false;
	private string activeHand;

    private float length = 0;

    CalipersCollision leftCalipScript;
    CalipersCollision rightCalipScript;


    void Start()
    {
        movingPartStartingPos = NewMovingPart.transform.localPosition;
        leftCalipScript = CalipersLeft.GetComponent<CalipersCollision>();
        rightCalipScript = CalipersRight.GetComponent<CalipersCollision>();

        frontFinArrow.color = arrowTransparentYellow;
        backFinArrow.color = arrowTransparentYellow;
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
			Debug.Log(activeHand);
	}
	
    public void FixedUpdate()
    {
        if (leftCalipScript.frontCollided && rightCalipScript.frontCollided) //Front fins successfully measured
        {
            if (!frontMeasured)
            {
                upperImage.color = new Color(1, 1, 1, 1);
                frontFinsCheck.color = new Color(1, 1, 1, 1);
                frontFinsX.color = new Color(1, 1, 1, 0);
                audiofeedback.playGood();
                StopAllCoroutines();
                frontMeasured = true;
                frontFinArrow.color = arrowGreen;
                calipersTextBig.text = "14cm";
                StartCoroutine(Pause());
                prog.TickProgressBar();
            }
        }
        else if (leftCalipScript.backCollided && rightCalipScript.backCollided) //Back fins successfully measured
        {
            if (!backMeasured)
            {
                lowerImage.color = new Color(1, 1, 1, 1);
                backFinsX.color = new Color(1, 1, 1, 0);
                backFinsCheck.color = new Color(1, 1, 1, 1);
                audiofeedback.playGood();
                StopAllCoroutines();
                backMeasured = true;
                backFinArrow.color = arrowGreen;
                calipersTextBig.text = "13.5cm";
                StartCoroutine(Pause());
                prog.TickProgressBar();
            }
        }

        if (pickedUp)
        {
            //Fades the arrows in and out
            if (frontMeasured == false)
                if (frontFinArrow.color == arrowYellow)
                    StartCoroutine(FadeOut(frontFinArrow));
                else if (frontFinArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(frontFinArrow));
            if (backMeasured == false)
                if (backFinArrow.color == arrowYellow)
                    StartCoroutine(FadeOut(backFinArrow));
                else if (backFinArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(backFinArrow));
			//get control stick movement
			Vector2 touchPad = new Vector2();
            //get button input
            bool extend = false;
            bool retract = false;
            //get button input based on current hand
            if (activeHand == "right"){
				touchPad = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
                extend = OVRInput.Get(OVRInput.RawButton.B);
                retract = OVRInput.Get(OVRInput.RawButton.A);
            }
            if (activeHand == "left"){
				touchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                extend = OVRInput.Get(OVRInput.RawButton.Y);
                retract = OVRInput.Get(OVRInput.RawButton.X);
            }
            //move based on button inputs
            if (extend)
            {
                //print("extend");
                if (length < 12)
                {
                    length++;
                }
                //print(length);
                movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (-length / 100), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
                calipersText.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";

                if (measurePaused == false)//Big Text Output
                {
                    calipersTextBig.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";
                }
            }
            if (retract)
            {
                //print("retract");
                if (length > -2)
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
				//moves calipers based on control stick input]
				movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (touchPad.y / 14), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
				calipersText.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";
				
				if(measurePaused == false)//Big Text Output
				{
					calipersTextBig.text = (14-(-(movingPart.transform.localPosition.x * 100) + 4 + 5.2)).ToString("F1") + "cm";
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

            if (frontMeasured == false)
                frontFinArrow.color = arrowYellow;
            if (backMeasured == false)
                backFinArrow.color = arrowYellow;
        }
        else //Disable button hints
        {
            /*foreach (Hand hand in Player.instance.hands)
            {
                //ControllerButtonHints.HideTextHint(hand, TouchPosition);
                //ControllerButtonHints.HideButtonHint(hand, TouchPosition);
            }*/

            StopAllCoroutines(); //Stop any fading
            if (frontMeasured == false)
                frontFinArrow.color = arrowTransparentYellow;
            if (backMeasured == false)
                backFinArrow.color = arrowTransparentYellow;

            calipersTextBig.color = new Color(0, 0, 0, 0);
			
			calipers.transform.position = placeholder.transform.position;
			calipers.transform.rotation = StartRot;
            calipers.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

}
