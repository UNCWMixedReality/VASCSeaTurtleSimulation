using System.Collections;
using System.Collections.Generic;
using DataCollection;
using UnityEngine;
using UnityEngine.UI;

//Script By: Cameron Detig 10/16/2020
//Manages the turtle identification activity and determines when to switch to the next activity.

public class Table_Turtle_Manager : MonoBehaviour
{
    public GameObject Loggerhead;
    public GameObject Hawksbill;
    public GameObject Leatherback;
    public GameObject Load;

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
    public AudioSource HKB1;
    public AudioSource HKB2;
    public AudioSource HKB3;
    public AudioSource LTB1;
    public AudioSource LTB2;

    public GameObject LGHScreen1;
    public GameObject LGHScreen2;
    public GameObject HKBScreen1;
    public GameObject HKBScreen2;
    public GameObject HKBScreen3;
    public GameObject LTBScreen1;
    public GameObject LTBScreen2;

    public AudioSource lastPlayed;
    private AudioSource nextToPlay;
    private GameObject lastShown;
    public int backlog = 0;
    private Queue<AudioSource> Backlog = new Queue<AudioSource>();
    private Queue<GameObject> ScreenBacklog = new Queue<GameObject>();
	
	public InstructionManager instMan;
    public AudioFeedback audiofeedback;
    public Progress prog;
    public RandomOrder RO;

    private bool task2Done = false;
	public Button LoggerheadButton;
	public Button HawksbillButton;
	public Button LeatherbackButton;
    public Activity_Manager activityManager;

    private GameObject[] tortle = new GameObject[3];
    private int currentTurtleNum = -1;
    private int rightCounter = 0;
    private float scaleDuration = 2;

    private AudioSource[] wrongButton = new AudioSource[5];
    private AudioSource[] lgh = new AudioSource[2];
    private AudioSource[] hkb = new AudioSource[3];
    private AudioSource[] ltb = new AudioSource[2];
    private GameObject[] factoidslgh = new GameObject[2];
    private GameObject[] factoidshkb = new GameObject[3];
    private GameObject[] factoidsltb = new GameObject[2];

    void Start()
    {
        orderTortles();

        Load.SetActive(false);

        LoggerheadButton.interactable = false;
        HawksbillButton.interactable = false;
        LeatherbackButton.interactable = false;

        for (int i = 0; i < 3; i++)
        {
            tortle[i].transform.localScale = new Vector3(0, 0, 0);
            tortle[i].SetActive(true);
        }
        //set up lists
        wrongButton[0] = Wrong1;
        wrongButton[1] = Wrong2;
        wrongButton[2] = Wrong3;
        wrongButton[3] = Wrong4;
        wrongButton[4] = Wrong5;

        lgh[0] = LGH1;
        lgh[1] = LGH2;

        hkb[0] = HKB1;
        hkb[1] = HKB2;
        hkb[2] = HKB3;

        ltb[0] = LTB1;
        ltb[1] = LTB2;

        factoidslgh[0] = LGHScreen1;
        factoidslgh[1] = LGHScreen2;

        factoidshkb[0] = HKBScreen1;
        factoidshkb[1] = HKBScreen2;
        factoidshkb[2] = HKBScreen3;

        factoidsltb[0] = LTBScreen1;
        factoidsltb[1] = LTBScreen2;

        lastPlayed = Wrong1;
        lastShown = LGHScreen1;
    }


    void Update()
    {
        if(backlog > 0)
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
        if (rightCounter == 3)
        {
            LeatherbackButton.interactable = false;
            LoggerheadButton.interactable = false;
            HawksbillButton.interactable = false;
            task2Done = true;
            activityManager.ChangeTable(activityManager.TableThree);
            audiofeedback.playCompletion();
            rightCounter++;
        }
    }

    public void nextTurtle() //Will be called by the TableManager script to start the activity.
    {
        print("next turtle called");
        if (currentTurtleNum < 3 - 1) //Don't run if at the end of the list
        {
            print("Next Turtle");
            if (currentTurtleNum == -1) //If this is the first turtle
            {
                StartCoroutine(scaleUp(0));
                audiofeedback.playSelection();
                //enable selection buttons
                LoggerheadButton.interactable = true;
                HawksbillButton.interactable = true;
                LeatherbackButton.interactable = true;
                // line added by Blake to log correct choices
                DcDataLogging.SetCorrectAnswer("TurtleGuessing", "Loggerhead");
            }
            else //If this is the second or third turtle
            {
                StartCoroutine(scaleDown(currentTurtleNum));
                StartCoroutine(scaleUp(currentTurtleNum + 1));
                // line added by Blake to log correct choices
                DcDataLogging.SetCorrectAnswer("TurtleGuessing", new [] {
                    "Hawksbill", "Leatherback"}[currentTurtleNum]);
            }

            currentTurtleNum += 1;
        }
        else //End of activity
        {
            StartCoroutine(scaleDown(currentTurtleNum));
        }
    }

    public void selectTurtle(string turtleName)//will be called when the user selects turtle
    {
        if (turtleName == "Loggerhead" && tortle[currentTurtleNum] == Loggerhead) 
        {
            print("Loggerhead correct choice");
			Correct();
            playNoise(lgh, lgh.Length, factoidslgh);
            nextTurtle();
            prog.TickProgressBar();
        }
        else if(turtleName == "Hawksbill" && tortle[currentTurtleNum] == Hawksbill)
        {
            print("Hawksbill correct choice");
			Correct();
            playNoise(hkb, hkb.Length, factoidshkb);
            nextTurtle();
            prog.TickProgressBar();
        }
        else if(turtleName == "Leatherback" && tortle[currentTurtleNum] == Leatherback)
        {
            nextTurtle();
            print("Leatherback correct choice");
			Correct();
            playNoise(ltb, ltb.Length, factoidsltb);
            prog.TickProgressBar();
        }
        else
        {
            print("Incorrect choice");
			Incorrect();
			wrong++;
			
        }
    }
 
    public IEnumerator scaleUp(int index) //Scales up the active turtle.
    {
        float timer = 0f;
        Vector3 startScale = new Vector3(0,0,0);
        Vector3 distance = new Vector3(1,1,1) - startScale;

        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;
            tortle[index].transform.localScale = startScale + distance * timer / scaleDuration; //Scale the turtle up
            yield return null;
        }
    }

    public IEnumerator scaleDown(int index) //Scales down the inactive turtle.
    {
        float timer = 0f;
        Vector3 startScale = new Vector3(1, 1, 1);
        Vector3 distance = new Vector3(0, 0, 0) - startScale;

        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;
            tortle[index].transform.localScale = startScale + distance * timer / scaleDuration; //Scale the turtle up
            yield return null;
        }
    }

    public bool getTask2Done()
    {
        return task2Done;
    }
	
	private void Correct()
	{
		correct.color = new Color(1, 1, 1, 1);
        incorrect.color = new Color(1, 1, 1, 0);
        rightCounter++;
        audiofeedback.playGood();

	}
	
	private void Incorrect()
	{
		correct.color = new Color(1, 1, 1, 0);
        incorrect.color = new Color(1, 1, 1, 1);
        audiofeedback.playBad();
        playNoise(wrongButton, wrongButton.Length);
	}

    private void playNoise(AudioSource[] audioList, int length, GameObject[] screenList = null)
    {
        System.Random rnd = new System.Random();
        int track = rnd.Next(length);
        //use track to select the audio and the corresponding visual
        if (!lastPlayed.isPlaying)
        {
            //deactive previous thingy
            audioList[track].Play();
            lastPlayed = audioList[track];
            if (screenList != null)
            {
                lastShown.SetActive(false);
                screenList[track].SetActive(true);
                lastShown = screenList[track];
            }
        }
        else
        {
            backlog++;
            Backlog.Enqueue(audioList[track]);
            if (screenList != null)
            {
                ScreenBacklog.Enqueue(screenList[track]);
            }
        }
    }

    private void orderTortles()
    {
        var orderOfTortle = RO.randomize(3);
        foreach(var thing in orderOfTortle)
        {
            print(thing);
        }
        tortle[orderOfTortle[0]-1] = Loggerhead;
        tortle[orderOfTortle[1]-1] = Hawksbill;
        tortle[orderOfTortle[2]-1] = Leatherback;
        foreach(var item in tortle)
        {
            print(item);
        }
    }
}
