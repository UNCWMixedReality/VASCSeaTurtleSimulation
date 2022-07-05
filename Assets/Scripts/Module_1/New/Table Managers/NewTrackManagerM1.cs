using System.Collections;
using System.Collections.Generic;
using Altimit.UI;
using UnityEngine;
using UnityEngine.UI;
using DataCollection;

public class NewTrackManagerM1 : MonoBehaviour
{
    #region Class Variables

    //Tracks
    public GameObject Green_Tracks;
    public GameObject Loggerhead_Tracks;
    public GameObject Leatherback_Tracks;

    private GameObject[] trocks = new GameObject[3];
    private int currentTrackNum = -1;   // Unsure of intended use
    private int rightCountre = 0;       // ^^
    private float scaleDuration1 = 2;   //
    public RandomOrder R0;              // ^^
    //Buttons
    public GameObject Begin;
    public Button GreenButton;
    public Button LoggerheadButton;
    public Button LeatherbackButton;
    //Answers
    public Image incorrect;
    public Image correct;
    //Things I wish I understood
    
    #endregion

    #region Audio Stuff
    
    

    #endregion

    #region Correct/Incorrect Answer Methods
    
    private void Correct()
    {
        correct.color = new Color(1, 1, 1, 1);
        incorrect.color = new Color(1, 1, 1, 0);
        rightCountre++;
    }
    private void Incorrect()
    {
        correct.color = new Color(1, 1, 1, 0);
        incorrect.color = new Color(1, 1, 1, 1);
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
            trocks[index].transform.localScale = startScale + distance * timer / scaleDuration1; //Scale the turtle up
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
            trocks[index].transform.localScale = startScale + distance * timer / scaleDuration1; //Scale turtle down
            yield return null;
        }
    }

    #endregion
    public void PrepareBegin()
    {
        orderTrocks();
        Begin.SetActive(false); // May need to be set to true
        //Disabling Button Interaction
        GreenButton.interactable = false;
        LoggerheadButton.interactable = false;
        LeatherbackButton.interactable = false;
        //Activating Tracks
        for (int i = 0; i < 3; i++)
        {
            trocks[i].transform.localScale = new Vector3(0, 0, 0);
            trocks[i].SetActive(true);
        }
    }

    public void PrepareQuestion1()
    {
        nextTrack();
    }

    public void PrepareQuestion2()
    {
        nextTrack();
    }

    public void PrepareQuestion3()
    {
        nextTrack();
    }

    private void orderTrocks() //Randomizes the order in which tracks appear
    {
        var orderOfTrock = R0.randomize(3);
        trocks[orderOfTrock[0] - 1] = Green_Tracks;
        trocks[orderOfTrock[1] - 1] = Loggerhead_Tracks;
        trocks[orderOfTrock[2] - 1] = Leatherback_Tracks;
    }

    public void nextTrack()
    {
        if (currentTrackNum < 3 - 1) //Stops if at end of list
        {
            if (currentTrackNum == -1)
            {
                StartCoroutine(ScaleUp(0));
                //Enable Buttons
                GreenButton.interactable = true;
                LoggerheadButton.interactable = true;
                LeatherbackButton.interactable = true;
                //Copied line added by Blake
                DcDataLogging.SetCorrectAnswer("TrackGuessing", "Green");
            }
            else //Second or Third Turtles
            {
                StartCoroutine(ScaleDown(currentTrackNum));
                StartCoroutine(ScaleUp(currentTrackNum + 1));
                //Copied Line added by Blake
                DcDataLogging.SetCorrectAnswer("TrackGuessing", new [] {
                    "Loggerhead", "Leatherback"}[currentTrackNum]);
            }
        }
        else //End of Activity
        {
            StartCoroutine(ScaleDown(currentTrackNum));
        }
    }

    public void selectTracks(string trackName)
    {
        if (trackName == "Green" && trocks[currentTrackNum] == Green_Tracks)
        {
            Correct();
            //Mark Task Completion? (this would then call prepare nxt question)
        }
        else if (trackName == "Loggerhead" && trocks[currentTrackNum] == Loggerhead_Tracks)
        {
            Correct();
            //MTC
        }
        else if (trackName == "Leatherback" && trocks[currentTrackNum] == Leatherback_Tracks)
        {
            Correct();
            //MTC
        }
        else
        {
            Incorrect();
        }
    }
}
