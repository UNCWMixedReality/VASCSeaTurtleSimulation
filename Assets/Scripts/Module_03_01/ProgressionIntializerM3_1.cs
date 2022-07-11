using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionIntializerM3_1 : MonoBehaviour
{
	/*
	 * This whole script is really just a start method
	 * 
	 * When the scene is loaded, this performs all the set up for the progression system
	 * and plays/sets the introductory dialogue/instructions. 
	 */


	//references to the scripts that we need to intialize values for
	public InstructionUpdaterM3_1 instrUpdater;
	public NewActivityManM3_1 activityMan;
	public TaskManagerM3_1 taskMan;
	public AudioM3_1 audioPlayer;
	public ProgressM3 progressBar;

	void Start()
	{
		/*
		 * This function is going to initialize all the variables and stuff we use for progression across multiple scripts
		 * It is done this way because certain set up conditions need to be met to continue setting up the scene. Putting it all in one
		 * functions lets us control the order the variables are intialized and stuff and makes everything work correctly.
		 * 
		 * In addition, this makes it easier for future changes to be made since all the set up is done in one location and not spread across multiple files.
		 */


		//intialize TaskManager variables
		taskMan.taskCount = 0;
		taskMan.taskTimes = new float[13];

		//intialize audiom2_1 variables
		//set current file to first
		audioPlayer.temp = audioPlayer.audioInstructions[0];
		audioPlayer.currentIdx = 0;

		//intialize InstructionUpdater variables
		instrUpdater.instructions = new string[12];
		instrUpdater.instructions[0] = ("Welcome! This is the Turtle Encounter Module. Move to the marker to begin.");
		instrUpdater.instructions[1] = ("Your first task is to take a DNA sample from the turtle. Pick up the syringe from the table to begin.");
		instrUpdater.instructions[2] = ("Draw a DNA sample from the turtle's head or front flippers using the A button.");
		instrUpdater.instructions[3] = ("Great job! Next, deposit the DNA sample into the test tube on the table using the B button.");
		instrUpdater.instructions[4] = ("Good job! Now it is time to clean the turtle's shell. Grab the cloth from the table to begin.");
		instrUpdater.instructions[5] = ("Wipe the turtle's shell until it is clean.");
		instrUpdater.instructions[6] = ("Great job! Next, we need to put the GPS Tracker in place. Pick up the GPS to begin.");
		instrUpdater.instructions[7] = ("Place the GPS on the upper half of the turtles shell.");
		instrUpdater.instructions[8] = ("Good job! Next, we need to secure the GPS tracker using the paste. Grab the shovel to begin.");
		instrUpdater.instructions[9] = ("Put the paste on the tracker.");
		instrUpdater.instructions[10] = ("Good job! You have successfully completed the turtle encounter module.");




		instrUpdater.current = 0;
		instrUpdater.queueCount = 0;

		//intialize ActivityManager variables
		activityMan.activityCount = 0;
		activityMan.activityTimes = new float[7];

		//intialize progress bar
		progressBar.pArray = new GameObject[7];
		progressBar.pArray[0] = progressBar.P1;
		progressBar.pArray[1] = progressBar.P2;
		progressBar.pArray[2] = progressBar.P3;
		progressBar.pArray[3] = progressBar.P4;
		progressBar.pArray[4] = progressBar.P5;
		progressBar.pArray[5] = progressBar.P6;
		progressBar.pArray[6] = progressBar.P7;

		//the first task is just entering the scene, so now that setup is done, mark it as complete
		taskMan.MarkTaskCompletion(0);
	}
}
