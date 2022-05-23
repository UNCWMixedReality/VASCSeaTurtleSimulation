using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoremanager : MonoBehaviour
{
	public static int score;        // The player's score.
	
	
	Text text;                      // Reference to the Text component.
	
	
	void Awake ()
	{
		// Set up the reference.
		text = GetComponent <Text> ();
		
		// Reset the score.
		score = 989959;
	}
	
	
	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = "" + score;
		score ++;
	}
}