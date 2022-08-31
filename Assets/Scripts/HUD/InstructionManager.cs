using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Linq;
using Altimit.UI;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
	//the textbox that will display the instructions
	public GameObject TextBox;
	
	//the Canvas gameobject
	public GameObject InstructionPanel;
	//tracks the player's location
	public GameObject playerTracker;
	
	//references the activity tables
	public GameObject TableOne;
	public GameObject TableTwo;
	public GameObject TableThree;

	//tool table calipers reference
	public GameObject calipers;
	//tool table container reference
	public GameObject container;
	//tool table tape measure reference
	public GameObject tapeMeasure;
	//tool table clipboard reference
	public GameObject clipboard;
	//measuring table calipers reference
	public GameObject calipersMsr;
	//measuring table tape reference
	public GameObject tapeMeasureMsr;
	//loadturtle button
	public GameObject loadTurtle;
	//loadtracks button
	public GameObject loadTrack;

	//scripts referenced
	public AudioConvo AC;
	public RoomSwitch RS;
	public Fading FD;

	[HideInInspector]
	//the time at which table one is started
	public float oneStart;

	[HideInInspector]
	//the time at which table two is started
	public float twoStart;

	[HideInInspector]
	//the time at which table three is started
	public float threeStart;

	[HideInInspector]
	//button for toggling panel on and off
	private bool switchActive;

	[HideInInspector]
	//keeps track of whether the panel is currently active
	public bool active;

	[HideInInspector]
	//keeps track of if a task is active
	public bool taskActive;

	//tracks when the calipers are picked up
	private bool caliperStart;
	//tracks when the tape measure is picked up
	private bool tmStart;
	//trakcs when the first instructions are loaded
	private bool playedFirst;
	//tracks the current instruction on display
	private int current;
	//how close the player must be to a waypoint
	public int range;
	//tracks which task the player is on
	private int taskTracker;
	//the text component that will display the instructions
	private Text text;
	//deprecated, still included to show reference to directory
	//private static readonly string textFile = Directory.GetCurrentDirectory() + @"\Assets\Scripts\HUD\InstructionsM1.txt";
	//list of instructions
	private string[] instructions = new string[16];

	public IEnumerator Wait(int wait, int index)
	{
		//change the panel after a given amount of time has passed
		//wait is the seconds to wait
		//index is the index of the instruction in the list that you want to display
		yield return new WaitForSeconds(wait);
		changePanel(index);
		//execute specific code based on which instruction is being displayed
		//activate tool table calipers
		if(index == 3)
        {
			calipers.SetActive(true);
		}
		//activate tool table container
		if (index == 5)
        {
			yield return new WaitForSeconds(7);
			container.transform.GetChild(1).gameObject.SetActive(true);
			container.transform.GetChild(2).gameObject.SetActive(true);
			container.transform.GetChild(3).gameObject.SetActive(true);
		}
		//activate tool table tape measure
		if (index == 6)
        {
			yield return new WaitForSeconds(2);
			tapeMeasure.SetActive(true);
		}
		//activate tool table clipboard
		if (index == 8)
        {
			yield return new WaitForSeconds(2);
			clipboard.transform.GetChild(1).gameObject.SetActive(true);
			clipboard.transform.GetChild(2).gameObject.SetActive(true);
			clipboard.transform.GetChild(3).gameObject.SetActive(true);
		}
		//let user start turtle ID activity by activating load turtle button
		if (index == 12)
        {
			yield return new WaitForSeconds(3);
			loadTurtle.SetActive(true);
        }
		//let user start tracks ID activity by activating load tracks button
		if(index == 14)
        {
			yield return new WaitForSeconds(3);
			loadTrack.SetActive(true);
        }
	}
	
	//call this when fading between tool table and measuring table
	public IEnumerator WaitForFade1(int wait)
    {
		yield return new WaitForSeconds(wait);
		RS.switchRoom();
		FD.Fade(false, true);
		print("Calling fade out");
		calipersMsr.SetActive(true);	//enable calipers/tape for measuring table
		tapeMeasureMsr.SetActive(true);
		yield return new WaitForSeconds(wait);
		changePanel(10);    //load task 1 instructions
		AC.playSound();
		Debug.Log("first task started");
		TableOne.SetActive(false);
		taskTracker = 1;
		oneStart = Time.time;
	}

	//call this when fading between measurnig table and turtle ID table
	public IEnumerator WaitForFade2(int wait)
	{
		yield return new WaitForSeconds(wait);
		RS.switchRoom();
		FD.Fade(false, true);
		print("Calling fade out");
		calipersMsr.SetActive(false);	//disable calipers/tape for measuring table
		tapeMeasureMsr.SetActive(false);
		yield return new WaitForSeconds(wait);
		StartCoroutine(Wait(6, 12));    //load task 2 instructions
		AC.playSound();
		Debug.Log("second task started");
		TableTwo.SetActive(false);
		taskTracker = 2;
		twoStart = Time.time;
	}

	//call this when fading between turtle ID table and tracks ID table
	public IEnumerator WaitForFade3(int wait)
	{
		yield return new WaitForSeconds(wait);
		RS.switchRoom();
		FD.Fade(false, true);
		print("Calling fade out");
		yield return new WaitForSeconds(wait);
		StartCoroutine(Wait(5, 14));    //load task 3 instructions
		AC.playSound();
		Debug.Log("third task started");
		TableThree.SetActive(false);
		taskTracker = 3;
		threeStart = Time.time;
	}

	void Start()
    {
	    calipersMsr.SetActive(false);
	    tapeMeasureMsr.SetActive(false);
		calipers.SetActive(false);
		TableOne.SetActive(false);
		active = true;
		taskActive = false;
		caliperStart = false;
		tmStart = false;
		current = 0;
		taskTracker = 0;
        text = TextBox.GetComponent<Text>();
		instructions[0] = ("Welcome! This is Module 1 of the VASC Sea Turtle Simulation.");
		instructions[1] = ("During this simulation, you will complete three tasks.");
		instructions[2] = ("Before you begin, let's go over the tools you'll be using.");
		instructions[3] = ("First, use your laser pointer and grip button (middle finger) to pick up the calipers on the left side of the table.");
		instructions[4] = ("Good job. Press the A button on that controller to extend and the B button to retract the calipers.");
		instructions[5] = ("Looks like you've got the hang of it. Now it's time to use them. Extend the calipers to measure the container on the table by following the yellow arrow.");
		instructions[6] = ("Nice work! We're done with the calipers for now. Next, pick up the tape measure on the right side of the table.");
		instructions[7] = ("As you can see, when you're holding the tape measure, the tape will automatically follow your other hand.");
		instructions[8] = ("Use the tape measure to measure the clipboard, following the yellow arrow.");
		instructions[9] = ("Great! Now that you know how to use the tools, you're ready to go. Move to the blue waypoint to get started.");
		instructions[10] = ("Your first task is to measure this sea turtle using the calipers on the flippers and the tape measure on the shell.");
		instructions[11] = ("Good job! You finished the first task! Move to the waypoint to continue.");
		instructions[12] = ("This task needs you to identify the sea turtles that appear on the table. Use your laser pointer to click the Load Turtle button to begin.");
		instructions[13] = ("Nice job! You finished the second task! Move to the waypoint to continue.");
		instructions[14] = ("This task needs you to identify the sets of turtle tracks that appear on the table. Click the Load Tracks button to begin.");
		instructions[15] = ("Excellent work! You finished the third task! That's all for this Module.");
		text.text = instructions[current];
		Debug.Log(text.text);

		//begin with the welcome bits
		//play audio clip
		StartCoroutine(Wait(5, 1));
		StartCoroutine(Wait(10, 2));
		StartCoroutine(Wait(14, 3));		//this loads first instructions for tool table
	}

	void Update()
    {
		//this just makes sure that the first instruction plays, as audio can be a bit finicky when loading in from the main menu
        if (!playedFirst)
        {
			AC.playSound();
				playedFirst = true;
			taskActive = true;
		}
		
		//start first activity is player is in proximity
		if (Vector3.Distance(playerTracker.transform.position, TableOne.transform.position) < range && !taskActive && taskTracker == 0)
		{
			FD.Fade(true, false);
			taskActive = true;
			StartCoroutine(WaitForFade1(2));

		}
		//start second activity is player is in proximity and first activity has been completed
		if (Vector3.Distance(playerTracker.transform.position, TableTwo.transform.position) < range && !taskActive && taskTracker == 1)
		{
			FD.Fade(true, false);
			taskActive = true;
			StartCoroutine(WaitForFade2(2));
		}
		//start third activity if player is in proximity and second activity has been completed
		if (Vector3.Distance(playerTracker.transform.position, TableThree.transform.position) < range && !taskActive && taskTracker == 2)
		{
			FD.Fade(true, false);
			taskActive = true;
			StartCoroutine(WaitForFade3(2));
		}
	}

	
	//toggle the panel on and off
	public void togglePanel(){
		active = !active;
		if(active){
			InstructionPanel.SetActive(true);
		}
		else{
			InstructionPanel.SetActive(false);
		}
	}
	
	//change to the next or previous panel
	public void changePanel(int value){
		text.text = instructions[value];
		current = value;
	}

	//called when tool table calipers are picked up for the first time
	public void pickUpCal()
    {
		if(caliperStart == false)
        {
			caliperStart = true;
			changePanel(4);
			AC.playSound();
        }
    }

	//called when tool table tape measure is picked up for the first time
	public void pickUpTM()
    {
		if(tmStart == false)
        {
			tmStart = true;
			changePanel(7);
			AC.playSound();
			StartCoroutine(Wait(8, 8));
		}
	}
}
