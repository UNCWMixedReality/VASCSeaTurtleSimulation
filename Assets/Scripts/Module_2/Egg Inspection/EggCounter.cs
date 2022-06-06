using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggCounter : MonoBehaviour
{
    //keeps track of how many eggs have been sorted and displays this to the player

    public GameObject Goodtext;
    public GameObject Badtext;
    public GameObject Totaltext;

    private Text gtext;
    private Text btext;
    private Text ttext;
    private int goodCount;
    private int badCount;
    private int totalCount;

    // Start is called before the first frame update
    void Start()
    {
        gtext = Goodtext.GetComponent<Text>();
        btext = Badtext.GetComponent<Text>();
        ttext = Totaltext.GetComponent<Text>();
        goodCount = 0;
        badCount = 0;
        totalCount = 0;
    }

    public void increment(int type)
    {
        if(type == 1)
        {
            goodCount++;
            gtext.text = goodCount.ToString();
        }
        else
        {
            badCount++;
            btext.text = badCount.ToString();
        }
        totalCount++;
        ttext.text = "Total Eggs: " + totalCount + "/10";
    }
}
