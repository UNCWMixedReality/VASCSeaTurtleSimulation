using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM2_1 : MonoBehaviour
{
    //This script handles the audio portion of the instructions for Module 2
    //GameObjects with AudioSource component
    //The variable name describes when the audio should be played. ie: the first audio should be play when the player "enteredScene"
    public AudioSource enteredScene;
    public AudioSource movedToNest;
    public AudioSource putOnGloves;
    public AudioSource dugNest;
    public AudioSource sortedEggs;

    //this queue is ued to store files in order
    public Queue<AudioSource> audioInstructions = new Queue<AudioSource>();

    //this keep track of how many files are waiting to be played
    private int backlog = 0;

    //this is used to keep track of the current audio file, is updated throughout
    public AudioSource temp;

    //Plays audio file at the top of the queue
    public void playSound()
    {
        if (!temp.isPlaying)
        {
            temp = audioInstructions.Dequeue();
            temp.Play();
        }
        //increments backlog if a file is currently playing
        else
        {
            backlog++;
        }
    }

    void Update()
    {
        //if there is a backlog, this waits for the first opportunity to play the next instruction audio file and decrement the backlog counter
        if (backlog > 0)
        {
            if (!temp.isPlaying)
            {
                backlog--;
                temp = audioInstructions.Dequeue();
                temp.Play();
            }
        }
    }

    public float GetLength()
    {
        return temp.clip.length;
    }
}