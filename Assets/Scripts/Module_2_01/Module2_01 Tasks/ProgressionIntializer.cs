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

	public GameObject EggPanel;
	public GameObject MarkerExcavate;
	public GameObject MarkerMeasure;
	public bool taskActive;
	public int current;
	public Text text;
	public GameObject TextBox;
	public string[] instructions = new string[8];

    void Start()
    {

		EggPanel.SetActive(false);
		MarkerExcavate.SetActive(false);
		MarkerMeasure.SetActive(false);

		//begin with welcome bits
		//play audio clips
		StartCoroutine(Wait(3, 1));
		StartCoroutine(Wait(7, 2));
	}

    // Update is called once per frame
    public IEnumerator Wait(int a, int b)
    {
		yield return new WaitForSeconds(a);
	}
}
