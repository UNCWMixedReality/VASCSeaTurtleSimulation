using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class PlacePaste : MonoBehaviour
{
	public TaskManagerM3_1 taskMan;
	public Material pasteMaterial;

	void OnTriggerEnter(Collider col)
	{
		Debug.Log("collision detected");
		if (col.name == "Shovel")
		{
			col.transform.GetChild(6).gameObject.SetActive(false);
			gameObject.GetComponent<MeshRenderer>().material = pasteMaterial;
			gameObject.GetComponent<Outline>().enabled = false;

			taskMan.MarkTaskCompletion(10);

		}
	}
}

