using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class InstructionUpdater : MonoBehaviour
{
    /*
     * This class has one purpose: update instructions.
     * 
     * This is done by calling the RunInstructions() function.
     * --This function: sets the instruction text on the left hand display, ticks the instruction progress bar, and plays the corresponding audio.
     * 
     */


    // Instruction canvas attached to left controller that displays instructions/character/progress
    public GameObject InstructionPanel;
    // Gameobject under canvas that is responsible for displaying text
    public GameObject TextBox;
    // the text component of TextBox
    private Text text;
    
    // Game object that holds the audio portion of the instructions
    public AudioM2_1 InstructionAudio;
    // holds the text portion of the instructions
    private string[] instructions = new string[9];

    //keeps track of the current instruction being displayed
    private int current;

    //progress bar
    public ProgressM2 progressBar;


    public void Start()
    {
        //we first need to initialize our variables.
        text = TextBox.GetComponent<Text>();

        instructions[0] = ("Welcome! This is part one of the Nest Relocation Module");
        instructions[1] = ("During this simulation, you will move sea turtle eggs from an endangered nest to a safer one.");
        instructions[2] = ("When you're ready, go ahead and move to the first marker to get started.");
        instructions[3] = ("Your first task is to dig up this turtle nest. First, grab the blue gloves to put them on.");
        instructions[4] = ("Great job! Now, carefully dig in the sand using your hands until you can see the eggs.");
        instructions[5] = ("Good job! You're ready to move on to the next task.");
        instructions[6] = ("Next, these eggs need to be carefully moved. Some are cracked, though, and will need to be separated from the good eggs.");
        instructions[7] = ("Examine the eggs, and put them in the red bucket if they're cracked, or the green bucket if they aren't.");
        instructions[8] = ("Good job! You have completed VASC module 2 part 1.");

        current = 0;
    }

    //this function calls two functions to set the text and play the audio for the current instruction.
    public void RunInstructions()
    {
        SetInstructionText();
        PlayInstructionAudio();
        progressBar.TickProgressBar();
        current += 1;
    }

    public void SetInstructionText()
    {
        /*
         * set instructions
         */
        text.text = instructions[current];
    }

    public void PlayInstructionAudio()
    {
        /*
         * play sound
         */
        InstructionAudio.playSound();
    }

}
