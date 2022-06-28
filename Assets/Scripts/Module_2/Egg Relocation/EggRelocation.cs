using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EggRelocation : MonoBehaviour
{
	/*
	 * When an egg is placed in a collider, this snaps the egg into position and sets placed to true
	 */

	private GameObject placeholder;
	public AudioFeedback audiofeedback;
	
	//locks the eggs in position once they've been placed
	public void OnTriggerEnter(Collider collider){
		if(collider.tag == "Placeholder")
        {
			placeholder = collider.gameObject;
			int eggsPlaced = placeholder.GetComponent<RelocationTracker>().eggsPlaced;

			//set the egg position to the position of an attach point, just cycle through each attach point [0, 5] inclusive based on how many eggs have already been placed
			transform.position = placeholder.transform.GetChild(eggsPlaced).position;
			//update number of eggs placed 
			placeholder.GetComponent<RelocationTracker>().UpdateEggCount();
			//freeze egg in place
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			
			audiofeedback.playGood();
		}
	}
}
