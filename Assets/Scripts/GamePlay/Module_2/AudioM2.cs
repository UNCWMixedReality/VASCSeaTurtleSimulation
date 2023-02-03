using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM2 : MonoBehaviour
{
    //This script handles the audio portion of the instructions for Module 2


    //GameObject with AudioSource components
    public AudioSource[] audioInstructions = new AudioSource[5];
    public int currentIdx { get; set; }

    //this queue is used to store files in order
    //public Queue<AudioSource> audioInstructions = new Queue<AudioSource>();

    //this is used to keep track of the current audio file, is updated throughout
    public AudioSource temp { get; set; }

    //Plays audio file at the top of the queue
    public void playSound()
    {

        if (currentIdx > 0 && audioInstructions[currentIdx - 1].isPlaying)
        {
            audioInstructions[currentIdx - 1].Stop();
        }

        audioInstructions[currentIdx].Play();
        currentIdx++;
    }



    public float GetLength()
    {
        return temp.clip.length;
    }

}