using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayer : MonoBehaviour
{
    //these videos are played in M2 to show how to complete tasks
    public GameObject DigVid;
    public GameObject SortVid;
    public GameObject ReplaceVid;
    public GameObject CoverVid;

    //this is the tortle on the instruction panel
    public GameObject VisualAid;

    //used for iterating through the queue
    private GameObject temp;

    private Queue<GameObject> videos = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //fill out the queue
        videos.Enqueue(DigVid);
        videos.Enqueue(SortVid);
        videos.Enqueue(ReplaceVid);
        videos.Enqueue(CoverVid);
        temp = null;
    }


    public void stopVid()
    {
        //stop the current vid and bring back the tortle
        temp.SetActive(false);
        VisualAid.SetActive(true);
    }

    public void nextVidPlay()
    {
        //get rid of the tortle and play the video
        temp = videos.Dequeue();
        temp.SetActive(true);
        VisualAid.SetActive(false);
    }
}
