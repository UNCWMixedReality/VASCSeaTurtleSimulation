using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class InstructionUpdaterM3_1 : MonoBehaviour
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
    public GameObject TextBox;
    public Text text;
    public ProgressM3 progressBar;

    //Holds the audio/text portion of the instructions
    public AudioM3_1 instructionAudio;
    public string[] instructions { get; set; }


    //keeps track of the current instruction being displayed and the number of instructions that have been queued
    public int current { get; set; }
    public int queueCount { get; set; }

    public void RunInstructions()
    {
        /*
         * This is the primary function in this script, it performs the three necessary steps in running the next set of instructions
         * Marks the progress bar, sets the instruction text, and plays the instruction audio
         */

        //set text, mark progress bar, and play audio
        SetInstructionText();
        PlayInstructionAudio();

        current += 1;
        if (current > 1)
        {
            progressBar.TickProgressBar();
        }
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
        instructionAudio.playSound();
    }

}