using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UltimateXR.Core;
using UltimateXR.Manipulation;
using UltimateXR.Avatar;


public class CalipSize : MonoBehaviour
{
    // progression scripts
    public NewTaskManagerM1 taskMan;
    public int taskNum;
    public GameObject player;

    // caliper parts used by this script
    public GameObject calipers;
    public GameObject movingPart;
    private float caliperOriginalOrientation;

    // text boxs to update
    public GameObject displayTextBox;
    public GameObject floatingTextBox;
    private Text displayText;
    private Text floatingText;
    
    // reference transofrm of the main and moving parts of the caliper to measure distance
    public Transform leftMeasure;
    public Transform rightMeasure;

    // Caliper Colliders to determine if caliper is colliding with measurable object
    public CaliperMeasure LCollider;
    public CaliperMeasure RCollider;

    // calculation variable 
    private float length;



    public void Start()
    {
        displayText = displayTextBox.GetComponent<Text>();
        floatingText = floatingTextBox.GetComponent<Text>();
        caliperOriginalOrientation = calipers.transform.localScale.x;
    }

    IEnumerator CountdownResetCaliper()
    {
        yield return new WaitForSeconds(3);
        movingPart.transform.localPosition = new Vector3(0.14f, 0, 0);
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        // calcualte length ebtween colliders
        length = (Vector3.Distance(leftMeasure.position, rightMeasure.position)*100);

        // Update caliper text
        displayText.text = ((int)length).ToString() + "cm";
        floatingText.text = ((int)length).ToString() + "cm";

        if (LCollider is not null && RCollider is not null && taskMan is not null)
        {
            if (LCollider.collided && RCollider.collided)
            {
                Debug.Log("Caliper Measured");
                taskMan.MarkTaskCompletion(taskNum);
            }
        }

    }


    public void startCountdown() { StartCoroutine(CountdownResetCaliper());}
}
