using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnabled : MonoBehaviour {
	public bool startEnabled = true;

	void Start () {
		if(startEnabled){
			GetComponent<Animator>().SetTrigger("Fade");
		}
	}
}
