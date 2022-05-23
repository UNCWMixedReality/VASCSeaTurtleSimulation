using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script By: Cameron Detig 10/02/2020
//Determines when the measuring activity is complete and switches to the next table

public class Table_Measuring_Manager : MonoBehaviour
{
    public Image backFinsCheck;
    public Image frontFinsCheck;
    public Image ShellCheck;
	
	public InstructionManager instMan;
    public AudioFeedback audiofeedback;
    public Activity_Manager activityManager;

    private bool task1Done = false;

    void Update()
    {
        if (backFinsCheck.color == new Color(1, 1, 1, 1) &&
            frontFinsCheck.color == new Color(1, 1, 1, 1) &&
            ShellCheck.color == new Color(1, 1, 1, 1) && !task1Done)
        {
			activityManager.ChangeTable(activityManager.TableTwo);
            task1Done = true;
            /*if(!instMan.active){
				instMan.togglePanel();
			}
			instMan.changePanel(11);
            instMan.AC.playSound(7);
            instMan.taskActive = false;*/
            audiofeedback.playCompletion();
        }
    }

    public bool getTask1Done()
    {
        return task1Done;
    }
}
