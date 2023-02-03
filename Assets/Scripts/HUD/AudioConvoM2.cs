using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConvoM2 : MonoBehaviour
{
    //This script handles the audio portion of the instructions for Module 2
    //GameObjects with AudioSource component
    public AudioSource M201;
    public AudioSource M202;
    public AudioSource M203;
    public AudioSource M204;
    public AudioSource M205;
    public AudioSource M206;
    public AudioSource M207;
    public AudioSource M208;
    public AudioSource M209;
    public AudioSource M210;
    public AudioSource M211;
    public AudioSource M212;
    public AudioSource M213;

    //this queue is ued to store files in order
    private Queue<AudioSource> audioInstructions = new Queue<AudioSource>();

    //this keep track of how many files are waiting to be played
    private int backlog = 0;

    //this is used to keep track of the current audio file, is updated throughout
    private AudioSource temp;

    // Start is called before the first frame update
    void Start()
    {
        //Enqueue audio files
        audioInstructions.Enqueue(M201);
        audioInstructions.Enqueue(M202);
        audioInstructions.Enqueue(M203);
        audioInstructions.Enqueue(M204);
        audioInstructions.Enqueue(M205);
        audioInstructions.Enqueue(M206);
        audioInstructions.Enqueue(M207);
        audioInstructions.Enqueue(M208);
        audioInstructions.Enqueue(M209);
        
        //set current file to first
        temp = M201;
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
    /*
    void Update()
    {
        //if there is a backlog, this waits for the first opportunity to play the next instruction audio file and decrement the backlog counter
        if(backlog > 0)
        {
            if (!temp.isPlaying)
            {
                backlog--;
                temp = audioInstructions.Dequeue();
                temp.Play();
            }
        }
    }
    */
}
