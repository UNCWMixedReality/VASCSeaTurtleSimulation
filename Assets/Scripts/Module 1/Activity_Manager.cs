using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Activity_Manager : MonoBehaviour
{
	public Table_Tool tableTool;
    public Table_Measuring_Manager tableMeasuringManager;
    public Table_Turtle_Manager tableTurtleManager;
    public Table_Tracks_Manager tableTrackManager;
	public InstructionManager instMan;

	public TexttoFile ttf;

	public GameObject TableOne;
	public GameObject TableTwo;
	public GameObject TableThree;

	public GameObject Shiny;
	public GameObject Normal;

	private bool toolDone = false;
    private bool oneDone = false;
    private bool twoDone = false;
	private bool threeDone = false;
	private bool fourDone = false;
    private bool complete = false;

	[HideInInspector]
	public float toolTime;
	[HideInInspector]
	public float oneTime;
	[HideInInspector]
	public int twoWrong;
	[HideInInspector]
	public float twoTime;
	[HideInInspector]
	public int threeWrong;
	[HideInInspector]
	public float threeTime;
	[HideInInspector]
	public float totalTime;


    IEnumerator Pause()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("Main");
		//uncomment this line to change what scene is loaded after completion
		//SceneManager.LoadScene("Module_02");
    }


    // Start is called before the first frame update
    void Start()
    {
		TableOne.SetActive(false);
		TableTwo.SetActive(false);
		TableThree.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(tableTool.getTaskToolDone() == true && !toolDone)
			//tool table is finished, progress to next table
        {
			toolDone = true;
			toolTime = Time.time;
			if (!instMan.active)
			{
				instMan.togglePanel();
			}
			instMan.changePanel(9);
			instMan.taskActive = false;
			instMan.AC.playSound();
			Debug.Log("Tool Table Completed");
			//This is where the switch should take place
			//activate door
			instMan.TableOne.SetActive(true);
			//determine if you get a shiny turtle
			float shiny = Random.Range(1, 8192);
			if(shiny == 1)
            {
				Shiny.SetActive(true);
				Normal.SetActive(false);
				print("Shiny turtle!");
            }
        }
		if(tableMeasuringManager.getTask1Done() == true && !oneDone){
			//measuring table is finished, progress to next table
			oneDone = true;
			oneTime = Time.time;
			if (!instMan.active)
			{
				instMan.togglePanel();
			}
			instMan.changePanel(11);
			instMan.AC.playSound();
			instMan.taskActive = false;
			Debug.Log("Turtle Measuring Activity Completed");
			//Switch happens here
			//activate door
		}
		if (tableTurtleManager.getTask2Done() == true && !twoDone){
			//turtle ID is finished, progress to next table
			twoDone = true;
			twoTime = Time.time;
			twoWrong = tableTurtleManager.wrong;
			if (!instMan.active)
			{
				instMan.togglePanel();
			}
			instMan.changePanel(13);
			instMan.AC.playSound();
			instMan.taskActive = false;
			Debug.Log("Turtle Identification Activity Completed");
			//switch happens here
			//activate door
		}
		if (tableTrackManager.getTask3Done() == true && !threeDone){
			//tracks ID is finished, progress to next table
			threeTime = Time.time;
			threeWrong = tableTrackManager.wrong;
			if (!instMan.active)
			{
				instMan.togglePanel();
			}
			instMan.AC.playSound();
			instMan.changePanel(15);
			instMan.taskActive = false;
			Debug.Log("Tracks Identification Activity Completed");
			threeDone = true;
		}
        if(complete == false && oneDone && twoDone && threeDone)
			//all activities are finished
        {
            Debug.Log("Module 1 Activities finished");
            complete = true;
			totalTime = Time.time;
			ttf.GetMOneInfo();
			StartCoroutine(Pause());
        }
    }
	
	public void ChangeTable(GameObject newTable)
	{
		newTable.SetActive(true);
	}
}
