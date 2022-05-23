using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IMTutorial : MonoBehaviour
{
    //scripts referenced
    public AudioConvoTutorial ACT;

    //tracks the player
    public GameObject playerTracker;
    //second room location
    public GameObject twoPlaceholder;
    //third room location
    public GameObject threePlaceholder;
    //the textbox that will display the instructions
    public GameObject TextBox;
    
    //how close the player must be to new rooms
    public int range;

    //second room started
    private bool twoStarted;
    //third room started
    private bool threeStarted;

    //the text component that will be updated
    private Text text;

    //tracks the current instruction
    private int current;

    //list of instuction strings
    private string[] instructions = new string[7];


    void Start()
    {
        //set current instructions to the head of the list
        current = 0;
        //set booleans
        twoStarted = false;
        threeStarted = false;
        //get reference to the text object
        text = TextBox.GetComponent<Text>();
        //set up instructions list
        instructions[0] = ("Welcome to the VASC tutorial.");
        instructions[1] = ("You can teleport around the environment. To teleport, point your laser at the ground where you want to teleport to and press the trigger (see picture).");
        instructions[2] = ("To turn to the sides quickly, use the joysticks. Press left to turn left, and right to turn right.");
        instructions[3] = ("Go ahead and go through the door to the second room");
        instructions[4] = ("To pick up items, you'll use the grip button on your controller. You can simply point your laser at an object and press the button - you do not need to be close by.");
        instructions[5] = ("Good job, now go through the final door to the next room.");
        instructions[6] = ("To interact with buttons on the screen, point the laser at the screen and press the grip button. Click the button to complete the tutorial");
        //update displayed text
        text.text = instructions[current];
        Debug.Log(text.text);
        //update pannel
        StartCoroutine(Wait(3));
    }

    // Update is called once per frame
    void Update()
    {
        //do thing for scond room
        if (Vector3.Distance(playerTracker.transform.position, twoPlaceholder.transform.position) < range && !twoStarted)
        {
            twoPlaceholder.SetActive(false);
            twoStarted = true;
            changePanel(4);
            ACT.playSound();
        }

        //do thing for third room
        if(Vector3.Distance(playerTracker.transform.position, threePlaceholder.transform.position) < range && !threeStarted)
        {
            threePlaceholder.SetActive(false);
            threeStarted = true;
            changePanel(6);
            ACT.playSound();
        }
    }

    public IEnumerator Wait(int wait)
    {
        //change the panel after a given amount of time has passed
        yield return new WaitForSeconds(wait);
        ACT.playSound();
        yield return new WaitForSeconds(2);
        ACT.playSound();
        changePanel(1);
    }
    
    //change to the next or previous panel
    public void changePanel(int value)
    {
        text.text = instructions[value];
        current = value;
    }

}
