using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionIntializer : MonoBehaviour
{
	// Start is called before the first frame update
	/*
	 * This whole script is really just a start method
	 * 
	 * When the scene is loaded, this performs all the set up for the progression system
	 * and plays/sets the introductory dialogue/instructions. 
	 */

	public InstructionUpdater InstrUpdater;
	public New_Activity_Manager ActivityMan;
	public TaskManager taskMan;

	public GameObject EggPanel;
	public GameObject MarkerExcavate;
	public GameObject MarkerMeasure;

    void Start()
    {

		EggPanel.SetActive(false);
		MarkerExcavate.SetActive(false);
		MarkerMeasure.SetActive(false);

		//Run the intial instructions
		//I'm gonna change this so it only needs to be called once.
		StartCoroutine(InstrUpdater.RunInstructions());
		StartCoroutine(InstrUpdater.RunInstructions());
	}
}
