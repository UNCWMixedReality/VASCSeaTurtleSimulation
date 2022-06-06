using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityStart : MonoBehaviour
{
	//not currently used
	public InstructionManager instMan;
	public int value;
	
	private bool active = false;
	
    void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("MainCamera") && !active){
			Debug.Log("Activity Started");
			active= !active;
			instMan.changePanel(value);
		}
	}
}
