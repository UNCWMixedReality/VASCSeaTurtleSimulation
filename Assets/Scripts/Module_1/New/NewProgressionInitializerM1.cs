using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProgressionInitializerM1 : MonoBehaviour
{

	/*
	 * This whole script is really just a start method
	 * 
	 * When the scene is loaded, this performs all the set up for the progression system
	 * and runs the introductory dialogue/instructions. 
	 */


	//references to the scripts that we need to intialize values for
	public NewInstructionUpdaterM1 instrUpdater;
	public NewActivityManagerM1 activityMan;
	public NewTaskManagerM1 taskMan;
	public NewInstructionAudioM1 audioPlayer;
	public Progress progressBar;

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
		taskMan.taskTimes = new float[21];

		//intialize audiom2_1 variables
		audioPlayer.currentIdx = 0;

		//intialize InstructionUpdater variables
		instrUpdater.instructions = new string[21];
		instrUpdater.instructions[0] = ("Welcome! This is module 1 of the VASC Sea Turtle Simulation. Let's get familiar with the tools you will be using in this module, grab the calipers on the table to start.");
		instrUpdater.instructions[1] = ("To use the calipers, press the A button to extend or the B button to retract. Try it out!");
		instrUpdater.instructions[2] = ("Now, use the calipers to measure the jar on the table.");
		instrUpdater.instructions[3] = ("Great job, we will also be using a tape measurer in this module. Grab the tape measurer on the table.");
		instrUpdater.instructions[4] = ("You'll notice that the tape measurer automatically measures the distance between your two hands. Try measuring the clipboard on the table with the tape measurer.");
		instrUpdater.instructions[5] = ("Great work, you've successfully used the calipers and tape measurer! Move to the blue waypoint to begin your first task.");
		instrUpdater.instructions[6] = ("a");
		instrUpdater.instructions[7] = ("Good job! Move the sign to the designated position.");
		instrUpdater.instructions[8] = ("Good job! You have successfully completed part two of the nest relocation module.");

		instrUpdater.current = 0;
		instrUpdater.queueCount = 0;

		//intialize ActivityManager variables
		activityMan.activityCount = 0;
		activityMan.activityTimes = new float[3];

		//intialize progress bar
		progressBar.pArray = new GameObject[11];
		progressBar.pArray[0] = progressBar.P1;
		progressBar.pArray[1] = progressBar.P2;
		progressBar.pArray[2] = progressBar.P3;
		progressBar.pArray[3] = progressBar.P4;
		progressBar.pArray[4] = progressBar.P5;

		//the first task is just entering the scene, so now that setup is done, mark it as complete
		taskMan.MarkTaskCompletion(0);
	}

}
