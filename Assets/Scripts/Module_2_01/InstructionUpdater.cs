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
    public Text text;
    
    // Game object that holds the audio portion of the instructions
    public AudioM2_1 InstructionAudio;
    // holds the text portion of the instructions
    private string[] instructions = new string[5];

    //keeps track of the current instruction being displayed
    private int current;

    //progress bar
    public ProgressM2 progressBar;


    public void Start()
    {
        instructions[0] = ("Welcome! This is part one of the Nest Relocation Module. Move to the marker to begin.");
        instructions[1] = ("Your first task is to dig up this turtle nest. First, grab the blue gloves to put them on.");
        instructions[2] = ("Great job! Now, carefully dig in the sand using your hands until you can see the eggs.");
        instructions[3] = ("Good job! Next, these eggs need to be carefully moved. Examine the eggs, and put them in the red bucket if they're cracked, or the green bucket if they aren't.");
        instructions[4] = ("Good job! You have completed VASC module 2 part 1.");

        current = 0;
    }

    //this function calls two functions to set the text and play the audio for the current instruction.
    public IEnumerator RunInstructions()
    {
        //change text and play audio
        SetInstructionText();
        PlayInstructionAudio();
        progressBar.TickProgressBar();
        //determine how long the audio needs to play and wait for that long
        float audioClipLength = InstructionAudio.GetLength();
        yield return new WaitForSecondsRealtime(audioClipLength);
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
