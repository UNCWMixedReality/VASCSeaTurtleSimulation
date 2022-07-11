using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDNA : MonoBehaviour
{
	[HideInInspector]
	public bool collided;


	private void Start()
	{
		collided = false;
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log("Head collision detected");
		if (col.name == "Syringe" || col.name == "Needle")
		{
			collided = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		collided = false;
	}
}
