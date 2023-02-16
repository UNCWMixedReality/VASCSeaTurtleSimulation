using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionIntializer : MonoBehaviour
{
	/*
	 * This whole script is really just a start method
	 * 
	 * When the scene is loaded, this performs all the set up for the progression system
	 * and plays/sets the introductory dialogue/instructions. 
	 */


	//references to the scripts that we need to intialize values for
	public InstructionUpdater instrUpdater;
	public New_Activity_Manager activityMan;
	public TaskManager taskMan;
	public AudioM2 audioPlayer;
	public ProgressM2 progressBar;

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
		taskMan.taskTimes = new float[5];

		//intialize audiom2_1 variables
		//set current file to first
		audioPlayer.temp = audioPlayer.audioInstructions[0];
		audioPlayer.currentIdx = 0;

		//intialize InstructionUpdater variables
		instrUpdater.instructions = new string[5];
		instrUpdater.instructions[0] = ("Welcome! This is part one of the Nest Relocation Module. Move to the marker to begin.");
		instrUpdater.instructions[1] = ("Your first task is to dig up this turtle nest. First, grab the blue gloves to put them on.");
		instrUpdater.instructions[2] = ("Great job! Now, carefully dig in the sand using your hands until you can see the eggs.");
		instrUpdater.instructions[3] = ("Good job! Next, these eggs need to be carefully moved. Examine the eggs, and put them in the red bucket if they're cracked, or the green bucket if they aren't.");
		instrUpdater.instructions[4] = ("Good job! You have completed VASC module 2 part 1.");
		instrUpdater.current = 0;
		instrUpdater.queueCount = 0;

		//intialize ActivityManager variables
		activityMan.activityCount = 0;
		activityMan.activityTimes = new float[2];

		//intialize progress bar
		progressBar.pArray = new GameObject[5];
		progressBar.pArray[0] = progressBar.P1;
		progressBar.pArray[1] = progressBar.P2;
		progressBar.pArray[2] = progressBar.P3;
		progressBar.pArray[3] = progressBar.P4;
		progressBar.pArray[4] = progressBar.P5;

		//the first task is just entering the scene, so now that setup is done, mark it as complete
		taskMan.MarkTaskCompletion();
	}
}
