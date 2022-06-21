using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//keeps track of all activities in module 2
public class Activity_Manager_2_01 : MonoBehaviour
{
	public LayerDetect layerDetect;
	public EggManager eggManager;

	public Instruction_Manager_2_01 instMan;
	public AudioFeedback audiofeedback;
	public ProgressM2 PM2;

	private GameObject[] ttf;

	public int wrongBucket = 0;

	public GameObject GEggA;
	public GameObject GEggB;
	public GameObject GEggC;
	public GameObject GEggD;
	public GameObject GEggE;
	public GameObject GEggF;
	public GameObject BEggA;
	public GameObject BEggB;
	public GameObject CEggA;
	public GameObject CEggB;

	private bool oneDone = false;
	private bool twoDone = false;
	private bool threeDone = false;
	private bool fourDone = false;
	private bool fiveDone = false;
	private bool sixDone = false;
	private bool complete = false;

	public float oneTime;
	public float twoTime;
	public float threeTime;
	public float fourTime;
	public float fiveTime;
	public float sixTime;
	public float totalTime;

	IEnumerator Pause()
	{
		yield return new WaitForSeconds(8);
		SceneManager.LoadScene("Main");
	}

	// Start is called before the first frame update
	void Start()
	{
		EggToggle();
	}

	// Update is called once per frame
	void Update()
	{
		if (complete == false)
		{
			//if the nest has been dug up
			if (!oneDone && layerDetect.taskDone == true)
			{
				oneDone = true;
				audiofeedback.playCompletion();
				oneTime = Time.time;
				EggToggle();
				Debug.Log("Nest Excavation Activity Finished");
				instMan.changePanel(4);
				instMan.VP.stopVid();
				instMan.AC.playSound();
				instMan.MarkerMeasure.SetActive(true);
				instMan.taskActive = false;
				PM2.TickProgressBar();
			}
			//if the eggs have been sorted properly
			if (eggManager.taskDone == true && !twoDone)
			{
				twoDone = true;
				audiofeedback.playCompletion();
				twoTime = Time.time;
				wrongBucket = eggManager.wrongBucket;
				Debug.Log("Egg Identification Activity Finished");
				instMan.changePanel(7);
				instMan.VP.stopVid();
				instMan.AC.playSound();
				instMan.taskActive = false;
				PM2.TickProgressBar();
			}
			if (oneDone && twoDone && !complete)
			{
				complete = true;
				totalTime = Time.time;
				Debug.Log("All activities in Module 2 finished!");
				StartCoroutine(Pause());
			}


		}
	}

	//activates/deactivates eggs as necessary to prevent sequence breaking
	void EggToggle()
	{
		GEggA.SetActive(oneDone);
		GEggB.SetActive(oneDone);
		GEggC.SetActive(oneDone);
		GEggD.SetActive(oneDone);
		GEggE.SetActive(oneDone);
		GEggF.SetActive(oneDone);
		BEggA.SetActive(oneDone);
		BEggB.SetActive(oneDone);
		CEggA.SetActive(oneDone);
		CEggB.SetActive(oneDone);
	}
}