using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConvo : MonoBehaviour
{
    //This script handles the audio portion of the instructions for Module 1
    //GameObjects with AudioSource component
    public AudioSource M101;
    public AudioSource M102;
    public AudioSource M103;
    public AudioSource M104;
    public AudioSource M105;
    public AudioSource M106;
    public AudioSource M107;
    public AudioSource M108;
    public AudioSource M109;
    public AudioSource M110;
    public AudioSource M111;
    public AudioSource M112;

    //Scripts that will be referenced
    public Table_Turtle_Manager turtles;
    public Table_Tracks_Manager tracks;

    //this queue is used to store files in order
    private Queue<AudioSource> audioInstructions = new Queue<AudioSource>();

    //this keeps track of how many files are waiting to be played
    private int backlog = 0;

    //this is used to keep track of the current audio file, is updated throughout
    private AudioSource temp;

    // Start is called before the first frame update
    void Start()
    {
        //Enqueue audio files
        audioInstructions.Enqueue(M101);
        audioInstructions.Enqueue(M102);
        audioInstructions.Enqueue(M103);
        audioInstructions.Enqueue(M104);
        audioInstructions.Enqueue(M105);
        audioInstructions.Enqueue(M106);
        audioInstructions.Enqueue(M107);
        audioInstructions.Enqueue(M108);
        audioInstructions.Enqueue(M109);
        audioInstructions.Enqueue(M110);
        audioInstructions.Enqueue(M111);
        audioInstructions.Enqueue(M112);
        //set current file to first 
        temp = M101;
    }

    //Plays the audio file at the top of the queue
    public void playSound()
    {
        //checks if a different audio file is playing, including non-instruction audio files
        if (!temp.isPlaying && !turtles.lastPlayed.isPlaying && !tracks.lastPlayed.isPlaying){
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
        if (backlog > 0){
            if (!temp.isPlaying && !turtles.lastPlayed.isPlaying && !tracks.lastPlayed.isPlaying){
                backlog--;
                temp = audioInstructions.Dequeue();
                temp.Play();
            }
        }
    }
}
