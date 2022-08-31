using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class RandomFacts : MonoBehaviour
{
    public string[] words = { "XA", "XB", "XC" };
    public TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        
        // log whatever comes out of the RandomWord string.
        string wordToDisplay = RandomWord();

        text.text = wordToDisplay;
    }

    // when you see a string function,
    // it will return a string that
    // you can use anywhere!
    private string RandomWord()
    {
        // grab a random string from the words array
        string randomWord = words[Random.Range(0, words.Length)];

        // return it (this will be the string that the script will use)
        return randomWord;
    }
}
