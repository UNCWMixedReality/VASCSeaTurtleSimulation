using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CalipSize : MonoBehaviour
{
    // progression scripts
    public NewTaskManagerM1 taskMan;
    public int taskNum;

    // text boxs to update
    public Text displayText;
    public Text floatingText;
    
    // reference transofrm of the main and moving parts of the caliper to measure distance
    public Transform leftMeasure;
    public Transform rightMeasure;

    // Caliper Colliders to determine if caliper is colliding with measurable object
    public CaliperMeasure LCollider;
    public CaliperMeasure RCollider;

    // calculation variable 
    private float length;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        // calcualte length ebtween colliders
        length = (Vector3.Distance(leftMeasure.position, rightMeasure.position)*100);

        // Update caliper text
        displayText.text = ((int)length).ToString() + "cm";
        floatingText.text = ((int)length).ToString() + "cm";

        if (LCollider.collided && RCollider.collided)
        {
            Debug.Log("Caliper Measured");
            taskMan.MarkTaskCompletion(taskNum);
        }

    }
}
