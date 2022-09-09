using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//handles egg relocation
public class EggRelocation : MonoBehaviour
{
	public GameObject egg;
	
	public bool placed = false;

	public AudioFeedback audiofeedback;
	
	//locks the eggs in position once they've been placed
	public void OnTriggerEnter(Collider collider){
		if(collider.tag == "Placeholder")
        {
			egg.transform.position = collider.transform.position;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			//collider.transform.position = new Vector3(0, 0f, 0);
			collider.gameObject.SetActive(false);
			placed = true;
			audiofeedback.playGood();
		}
	}
}
