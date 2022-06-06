using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script By: Cameron Detig 02/24/2020
//Handles the interactions for using the calipers to measure the eggs.
//NOT USED

public class CalipersEgg : MonoBehaviour
{
	public GameObject calipers;
    public GameObject movingPart;
    public GameObject CalipersLeft;
    public GameObject CalipersRight;

    public GameObject eggOne;
    public GameObject eggTwo;
    public GameObject eggThree;
    public GameObject eggFour;

    public GameObject placeholder;

    public Text calipersText;
    public Text calipersTextBig;

    public SpriteRenderer eggOneArrow;
    public SpriteRenderer eggTwoArrow;
    public SpriteRenderer eggThreeArrow;
    public SpriteRenderer eggFourArrow;

	public Quaternion StartRot;
	
	public GameObject leftHand;
	public GameObject rightHand;

    public float fadingSpeed = 0.05f;

    private Color arrowYellow = new Color(1, 1, 0, 1);
    private Color arrowTransparentYellow = new Color(1, 1, 0, 0);
    private Color arrowGreen = new Color(0, 1, 0, 1);

    private bool pickedUp = false;
    private Vector3 movingPartStartingPos;
    private bool eggOneMeasured = false;
    private bool eggTwoMeasured = false;
    private bool eggThreeMeasured = false;
    private bool eggFourMeasured = false;
    private bool measurePaused = false;
	private string activeHand;

    CalipersCollisionEgg leftCalipScript;
    CalipersCollisionEgg rightCalipScript;

    EggInspection eggOneScript;
    EggInspection eggTwoScript;
    EggInspection eggThreeScript;
    EggInspection eggFourScript;




    void Start()
    {
        movingPartStartingPos = movingPart.transform.localPosition;
        leftCalipScript = CalipersLeft.GetComponent<CalipersCollisionEgg>();
        rightCalipScript = CalipersRight.GetComponent<CalipersCollisionEgg>();


        eggOneScript = eggOne.GetComponent<EggInspection>();
        eggTwoScript = eggTwo.GetComponent<EggInspection>();
        eggThreeScript = eggThree.GetComponent<EggInspection>();
        eggFourScript = eggFour.GetComponent<EggInspection>();


        eggOneArrow.color = arrowTransparentYellow;
        eggTwoArrow.color = arrowTransparentYellow;
        eggFourArrow.color = arrowTransparentYellow;
        eggThreeArrow.color = arrowTransparentYellow;
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
	
	IEnumerator GetHand(){
		yield return new WaitForSeconds(1.0f);
		if (Vector3.Distance(this.transform.position, leftHand.transform.position) < Vector3.Distance(this.transform.position, rightHand.transform.position))
		{
			activeHand = "left";
		}
		else{
			activeHand = "right";
		}
		Debug.Log(activeHand);
	}

    public void FixedUpdate()
    {
        if (leftCalipScript.eggOne && rightCalipScript.eggOne) //First Egg Measured successfully
        {
           
            StopAllCoroutines();
            eggOneMeasured = true;
            eggOneArrow.color = arrowGreen;
			calipersTextBig.text = "8cm";
			StartCoroutine(Pause());
        }
        else if (leftCalipScript.eggTwo && rightCalipScript.eggTwo) //Second Egg Measured sucessfullly
        {
            StopAllCoroutines();
            eggTwoMeasured = true;
            eggTwoArrow.color = arrowGreen;
			calipersTextBig.text = "8cm";
			StartCoroutine(Pause());
        }
        else if (leftCalipScript.eggThree && rightCalipScript.eggThree) //Third Egg Measured successfully
        {
            StopAllCoroutines();
            eggThreeMeasured = true;
            eggThreeArrow.color = arrowGreen;
 			calipersTextBig.text = "8cm";
			StartCoroutine(Pause());
        }
        else if (leftCalipScript.eggFour && rightCalipScript.eggFour) //Fourth Egg Measured successfully
        {
            StopAllCoroutines();
            eggFourMeasured = true;
            eggFourArrow.color = arrowGreen;
			calipersTextBig.text = "8cm";
			StartCoroutine(Pause());
        }

        if (pickedUp)
        {
            //Fades the arrows in and out
            if (eggOneMeasured == false)
                if (eggOneArrow.color == arrowYellow)
                    StartCoroutine(FadeOut(eggOneArrow));
                else if (eggOneArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(eggOneArrow));
            
            if (eggTwoMeasured == false)
                if (eggTwoArrow.color == arrowYellow)
                   StartCoroutine(FadeOut(eggTwoArrow));
                else if (eggTwoArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(eggTwoArrow));

            if (eggThreeMeasured == false)
                if (eggThreeArrow.color == arrowYellow)
                    StartCoroutine(FadeOut(eggTwoArrow));
                else if (eggTwoArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(eggTwoArrow));

            if (eggFourMeasured == false)
                if (eggFourArrow.color == arrowYellow)
                    StartCoroutine(FadeOut(eggFourArrow));
                else if (eggFourArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(eggFourArrow));
			
			//get control stick movement
			Vector2 touchPad = new Vector2();
			
			if(activeHand == "right")
			{
				touchPad = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
			}
			if(activeHand == "left")
			{
				touchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
			}
			
			
			if(touchPad != Vector2.zero)
			{
				movingPart.transform.localPosition = new Vector3(movingPartStartingPos.x + (touchPad.y / 14), movingPart.transform.localPosition.y, movingPart.transform.localPosition.z);
				calipersText.text = (-(movingPart.transform.localPosition.x * 100) +  14).ToString("F1") + "cm";
				
				if(measurePaused == false) // Big Text Output
				{
					calipersTextBig.text = (-(movingPart.transform.localPosition.x * 100) + 14).ToString("F1") + "cm";
				}
			}
			else{
				//Reset calipers length
				movingPart.transform.localPosition = movingPartStartingPos;
				calipersText.text = "6cm";
			}
        }
    }

   
    public void togglePickedUp()
    {
        pickedUp = !pickedUp;

        if (pickedUp)
        {
			Debug.Log("picked up");
			StartCoroutine(GetHand());
			
            if (eggOneMeasured == false)
                eggOneArrow.color = arrowYellow;
            if (eggTwoMeasured == false)
                eggTwoArrow.color = arrowYellow;
            if (eggThreeMeasured == false)
                eggThreeArrow.color = arrowYellow;
            if (eggFourMeasured == false)
                eggFourArrow.color = arrowYellow;
        }
        else 
        {
            StopAllCoroutines(); //Stop any fading
            if (eggOneMeasured == false)
                eggOneArrow.color = arrowTransparentYellow;
            if (eggTwoMeasured == false)
                eggTwoArrow.color = arrowTransparentYellow;
            if (eggThreeMeasured == false)
                eggThreeArrow.color = arrowTransparentYellow;
            if (eggFourMeasured == false)
                eggFourArrow.color = arrowTransparentYellow;

            calipersTextBig.color = new Color(0, 0, 0, 0);
			
			calipers.transform.position = placeholder.transform.position;
			calipers.transform.rotation = StartRot;
        }
    }

}
