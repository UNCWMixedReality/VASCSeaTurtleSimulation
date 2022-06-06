using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardsManager : MonoBehaviour
{
	private int count;

	Text text;                     
		
	void Awake ()
	{

		text = GetComponent <Text> ();
		count = 1;
	}
	
	void Update ()
	{
		text.text = count + "%";
		count ++;
	}
}