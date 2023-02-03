using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    //this script handles choosing the character in the main menu

    //different .png models of the characters
    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;
    public GameObject char5;
    


    //different audio files for each monster
    public AudioSource monster1;
    public AudioSource monster2;
    public AudioSource monster3;
    public AudioSource monster4;
    public AudioSource monster5;
    

    public GameObject playerChar;

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
        //set the current object to inactive, switch to next, and set to active
        charArray[current].SetActive(false);
        current += 1;
        current %= 5;
        charArray[current].SetActive(true);
        monsterArray[current].Play();
    }

    public void prevChar()
    {
        //set the current object to inactive, switch to prev, and set to active
        Debug.Log("Current is" + current);
        charArray[current].SetActive(false);
        current -= 1;
        Debug.Log("Current is" + current);
        current = 5 - (-(current) % 5);
        Debug.Log("Current is" + current);

        charArray[current].SetActive(true);
        monsterArray[current].Play();
    }

    public void SaveCharacterID()
    {
        playerChar.GetComponent<Image>().sprite = charArray[current].GetComponent<Image>().sprite;
    }
}
