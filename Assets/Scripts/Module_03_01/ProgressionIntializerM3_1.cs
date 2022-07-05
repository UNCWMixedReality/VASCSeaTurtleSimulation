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
		taskMan.taskTimes = new float[9];

		//intialize audiom2_1 variables
		//set current file to first
		audioPlayer.temp = audioPlayer.audioInstructions[0];
		audioPlayer.currentIdx = 0;

		//intialize InstructionUpdater variables
		instrUpdater.instructions = new string[9];
		instrUpdater.instructions[0] = ("Welcome! This is part two of the Nest Relocation Module. Move to the marker to begin.");
		instrUpdater.instructions[1] = ("Your first task is to place the eggs in their new nest. Grab the eggs from the green bucket and place them in the nest");
		instrUpdater.instructions[2] = ("Great job! Next, we need to cover the nest with sand. Grab the shovel to begin.");
		instrUpdater.instructions[3] = ("Use the shovel to move the sand pile on top of the nest.");
		instrUpdater.instructions[4] = ("Good job! Next, we need to cover the nest with a protective cage. Grab the cage to begin.");
		instrUpdater.instructions[5] = ("Move the cage so it covers the nest.");
		instrUpdater.instructions[6] = ("Next, we need to place a sign near the nest. Grab the sign to begin.");
		instrUpdater.instructions[7] = ("Good job! Move the sign to the designated position.");
		instrUpdater.instructions[8] = ("Good job! You have successfully completed part two of the nest relocation module.");

		instrUpdater.current = 0;
		instrUpdater.queueCount = 0;

		//intialize ActivityManager variables
		activityMan.activityCount = 0;
		activityMan.activityTimes = new float[3];

		//intialize progress bar
		progressBar.pArray = new GameObject[5];
		progressBar.pArray[0] = progressBar.P1;
		progressBar.pArray[1] = progressBar.P2;
		progressBar.pArray[2] = progressBar.P3;
		progressBar.pArray[3] = progressBar.P4;
		progressBar.pArray[4] = progressBar.P5;

		//the first task is just entering the scene, so now that setup is done, mark it as complete
		taskMan.MarkTaskCompletion(0);
	}
}
