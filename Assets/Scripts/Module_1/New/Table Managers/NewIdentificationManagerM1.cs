using System.Collections;
using System.Collections.Generic;
using DataCollection;
using UnityEngine;
using UnityEngine.UI;

public class NewIdentificationManagerM1 : MonoBehaviour
{

    //class variables
    #region
    //references to the turtle models
    public GameObject Loggerhead;
    public GameObject Hawksbill;
    public GameObject Leatherback;
    private GameObject[] TurtleList;
    private int turtleIdx;
    private string correctAnswer;
    //screen buttons
    public GameObject BeginButton;
    public GameObject LoggerheadButton;
    public GameObject HawksbillButton;
    public GameObject LeatherbackButton;

    //reference to green check mark and red x images
    public Image incorrect;
    public Image correct;

    //number of incorrect/correct answers
    public int wrong = 0;
    public int right = 0;

    //references to necessary scripts
    public NewTaskManagerM1 taskMan;
    public AudioFeedback audiofeedback;
    public RandomOrder RO;
    public IdentifyInstructions instrSetter;

    private float scaleDuration = 2;
    #endregion

    public void SetNextTurtle(int idx)
    {
        /*
         * This scales up the current turtle so it is visible
         */
        Debug.Log("The index is " + idx);
        if (idx > 0)
        {
            StartCoroutine(scaleDown(idx-1));
        }
        TurtleList[idx].SetActive(true);
        StartCoroutine(scaleUp(idx));
        turtleIdx++;

    }

    public void CheckAnswer(string turtleName)//will be called when the user selects a answer button
    {
        if (turtleName == "Loggerhead" && TurtleList[turtleIdx - 1] == Loggerhead)
        {
            taskMan.LogDecision(turtleName, "Loggerhead", "Correctly answered Turtle Identification question number " + turtleIdx);
            Correct();
        }
        else if (turtleName == "Hawksbill" && TurtleList[turtleIdx - 1] == Hawksbill)
        {
            taskMan.LogDecision(turtleName, "Hawksbill", "Correctly answered Turtle Identification question number " + turtleIdx);
            Correct();
        }
        else if (turtleName == "Leatherback" && TurtleList[turtleIdx - 1] == Leatherback)
        {
            taskMan.LogDecision(turtleName, "Leatherback", "Correctly answered Turtle Identification question number " + turtleIdx);
            Correct();
        }
        else
        {   
            if (TurtleList[turtleIdx - 1] == Loggerhead)
            {
                correctAnswer = "Loggerhead";
            }
            else if (TurtleList[turtleIdx - 1] == Hawksbill)
            {
                correctAnswer = "Hawksbill";
            }
            else
            {
                correctAnswer = "Leatherback";
            }

            taskMan.LogDecision(turtleName, correctAnswer , "Incorrectly answered Turtle Identification question number" + turtleIdx);
            Incorrect();
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

    private void Correct()
    {
        correct.color = new Color(1, 1, 1, 1);
        incorrect.color = new Color(1, 1, 1, 0);
        right++;
        audiofeedback.playGood();
        taskMan.MarkTaskCompletion(turtleIdx + 12);

    }

    private void Incorrect()
    {
        correct.color = new Color(1, 1, 1, 0);
        incorrect.color = new Color(1, 1, 1, 1);
        wrong++;
        audiofeedback.playBad();
    }

    private void OrderTurtles()
    {

        //This puts the turtles in radom order each time the scene is run

        var orderOfTurtles = RO.randomize(3);

        TurtleList[orderOfTurtles[0] - 1] = Loggerhead;
        TurtleList[orderOfTurtles[1] - 1] = Hawksbill;
        TurtleList[orderOfTurtles[2] - 1] = Leatherback;

        instrSetter.SetInstructions(orderOfTurtles[0] - 1, orderOfTurtles[1] - 1, orderOfTurtles[2] - 1);

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

    public void SetNextQuestion()
    {
        SetNextTurtle(turtleIdx);

    }
}
