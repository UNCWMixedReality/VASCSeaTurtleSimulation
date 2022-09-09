using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInstructionAudioM1 : MonoBehaviour
{
    //This script handles the audio portion of the instructions for Module 1


    //GameObject with AudioSource components
    public AudioSource[] audioInstructions = new AudioSource[21];
    public int currentIdx { get; set; }

    //Plays audio file at the top of the queue
    public void PlaySound()
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
        return audioInstructions[currentIdx].clip.length;
    }
}
