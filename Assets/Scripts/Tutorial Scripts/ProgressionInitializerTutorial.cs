using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionInitializerTutorial : MonoBehaviour
{
	public InstructionUpdaterTutorial instrUpdater;
	public ActivityManagerTutorial activityMan;
	public TaskManagerTutorial taskMan;
	public AudioTutorial audioPlayer;

	// Start is called before the first frame update
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
		taskMan.taskTimes = new float[10];

		//intialize audiom2_1 variables
		//set current file to first
		//audioPlayer.temp = audioPlayer.audioInstructions[0];
		//audioPlayer.currentIdx = 0;

		//intialize InstructionUpdater variables
		instrUpdater.instructions = new string[11];
		instrUpdater.instructions[0] = ("Welcome to the VASC tutorial. Let's get you familiar with your controller. To begin, hit either the A or X buttons.");
		instrUpdater.instructions[1] = ("Good! Now hit the B or Y button.");
		instrUpdater.instructions[2] = ("Great Job! Now try out the grip button on the side of your remote.");
		instrUpdater.instructions[3] = ("Okay, now try using your joystick to turn around. Press left or right to perform a snap turn.");
		instrUpdater.instructions[4] = ("Great job! Using the trigger on the back of the remote, teleport to the blue waypoint.");
		instrUpdater.instructions[5] = ("Great job! Now pick up the egg in front of you using the grip button.");
		instrUpdater.instructions[6] = ("To interact with buttons, hold down the A button to reveal the laser, then hit the trigger to select the button you are hovering over.");
		instrUpdater.instructions[7] = ("Now that you know the basics, feel free to play around. Once you are ready to continue, press the Finish button.");
		instrUpdater.instructions[8] = ("Congrats! You have completed the tutorial, we will now be moving to the first module.");

		instrUpdater.current = 0;
		instrUpdater.queueCount = 0;

		//intialize ActivityManager variables
		activityMan.activityCount = 0;
		activityMan.activityTimes = new float[3];

		//the first task is just entering the scene, so now that setup is done, mark it as complete
		taskMan.MarkTaskCompletion(0);
	}
}
