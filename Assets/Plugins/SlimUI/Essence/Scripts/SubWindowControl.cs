using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWindowControl : MonoBehaviour {

	public GameObject[] window;

	void Start(){ // disable all the windows at start
		for(int i = 0; i < window.Length; i++)
        {
           window[i].SetActive(false);
        }

		window[0].SetActive(true); // default to the first window always on start
	}

	public void SwitchWindow(int num){
		for(int i = 0; i < window.Length; i++)
        {
           window[i].SetActive(false);
        }

		window[num].SetActive(true);
	}
}
