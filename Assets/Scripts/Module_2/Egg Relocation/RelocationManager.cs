using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocationManager : MonoBehaviour
{
	//tracks if all eggs have been relocated
	public bool taskDone = false;
	public EggRelocation egg1;
	public EggRelocation egg2;
	public EggRelocation egg3;
	public EggRelocation egg4;
	public EggRelocation egg5;
	public EggRelocation egg6;
	/*
	void Update()
    {
		if(!taskDone && egg1.placed && egg2.placed && egg3.placed && egg4.placed && egg5.placed && egg6.placed)
        {
			taskDone = true;
        }
    }
	*/
}

