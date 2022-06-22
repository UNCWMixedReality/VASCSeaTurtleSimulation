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
    public AudioSource temp { get; set; }

    //Plays audio file at the top of the queue
    public void playSound()
    {

        temp = audioInstructions.Dequeue();
        temp.Play();

    }

    public float GetLength()
    {
        return temp.clip.length;
    }
}