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
    public string[] instructions { get; set; }

    //keeps track of the current instruction being displayed
    public int current { get; set; }

    //progress bar
    public ProgressM2 progressBar;

    private bool audioPlaying;
    private float audioClipLength;
    //this function calls two functions to set the text and play the audio for the current instruction.
    public IEnumerator RunInstructions()
    {
        //if instructions are currently being read, wait to update until they are completed.
        if (audioPlaying)
        {
            //get the length of the current clip
            audioClipLength = InstructionAudio.GetLength();
            //wait that long
            yield return new WaitForSecondsRealtime(audioClipLength);
        }
        //set text and play audio
        SetInstructionText();
        progressBar.TickProgressBar();
        audioPlaying = true;
        PlayInstructionAudio();
        //determine how long the audio needs to play and wait for that long
        audioClipLength = InstructionAudio.GetLength();
        yield return new WaitForSecondsRealtime(audioClipLength);
        //now the audio should be done playing.
        audioPlaying = false;
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
