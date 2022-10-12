using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggSortManager : MonoBehaviour
{
    public TaskManager taskMan;

    // Text objects for counting eggs sorted
    public Text goodEggText;
    public Text badEggText;
    public Text totalEggText;

    // base number of good/bad eggs in scene
    public int numGoodEggs;
    public int numBadEggs;

    // keeps track of total num eggs sorted
    private int goodEggsDone;
    private int badEggsDone;

    // Start is called before the first frame update
    void Start()
    {
        goodEggsDone = 0;
        badEggsDone = 0;
        goodEggText.text = "0";
        badEggText.text = "0";
    }

    // increments the number of good eggs sorted then checks if all eggs are sorted
    public void IncrementGoodEggs()
    {
        goodEggsDone++;
        goodEggText.text = goodEggsDone.ToString();
        CheckDone();
    }

    // increments number of bad eggs sorted and checks if all eggs are sorted
    public void IncrementBadEggs()
    {
        badEggsDone++;
        badEggText.text = badEggsDone.ToString();
        CheckDone();
    }

    public void CheckDone()
    {
        totalEggText.text = "Total Eggs: " + (goodEggsDone + badEggsDone).ToString() + "/" + (numGoodEggs + numBadEggs).ToString();
        if (goodEggsDone == numGoodEggs && badEggsDone == numBadEggs)
        {
            taskMan.MarkTaskCompletion();
            Debug.Log("sorting done");
        }
    }
}
