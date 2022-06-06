using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Tool : MonoBehaviour
{
    //tracks the sequence of tasks for the tool table (using calipers to measure a test tube and tape measure to measure a clipboard)

    public GameObject calipers;
    public GameObject tm;

    public SpriteRenderer Container;
    public SpriteRenderer Clipboard;

    public InstructionManager instMan;
    public Activity_Manager activityManager;
    public AudioFeedback audiofeedback;
    public Progress prog;

    private bool taskToolDone = false;
    private bool containerDone = false;
    private bool clipboardDone = false;
    private Color arrowGreen = new Color(0, 1, 0, 1);

    void Start()
    {
        tm.SetActive(false);
    }

    void Update()
    {
        if(Container.color == arrowGreen && !containerDone)
        {
            containerDone = true;
            instMan.StartCoroutine(instMan.Wait(2, 6));
            instMan.AC.playSound();
            prog.TickProgressBar();
        }
        if (Clipboard.color == arrowGreen && !clipboardDone)
        {
            clipboardDone = true;
            prog.TickProgressBar();
        }
        if (containerDone && clipboardDone && !taskToolDone)
        {
            activityManager.ChangeTable(activityManager.TableOne);
            taskToolDone = true;
            /*if (!instMan.active)
            {
                instMan.togglePanel();
            }
            instMan.changePanel(9);
            instMan.AC.playSound(5);
            instMan.TableOne.SetActive(true);*/
            audiofeedback.playCompletion();
            calipers.SetActive(false);
            tm.SetActive(false);
        }
    }

    public bool getTaskToolDone()
    {
        return taskToolDone;
    }
}
