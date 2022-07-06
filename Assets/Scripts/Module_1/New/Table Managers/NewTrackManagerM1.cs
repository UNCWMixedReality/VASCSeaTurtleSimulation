using System.Collections;
using System.Collections.Generic;
using Altimit.UI;
using UnityEngine;
using UnityEngine.UI;
using DataCollection;

public class NewTrackManagerM1 : MonoBehaviour
{
    #region Class Variables
    
    //Necessary Scripts
    public NewTaskManagerM1 taskMan;
    public AudioFeedback audiofeedback;
    public RandomOrder R0; 
    
    //Tracks
    public GameObject Green_Tracks;
    public GameObject Loggerhead_Tracks;
    public GameObject Leatherback_Tracks;

    private GameObject[] TrackList;
    
    //private int currentTrackNum = -1; 
    private int trackIdx;
    private float scaleDuration1 = 2;   
                                        
    //Buttons
    public GameObject BeginButton;
    public GameObject GreenButton;
    public GameObject LoggerheadButton;
    public GameObject LeatherbackButton;
    
    //Answers
    public Image incorrect;
    public Image correct;
    
    //Number of incorrect/correct answers
    public int wrong = 0;
    public int right = 0;
    
    //Things I wish I understood
    
    #endregion
    
    #region Correct/Incorrect Answer Methods
    
    private void Correct()
    {
        correct.color = new Color(1, 1, 1, 1);
        incorrect.color = new Color(1, 1, 1, 0);
        right++;
        audiofeedback.playGood();
        taskMan.MarkTaskCompletion(trackIdx + 17);
    }
    private void Incorrect()
    {
        correct.color = new Color(1, 1, 1, 0);
        incorrect.color = new Color(1, 1, 1, 1);
        wrong++;
        audiofeedback.playBad();
    }
    
    #endregion

    #region ScaleUp/Down methods

    public IEnumerator ScaleUp(int index) //Scales up the active turtle
    {
        float timer = 0f;
        Vector3 startScale = new Vector3(0, 0, 0);
        Vector3 distance = new Vector3(1, 1, 1) - startScale;

        while (timer < scaleDuration1)
        {
            timer += Time.deltaTime;
            TrackList[index].transform.localScale = startScale + distance * timer / scaleDuration1; //Scale the turtle up
            yield return null;
        }
    }

    public IEnumerator ScaleDown(int index) //Scales down the inactive turtle
    {
        float timer = 0f;
        Vector3 startScale = new Vector3(1, 1, 1);
        Vector3 distance = new Vector3(0, 0, 0) - startScale;

        while (timer < scaleDuration1)
        {
            timer += Time.deltaTime;
            TrackList[index].transform.localScale = startScale + distance * timer / scaleDuration1; //Scale turtle down
            yield return null;
        }
    }

    #endregion
    public void PrepareTrackIdentification()
    {
        //Initialize list then order it
        TrackList = new GameObject[3];
        orderTracks();
        trackIdx = 0;
        
        //Activating Tracks
        for (int i = 0; i < 3; i++)
        {
            TrackList[i].transform.localScale = new Vector3(0, 0, 0);
            TrackList[i].SetActive(true);
        }
        
        //Activate begin button
        BeginButton.SetActive(true);
        
        //Disable answer buttons
        LoggerheadButton.SetActive(false);
        GreenButton.SetActive(false);
        LeatherbackButton.SetActive(false);
        
    }

    public void SetNxtTrack(int idx)
    {
        //Scales up the current turtle
        if (idx > 0)
        {
            StartCoroutine(ScaleDown(idx - 1));
        }

        StartCoroutine(ScaleUp(idx));
        //Line added by Blake to log correct choices
        DcDataLogging.SetCorrectAnswer("Track Guessing", new[]
            { "Loggerhead", "Green", "Leatherback" }[trackIdx]);

        trackIdx++; 
    }

    public void CheckAnswer(string trackName) //Called when user selects a button
    {
        if (trackName == "Green" && TrackList[trackIdx - 1] == Green_Tracks)
        {
            Correct();
        }
        else if (trackName == "Loggerhead" && TrackList[trackIdx - 1] == Loggerhead_Tracks)
        {
            Correct();
        }
        else if (trackName == "Leatherback" && TrackList[trackIdx - 1] == Leatherback_Tracks)
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }
    
    private void orderTracks() //Randomizes the order in which tracks appear
    {
        var orderOfTrack = R0.randomize(3);
        TrackList[orderOfTrack[0] - 1] = Green_Tracks;
        TrackList[orderOfTrack[1] - 1] = Loggerhead_Tracks;
        TrackList[orderOfTrack[2] - 1] = Leatherback_Tracks;
    }

    public void PrepareQuestions()
    {
        Debug.Log("This is working");
        audiofeedback.playSelection();
        //Enable selection buttons
        LoggerheadButton.SetActive(true);
        GreenButton.SetActive(true);
        LeatherbackButton.SetActive(true);
    }

    public void SetNextQuestion()
    {
        SetNxtTrack(trackIdx);
    }
}
