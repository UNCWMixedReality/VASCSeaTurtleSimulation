
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManagerM2 : MonoBehaviour
{
	//the textbox that will display the instructions
	public GameObject TextBox;

	//the Canvas gameobject
	public GameObject InstructionPanel;
	//the panel that displays the sorted eggs
	

	//tracks the player's location
	public GameObject playerTracker;

	//references to the waypoints
	
	public GameObject MarkerReplace;
	public GameObject MarkerDig;
	public GameObject MarkerCover;
	public GameObject MarkerSign;

	//the time at which activity one starts
	[HideInInspector]
	public float oneStart;

	//this time at which activity two starts
	[HideInInspector]
	public float twoStart;

	//this time at whcih activity three starts
	[HideInInspector]
	public float threeStart;

	//this time at which activity four starts
	[HideInInspector]
	public float fourStart;

	//this time at which activity five starts
	[HideInInspector]
	public float fiveStart;

	//this time at which activity six starts
	[HideInInspector]
	public float sixStart;

	//scripts referenced
	public AudioConvoM2 AC;
	public InteractableToggle IT;
	//public Gloves gloves;
	public VideoPlayer VP;

	//tracks when the first instructions are loaded
	private bool playedFirst;
	//keeps track of whether a task is active
	public bool taskActive;
	//keeps trakc of the current instruction being displayed
	private int current;
	//how close the player must be to a waypoint
	public int range;
	//tracks which task the player is on
	private int taskTracker;
	//the text component that will display the instructions
	private Text text;
	//deprecated, still included to show reference to directory
	//private static readonly string textFile = Directory.GetCurrentDirectory() + @"\Assets\Scripts\HUD\InstructionsM2.txt";
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
		//activate gloves
		if (index == 2)
		{
			yield return new WaitForSeconds(4);
			
			//gloves.pickupL.SetActive(true);
			//gloves.pickupR.SetActive(true);
        }
	}

	void Start()
    {
		
		
		MarkerReplace.SetActive(true);
		MarkerDig.SetActive(false);
		MarkerCover.SetActive(false);
		MarkerSign.SetActive(false);
		taskActive = false;
		current = 0;
		taskTracker = 2;
        text = TextBox.GetComponent<Text>();
		instructions[0] = ("Welcome! This is Module 2 of the VASC Sea Turtle Simlation.");
		instructions[1] = ("During this simulation, you will move sea turtle eggs from an endangered nest to a safer one.");//
		instructions[2] = ("When you're ready, go ahead and move to the first marker to get started.");
		instructions[3] = ("Your first task is to dig up this turtle nest. Grab the gloves to put them on and then dig in the sand until you can see the eggs.");//
		instructions[4] = ("Good job! You're ready to move on to the next task.");
		instructions[5] = ("Next, these eggs need to be carefully moved. Some are cracked, though, and will need to be separated from the good eggs.");//
		instructions[6] = ("Examine the eggs, and put them in the red bucket if they're cracked, or the green bucket if they aren't.");//
		instructions[7] = ("Good job! Follow the boardwalk to the next nest further down the beach");//
		instructions[8] = ("Here you need to place the good eggs into their new nest. Take the eggs from the bucket and place them in the new nest. The light blue sphere show where the eggs should go.");//
		instructions[9] = ("Good job! Now the nest needs to be covered up and protected so predators don't dig up the nest and eat the eggs.");//
		instructions[10] = ("Start by using the shovel to move some sand over the eggs.");
		instructions[11] = ("Good job! Now you need to place a protective cage over the nest.");
		instructions[12] = ("Pick up the wireframe cage and place it on top of the nest.");
		instructions[13] = ("Good job! All that's left is to mark the nest's location with a sign so people will know to be careful around it.");//
		instructions[14] = ("Move the sign over to the nest to mark its location.");
		instructions[15] = ("Good job! That's it for this Module.");
		text.text = instructions[current];

		//begin with welcome bits
		//play audio clips
		StartCoroutine(Wait(3, 1));
		StartCoroutine(Wait(7, 2));
    }

    void Update()
    {
		//this just make sure the first instruction plays, as audio can be finicky when loading from another scene
		if (!playedFirst)
		{
			AC.playSound();
			playedFirst = true;
		}

		
		//start third activity if player is in range
		if(Vector3.Distance(playerTracker.transform.position, MarkerReplace.transform.position) < range && !taskActive && taskTracker == 2){
			changePanel(8); //load task 3 instructions
			AC.playSound();
			VP.nextVidPlay();
			Debug.Log("third task started");
			IT.toggle(1, 9);
			IT.toggle(2, 9);
			MarkerReplace.SetActive(false);
			taskTracker++;
			taskActive = true;
			threeStart = Time.time;
		}
		//start fourth activity if player is in range
		if(Vector3.Distance(playerTracker.transform.position, MarkerDig.transform.position) < range && !taskActive && taskTracker == 3){
			changePanel(10);
			AC.playSound();
			VP.nextVidPlay();
			IT.toggle(0, 9);
			MarkerDig.SetActive(false);
			taskTracker++;
			taskActive = true;
			fourStart = Time.time;
		}
		//start fifth activity if player is in range
		if(Vector3.Distance(playerTracker.transform.position, MarkerCover.transform.position) < range && !taskActive && taskTracker == 4){
			changePanel(12);
			AC.playSound();
			IT.toggle(3, 9);
			MarkerCover.SetActive(false);
			taskTracker++;
			taskActive = true;
			fiveStart = Time.time;
		}
		//start sixth activity if player is in range
		if(Vector3.Distance(playerTracker.transform.position, MarkerSign.transform.position) < range && !taskActive && taskTracker == 5)
        {
			changePanel(14);
			AC.playSound();
			IT.toggle(4, 9);
			MarkerSign.SetActive(false);
			taskTracker++;
			taskActive = true;
			sixStart = Time.time;
        }
    }
	
	//change to the next or previous panel
	public void changePanel(int value){
		text.text = instructions[value];
		current = value;
		if(value == 6)
        {
			VP.nextVidPlay();
        }
	}
}
