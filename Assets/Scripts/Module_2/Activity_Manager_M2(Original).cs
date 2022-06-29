
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//keeps track of all activities in module 2
public class Activity_Manager_02_02 : MonoBehaviour
{
	public LayerDetect layerDetect;
	public EggManager eggManager;
	public RelocationManager eggRelocation;
	public NestCovering nestSand;
	public NestCage nestManager;
	public NestSign nestSign;
	public TexttoFile backup;
	public InstructionManagerM2 instMan;
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
	public GameObject PSign;
	public GameObject WholeSign;

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
		ttf = GameObject.FindGameObjectsWithTag("Data");
		Debug.Log(ttf + " is the gameobject for the output data");
		if(ttf.Length == 0)
		{
			Debug.Log("no data output found");
		}
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
				instMan.MarkerReplace.SetActive(true);
				instMan.taskActive = false;
				PM2.TickProgressBar();
			}
			//if the eggs have been relocated properly
			if (eggRelocation.taskDone == true && !threeDone)
			{
				threeDone = true;
				audiofeedback.playCompletion();
				threeTime = Time.time;
				Debug.Log("Egg Relocation Activity Finished");
				instMan.changePanel(9);
				instMan.VP.stopVid();
				instMan.AC.playSound();
				instMan.MarkerDig.SetActive(true);
				instMan.taskActive = false;
				PM2.TickProgressBar();
			}
			//if sand has been moved over the nest
			/*
			if (nestSand.isCovered == true && !fourDone){
				fourDone = true;
				audiofeedback.playCompletion();
				fourTime = Time.time;
				Debug.Log("Nest Covering Activity Finished");
				instMan.changePanel(11);
				instMan.VP.stopVid();
				instMan.AC.playSound();
				instMan.MarkerCover.SetActive(true);
				instMan.taskActive = false;
				PM2.TickProgressBar();
			}
			
			//if the cage has been placed over the nest
			if (nestManager.taskDone ==  true && !fiveDone){
				fiveDone = true;
				audiofeedback.playCompletion();
				fiveTime = Time.time;
				Debug.Log("Nest Protection Activity Finished");
				instMan.changePanel(13);
				instMan.AC.playSound();
				instMan.MarkerSign.SetActive(true);
				PSign.SetActive(true);
				instMan.taskActive = false;
				PM2.TickProgressBar();
			}
			
			//if the sign has been placed around the nest
			if (nestSign.taskDone == true && !sixDone)
            {
				sixDone = true;
				WholeSign.SetActive(true);
				audiofeedback.playCompletion();
				sixTime = Time.time;
				Debug.Log("Nest Sign Activity Finished");
				instMan.changePanel(15);
				instMan.AC.playSound();
				instMan.taskActive = false;
				PM2.TickProgressBar();
			}*/
			//if all tasks have been completed
			if (oneDone && twoDone && threeDone && fourDone && fiveDone && sixDone && !complete)
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