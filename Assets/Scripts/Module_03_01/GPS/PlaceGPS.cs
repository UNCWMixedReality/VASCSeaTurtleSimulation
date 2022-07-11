using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;

public class PlaceGPS : MonoBehaviour
{
	public TaskManagerM3_1 taskMan;

	void OnTriggerEnter(Collider col)
	{
		Debug.Log("collision detected");
		if (col.name == "GPS")
        {
			col.transform.position = transform.position;
			col.transform.rotation = transform.rotation;
			col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			col.GetComponent<DcGrabInteractable>().enabled = false;

			gameObject.SetActive(false);

			taskMan.MarkTaskCompletion(5);

		}
	}
}
