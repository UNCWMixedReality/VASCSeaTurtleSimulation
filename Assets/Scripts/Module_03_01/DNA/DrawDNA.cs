using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;



public class DrawDNA : MonoBehaviour
{
	public GameObject button;
	public Text buttonText;
	public string text;

	[HideInInspector]
	public bool collided;


	private void Start()
	{
		collided = false;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.name == "Syringe" || col.name == "Needle")
		{
			Debug.Log("Syringe collision detected");
			collided = true;
			button.GetComponent<cakeslice.Outline>().enabled = true;
			buttonText.text = text;

		}
	}

	void OnTriggerExit(Collider col)
	{
		collided = false;
		button.GetComponent<cakeslice.Outline>().enabled = false;
		buttonText.text = "";
	}
}
