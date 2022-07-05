using System.Collections;
using System.Collections.Generic;
using DataCollection;
using UnityEngine;
using UnityEngine.UI;

public class NewIdentificationManagerM1 : MonoBehaviour
{


    //references to the turtle models
    public GameObject Loggerhead;
    public GameObject Hawksbill;
    public GameObject Leatherback;
    private GameObject[] TurtleList;
    private int turtleIdx;

    //screen buttons
    public GameObject BeginButton;
    public GameObject LoggerheadButton;
    public GameObject HawksbillButton;
    public GameObject LeatherbackButton;

    //reference to green check mark and red x images
    public Image incorrect;
    public Image correct;

    //number of incorrect answers
    public int wrong = 0;

    //references to necessary scripts
    public NewTaskManagerM1 taskMan;

    public AudioFeedback audiofeedback;
    public RandomOrder RO;

    private bool task2Done = false;

    
    private int currentTurtleNum = -1;
    private int rightCounter = 0;
    private float scaleDuration = 2;

    public void SetNextTurtle(int idx) //Will be called by the TableManager script to start the activity.
    {
        StartCoroutine(scaleUp(idx));
        // line added by Blake to log correct choices
        DcDataLogging.SetCorrectAnswer("TurtleGuessing", new[] {
                    "Loggerhead" , "Hawksbill", "Leatherback"}[currentTurtleNum]);
        if (currentTurtleNum == -1) //If this is the first turtle
            {
                StartCoroutine(scaleUp(0));
                
                // line added by Blake to log correct choices
                DcDataLogging.SetCorrectAnswer("TurtleGuessing", "Loggerhead");
            }
            else //If this is the second or third turtle
            {
                StartCoroutine(scaleDown(currentTurtleNum));
                StartCoroutine(scaleUp(currentTurtleNum + 1));
                // line added by Blake to log correct choices
                DcDataLogging.SetCorrectAnswer("TurtleGuessing", new[] {
                    "Hawksbill", "Leatherback"}[currentTurtleNum]);
            }

            currentTurtleNum += 1;

    }

    public void selectTurtle(string turtleName)//will be called when the user selects turtle
    {
        if (turtleName == "Loggerhead" && TurtleList[currentTurtleNum] == Loggerhead)
        {
            print("Loggerhead correct choice");
            Correct();
        }
        else if (turtleName == "Hawksbill" && TurtleList[currentTurtleNum] == Hawksbill)
        {
            print("Hawksbill correct choice");
            Correct();
        }
        else if (turtleName == "Leatherback" && TurtleList[currentTurtleNum] == Leatherback)
        {
            print("Leatherback correct choice");
            Correct();
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
        Vector3 startScale = new Vector3(0, 0, 0);
        Vector3 distance = new Vector3(1, 1, 1) - startScale;

        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;
            TurtleList[index].transform.localScale = startScale + distance * timer / scaleDuration; //Scale the turtle up
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
            TurtleList[index].transform.localScale = startScale + distance * timer / scaleDuration; //Scale the turtle up
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
    }

    private void OrderTurtles()
    {

        //This puts the turtles in radom order each time the scene is run

        var orderOfTurtles = RO.randomize(3);

        TurtleList[orderOfTurtles[0] - 1] = Loggerhead;
        TurtleList[orderOfTurtles[1] - 1] = Hawksbill;
        TurtleList[orderOfTurtles[2] - 1] = Leatherback;
 
    }


    public void PrepareTurtleIdentification()
    {

        //initialize list and then order the turtles in it
        TurtleList = new GameObject[3];
        OrderTurtles();
        turtleIdx = 0;

        //scale the turtles down
        for (int i = 0; i < 3; i++)
        {
            TurtleList[i].transform.localScale = new Vector3(0, 0, 0);
            TurtleList[i].SetActive(false);
        }

        //set the begin button active
        BeginButton.SetActive(true);

        //disable the answer buttons
        LoggerheadButton.SetActive(false);
        HawksbillButton.SetActive(false);
        LeatherbackButton.SetActive(false);

        
    }

    public void PrepareQuestioning()
    {
        audiofeedback.playSelection();
        //enable selection buttons
        LoggerheadButton.SetActive(true);
        HawksbillButton.SetActive(true);
        LeatherbackButton.SetActive(true);
    }
}
