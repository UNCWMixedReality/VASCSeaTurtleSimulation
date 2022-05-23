using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Good
//FX78
//Bad
//FX77
//Selection
//FX13 4
//Completion
//FX07
//Starting?
//FX10

//in all scenes, this script is used to play generic sound effects
public class AudioFeedback : MonoBehaviour
{
    public AudioSource Good;
    public AudioSource Bad;
    public AudioSource Completion;
    public AudioSource Selection;
    public AudioSource Starting;

    public void playGood()
    {
        Good.Play();
    }

    public void playBad()
    {
        Bad.Play();
    }

    public void playCompletion()
    {
        Completion.Play();
    }

    public void playSelection()
    {
        Selection.Play();
    }

    public void playStarting()
    {
        Starting.Play();
    }
}
