using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionIntializer : MonoBehaviour
{
	// Start is called before the first frame update
	/*
	 * This whole script is really just a start method
	 * 
	 * When the scene is loaded, this performs all the set up for the progression system
	 * and plays/sets the introductory dialogue/instructions. 
	 */

	public InstructionUpdater instrUpdater;
	public New_Activity_Manager activityMan;
	public TaskManager taskMan;
	public AudioM2_1 audioPlayer;

    void Start()
    {
		/*
		 * This function is going to initialize all the variables and stuff we use for progression across multiple scripts
		 * It is done this way because certain set up conditions need to be met to continue setting up the scene. Putting it all in one
		 * functions lets us control the order the variables are intialized and stuff and makes everything work correctly.
		 * 
		 * In addition, this makes it easier for future changes to be made since all the set up is done in one location and not spread across multiple files.
		 * 
		 * Future change: revert all these variables to private in their respective classes and define functions for getting/setting them. This is good practice
		 * However, at the moment I am just concerned with getting this working.
		 */


		//intialize TaskManager variables
		taskMan.taskCount = 0;
		taskMan.taskTimes = new float[5];

		//intialize audiom2_1 variables
		////set current file to first
		audioPlayer.temp = audioPlayer.enteredScene;
		////Enqueue audio files
		audioPlayer.audioInstructions.Enqueue(audioPlayer.enteredScene);
		audioPlayer.audioInstructions.Enqueue(audioPlayer.movedToNest);
		audioPlayer.audioInstructions.Enqueue(audioPlayer.putOnGloves);
		audioPlayer.audioInstructions.Enqueue(audioPlayer.dugNest);
		audioPlayer.audioInstructions.Enqueue(audioPlayer.sortedEggs);

		//intialize InstructionUpdater variables
		instrUpdater.instructions[0] = ("Welcome! This is part one of the Nest Relocation Module. Move to the marker to begin.");
		instrUpdater.instructions[1] = ("Your first task is to dig up this turtle nest. First, grab the blue gloves to put them on.");
		instrUpdater.instructions[2] = ("Great job! Now, carefully dig in the sand using your hands until you can see the eggs.");
		instrUpdater.instructions[3] = ("Good job! Next, these eggs need to be carefully moved. Examine the eggs, and put them in the red bucket if they're cracked, or the green bucket if they aren't.");
		instrUpdater.instructions[4] = ("Good job! You have completed VASC module 2 part 1.");

		instrUpdater.current = 0;

		//intialize ActivityManager variables
		activityMan.activityCount = 0;
		activityMan.activityTimes = new float[2];

		//the first task is just entering the scene lol
		taskMan.MarkTaskCompletion();
	}
}
