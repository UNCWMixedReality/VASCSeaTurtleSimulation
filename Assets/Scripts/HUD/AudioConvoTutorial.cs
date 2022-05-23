using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConvoTutorial : MonoBehaviour
{
    //This script handles the audio portion of instructions for the Tutorial
    //GameObjects with AudioSource component
    public AudioSource T1;
    public AudioSource T2;
    public AudioSource T3;
    public AudioSource T4;
    public AudioSource T5;
    public AudioSource T6;
    public AudioSource T7;

    //this queue is used to store files in order
    private Queue<AudioSource> audioInstructions = new Queue<AudioSource>();

    //this keeps track of how many files are waiting to be played
    private int backlog = 0;

    //this is used to keep track of the current audio file, is updated throughout
    private AudioSource temp;

    void Start()
    {
        //Enqueue audio files
        audioInstructions.Enqueue(T1);
        audioInstructions.Enqueue(T2);
        audioInstructions.Enqueue(T3);
        audioInstructions.Enqueue(T4);
        audioInstructions.Enqueue(T5);
        audioInstructions.Enqueue(T6);
        audioInstructions.Enqueue(T7);
        //set current file to first
        temp = T1;
    }

    //Plays audio file at the top of the queue
    public void playSound()
    {
        //checks if a different audio file is playing
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
        //if there is a backlog, this waits for the first opportunity to play the next instruction audio file and decrements the backlog counter
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
}
