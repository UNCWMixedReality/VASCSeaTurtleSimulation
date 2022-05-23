using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCheck : MonoBehaviour {
	[Tooltip("If enabled, the Animator attached will start in closed position.")]
	public bool startDisabled = false;
	[Tooltip("If animator window is already opened, can't open until this variable is true.")]
	bool canSwitch = false; // if the window is not open yet, then trigger fade
	[Tooltip("Is the animator currently animated to open?")]
	bool currentlyOpen = false; // is the window currently open?
	[Tooltip("If using a menu button, this is the button that activates the animator window.")]
	public Button menuButton; // the button that activates the menu

	void Start () {
		if(!startDisabled){ // if start enabled is on, you can't switch to that same menu
			canSwitch = false;
			GetComponent<Animator>().SetTrigger("Fade");
			if(menuButton != null){
				menuButton.interactable = false;
			}
			currentlyOpen = true;
		}else if(startDisabled){
			canSwitch = true;
		}
	}

	// opening window, so disable button and state that it's open
	public void NoSwitch(){
		canSwitch = false;
		menuButton.interactable = false;
	}

	public void YesSwitch(){
		canSwitch = true;
		menuButton.interactable = true;
	}

	// if window is open, close, otherwise don't do anything
	public void ShouldWindowClose(){
		if(currentlyOpen){
			GetComponent<Animator>().SetTrigger("Fade");
		}
	}

	public void Switch(){
		if(canSwitch){
			menuButton.interactable = false;
			currentlyOpen = true;
		}else if(!canSwitch){
			currentlyOpen = false;
		}
	}
}
