using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

//Script By: Cameron Detig 02/28/2020
//Handles tape measure for sea turtle shell.

public class TapeMeasure : MonoBehaviour
{
	public GameObject tapeMeasure;
    public GameObject leftHand;
    public GameObject rightHand;
	public GameObject placeholder;
    public GameObject headCamera;
	
    public GameObject tapePoint;
    public GameObject tapeEndCol;

    public Image ShellX;
    public Image ShellCheck;
    public Image shellImage;
    public Image horizontalLine;
    public Image verticalLine;
    public Canvas measureCanvas;
    public Text measureText;

    public MeasureColliders frontColl;
    public MeasureColliders backColl;
    public MeasureColliders side1Coll;
    public MeasureColliders side2Coll;

    public SpriteRenderer shellVerticalArrow;
    public SpriteRenderer shellHorizontalArrow;
    public float fadingSpeed = 0.05f;
	
	public Quaternion StartRot;

    public AudioFeedback audiofeedback;
    public Progress prog;

    private Color arrowYellow = new Color(1, 1, 0, 1);
    private Color arrowTransparentYellow = new Color(1, 1, 0, 0);
    private Color arrowGreen = new Color(0, 1, 0, 1);

    bool verticalMeasured = false;
    bool horizontalMeasured = false;
    bool pickedUp = false;
    private bool measurePaused = false;
    private bool shellcolorchanged = false;
    private LineRenderer line;
    private string activeHand;

    public NewTaskManagerM1 taskMan;

    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>(); //Initialize line renderer to not be visible.
        line.SetWidth(0.0F, 0.0F);
        line.SetVertexCount(2);

        shellVerticalArrow.color = arrowTransparentYellow; //Start arrows as transparent.
        shellHorizontalArrow.color = arrowTransparentYellow;
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
        if (frontColl.collided && backColl.collided) //If hands are touching vertical shell colliders, change images.
        {
            if (!verticalMeasured)
            {
                verticalLine.color = new Color(1, 1, 1, 1);
                measureText.text = "82 cm";
                StopAllCoroutines();
                verticalMeasured = true;
                //shellVerticalArrow.color = arrowGreen;
                StartCoroutine(Pause());
                audiofeedback.playGood();
                taskMan.MarkTaskCompletion(9);
            }
        }
        else if (side1Coll.collided && side2Coll.collided) //If hands are touching horizontal shell colliders, change images.
        {
            if (!horizontalMeasured)
            {
                horizontalLine.color = new Color(1, 1, 1, 1);
                measureText.text = "68 cm";
                StopAllCoroutines();
                horizontalMeasured = true;
                shellHorizontalArrow.color = arrowGreen;
                StartCoroutine(Pause());
                audiofeedback.playGood();
                taskMan.MarkTaskCompletion(10);
            }
        }

        //If horizontal and vertical have been measured, turn whole shell green
        if (horizontalLine.color == new Color(1, 1, 1, 1) && verticalLine.color == new Color(1, 1, 1, 1) && !shellcolorchanged)
        {
            verticalLine.color = new Color(1, 1, 1, 0);
            horizontalLine.color = new Color(1, 1, 1, 0);
            shellImage.color = new Color(1, 1, 1, 1);
            ShellX.color = new Color(1, 1, 1, 0);
            ShellCheck.color = new Color(1, 1, 1, 1);
            shellcolorchanged = true;
            prog.TickProgressBar();
        }



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

            //Fades the arrows in and out
            if (verticalMeasured == false)
                if (shellVerticalArrow.color == arrowYellow)
                    StartCoroutine(FadeOut(shellVerticalArrow));
                else if (shellVerticalArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(shellVerticalArrow));
            if (horizontalMeasured == false && verticalMeasured == true)
                if (shellHorizontalArrow.color == arrowYellow)
                    StartCoroutine(FadeOut(shellHorizontalArrow));
                else if (shellHorizontalArrow.color == arrowTransparentYellow)
                    StartCoroutine(FadeIn(shellHorizontalArrow));

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
        }
        else
        {
            line.SetWidth(0.0F, 0.0F); //Clear line if tape measure is not picked up.
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));
            measureText.text = "";
        }
    }

    public void togglePickedUp()
    {
        pickedUp = !pickedUp;
        if (pickedUp) //Determine which hand is holding the tape measure.
        {
			StartCoroutine(GetHand());

            if (verticalMeasured == false)
                shellVerticalArrow.color = arrowYellow;
            if (horizontalMeasured == false && verticalMeasured == true)
                shellHorizontalArrow.color = arrowYellow;
        }
        else
        {
            StopAllCoroutines(); //Stop any fading
            if (verticalMeasured == false)
                shellVerticalArrow.color = arrowTransparentYellow;
            if (horizontalMeasured == false)
                shellHorizontalArrow.color = arrowTransparentYellow;
			
			tapeMeasure.transform.position = placeholder.transform.position;
			tapeMeasure.transform.rotation = StartRot;
            tapeMeasure.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
