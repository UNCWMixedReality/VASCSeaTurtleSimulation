using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataCollection;
//Script By: Cameron Detig 10/16/2020
//Manages the turtle identification activity and determines when to switch to the next activity.

public class Table_Tracks_Manager : MonoBehaviour
{

    public GameObject Green_Tracks;
    public GameObject Loggerhead_Tracks;
    public GameObject Leatherback_Tracks;
    public GameObject Load;

	public InstructionManager instMan;
    public AudioFeedback audiofeedback;
    public Progress prog;
    public RandomOrder RO;

	public Image incorrect;
    public Image correct;
	
	public int wrong = 0;

    public AudioSource Wrong1;
    public AudioSource Wrong2;
    public AudioSource Wrong3;
    public AudioSource Wrong4;
    public AudioSource Wrong5;
    public AudioSource LGH1;
    public AudioSource LGH2;
    public AudioSource GRN1;
    public AudioSource GRN2;
    public AudioSource GRN3;
    public AudioSource LTB1;
    public AudioSource LTB2;

    public GameObject LGHScreen1;
    public GameObject LGHScreen2;
    public GameObject GRNScreen1;
    public GameObject GRNScreen2;
    public GameObject GRNScreen3;
    public GameObject LTBScreen1;
    public GameObject LTBScreen2;
    
    public AudioSource lastPlayed;
    private AudioSource nextToPlay;
    private GameObject lastShown;
    public int backlog = 0;
    private Queue<AudioSource> Backlog = new Queue<AudioSource>();
    private Queue<GameObject> ScreenBacklog = new Queue<GameObject>();

    private bool task3Done = false;
	public Button GreenButton;
	public Button LoggerheadButton;
	public Button LeatherbackButton;
    public TableManager tableManager;

    private GameObject[] trocks = new GameObject[3];
    private int currentTrackNum = -1;
    private int rightCountre = 0;
    private float scaleDuration1 = 2;

    private AudioSource[] wrongButton = new AudioSource[5];
    private AudioSource[] lgh = new AudioSource[2];
    private AudioSource[] grn = new AudioSource[3];
    private AudioSource[] ltb = new AudioSource[2];
    private GameObject[] factoidlgh = new GameObject[2];
    private GameObject[] factoidgrn = new GameObject[3];
    private GameObject[] factoidltb = new GameObject[2];

    void Start()
    {
        //orderTrocks();

        Load.SetActive(false);

        GreenButton.interactable = false;
        LoggerheadButton.interactable = false;
        LeatherbackButton.interactable = false;

        for (int i = 0; i < 3; i++)
        {
            trocks[i].transform.localScale = new Vector3(0, 0, 0);
            trocks[i].SetActive(true);
        }
        //set up lists
        wrongButton[0] = Wrong1;
        wrongButton[1] = Wrong2;
        wrongButton[2] = Wrong3;
        wrongButton[3] = Wrong4;
        wrongButton[4] = Wrong5;

        lgh[0] = LGH1;
        lgh[1] = LGH2;

        grn[0] = GRN1;
        grn[1] = GRN2;
        grn[2] = GRN3;

        ltb[0] = LTB1;
        ltb[1] = LTB2;

        factoidlgh[0] = LGHScreen1;
        factoidlgh[1] = LGHScreen2;

        factoidgrn[0] = GRNScreen1;
        factoidgrn[1] = GRNScreen2;
        factoidgrn[2] = GRNScreen3;

        factoidltb[0] = LTBScreen1;
        factoidltb[1] = LTBScreen2;

        lastPlayed = Wrong1;
        lastShown = LGHScreen1;
    }


    void Update()
    {
        if (backlog > 0)
        {
            if (!lastPlayed.isPlaying)
            {
                backlog--;
                lastPlayed = Backlog.Dequeue();
                lastPlayed.Play();
                lastShown.SetActive(false);
                lastShown = ScreenBacklog.Dequeue();
                lastShown.SetActive(true);
            }
        }
        if(rightCountre == 3)
        {
            GreenButton.interactable = false;
            LoggerheadButton.interactable = false;
            LeatherbackButton.interactable = false;
            task3Done = true;
            audiofeedback.playCompletion();
            rightCountre++;
        }

    }
    public void nextTrack() //Will be called by the TableManager script to start the activity.
    {
        if (currentTrackNum < 3 - 1) //Don't run if at the end of the list
        {
            print("Next Track");
            if (currentTrackNum == -1) //If this is the first turtle
            {
                //StartCoroutine(scaleUp(0));
                audiofeedback.playSelection();
                //enable button selection
                GreenButton.interactable = true;
                LoggerheadButton.interactable = true;
                LeatherbackButton.interactable = true;
                // line added by Blake to log correct choices
                DcDataLogging.SetCorrectAnswer("TrackGuessing", "Green");

            }
            else //If this is the second or third turtle
            {
                //StartCoroutine(scaleDown(currentTrackNum));
                //StartCoroutine(scaleUp(currentTrackNum + 1));
                // line added by Blake to log correct choices
                DcDataLogging.SetCorrectAnswer("TrackGuessing", new [] {
                    "Loggerhead", "Leatherback"}[currentTrackNum]);
            }

            currentTrackNum += 1;
        }
        else //End of activity
        {
            //StartCoroutine(scaleDown(currentTrackNum));
        }
    }

    public void selectTracks(string trackName)
    {
        if (trackName == "Green" && trocks[currentTrackNum] == Green_Tracks)
        {
            print("Green correct choice");
			Correct();
            playNoise(grn, grn.Length, factoidgrn);
            nextTrack();
            prog.TickProgressBar();
        }
        else if (trackName == "Loggerhead" && trocks[currentTrackNum] == Loggerhead_Tracks)
        {
            print("Loggerhead correct choice");
			Correct();
            playNoise(lgh, lgh.Length, factoidlgh);
            nextTrack();
            prog.TickProgressBar();
        }
        else if (trackName == "Leatherback" && trocks[currentTrackNum] == Leatherback_Tracks)
        {
            nextTrack();
            print("Leatherback correct choice");
			Correct();
            playNoise(ltb, ltb.Length, factoidltb);
            prog.TickProgressBar();
        }
        else
        {
			Incorrect();
			wrong++;
            print("Incorrect choice");
        }
    }


    /*public IEnumerator scaleUp(int index) //Scales up the active turtle.
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
    }*/

    /*public IEnumerator scaleDown(int index) //Scales down the inactive turtle.
    {
        float timer = 0f;
        Vector3 startScale = new Vector3(1, 1, 1);
        Vector3 distance = new Vector3(0, 0, 0) - startScale;

        while (timer < scaleDuration1)
        {
            timer += Time.deltaTime;
            trocks[index].transform.localScale = startScale + distance * timer / scaleDuration1; //Scale the turtle up
            yield return null;
        }
    }*/

    public bool getTask3Done()
    {
        return task3Done;
    }
	
	private void Correct() // 7/5 - Implemented all but audio feedback
	{
		//correct.color = new Color(1, 1, 1, 1);
        //incorrect.color = new Color(1, 1, 1, 0);
        //rightCountre++;
        audiofeedback.playGood();
	}
	
	private void Incorrect() // 7/5 - Implemented all but audio feedback
	{
		//correct.color = new Color(1, 1, 1, 0);
        //incorrect.color = new Color(1, 1, 1, 1);
        audiofeedback.playBad();
        playNoise(wrongButton, wrongButton.Length);
	}

    //plays a feedback sound based on player selection
    private void playNoise(AudioSource[] audioList, int length, GameObject[] screenList = null)
    {
        //generates a random value in the given list of audio files
        System.Random rnd = new System.Random();
        int track = rnd.Next(length);
        //if there's no sound playing, play the new randomly selected sound
        if (!lastPlayed.isPlaying)
        {
            audioList[track].Play();
            lastPlayed = audioList[track];
            if(screenList != null)
            {
                lastShown.SetActive(false);
                screenList[track].SetActive(true);
                lastShown = screenList[track];
            }
        }
        //if there is a sound playing, add this sound to be played when it's done
        else
        {
            backlog++;
            Backlog.Enqueue(audioList[track]);
            if(screenList != null)
            {
                ScreenBacklog.Enqueue(screenList[track]);
            }
        }
    }

   /* private void orderTrocks()
    {
        //randomizes the order the tracks appear in
        var orderOfTrock = RO.randomize(3);
        foreach(var thing in orderOfTrock)
        {
            print(thing);
        }
        trocks[orderOfTrock[0] - 1] = Green_Tracks;
        trocks[orderOfTrock[1] - 1] = Loggerhead_Tracks;
        trocks[orderOfTrock[2] - 1] = Leatherback_Tracks;
        foreach(var item in trocks)
        {
            print(item);
        }
    } */
}
