using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    //once upon a time we were going to let players select which animal shows up on their instruction panel
    //this script handles choosing the character in the main menu

    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;
    public GameObject char5;

    public AudioSource monster1;
    public AudioSource monster2;
    public AudioSource monster3;
    public AudioSource monster4;
    public AudioSource monster5;

    private int current = 0;
    private GameObject[] charArray = new GameObject[5];
    private AudioSource[] monsterArray = new AudioSource[5];

    void Start()
    {
        charArray[0] = char1;
        charArray[1] = char2;
        charArray[2] = char3;
        charArray[3] = char4;
        charArray[4] = char5;
        //Debug.Log(charArray[0] + " " + charArray[1] + " " +  charArray[2] + " " + charArray[3] + " " + charArray[4]);
        monsterArray[0] = monster1;
        monsterArray[1] = monster2;
        monsterArray[2] = monster3;
        monsterArray[3] = monster4;
        monsterArray[4] = monster5;
        //Debug.Log(monsterArray[0] + " " + monsterArray[1] + " " + monsterArray[2] + " " + monsterArray[3] + " " + monsterArray[4]);
    }

    public void nextChar()
    {
        if (current == 4)
        {
            charArray[current].SetActive(false);
            current = 0;
            charArray[current].SetActive(true);
            monsterArray[current].Play();
        }
        else
        {
            charArray[current].SetActive(false);
            current++;
            charArray[current].SetActive(true);
            monsterArray[current].Play();
        }
    }

    public void prevChar()
    {
        if (current == 0)
        {
            charArray[current].SetActive(false);
            current = 4;
            charArray[current].SetActive(true);
            monsterArray[current].Play();
        }
        else
        {
            charArray[current].SetActive(false);
            current--;
            charArray[current].SetActive(true);
            monsterArray[current].Play();
        }
    }

    public int GetCharacterID()
    {
        return current;
    }

    public void SaveCharacterID()
    {
        Debug.Log("Function not written yet, but will store the selected character to the persistent database to be used throughout the different scenes.");
    }
}
