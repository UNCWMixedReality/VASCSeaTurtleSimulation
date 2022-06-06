using UnityEngine;
using System.Collections;

public class clickbutton : MonoBehaviour {
	
	public void ClickObtions() {
		Debug.Log ("Options");
		Application.LoadLevel (5);
	}
	
	public void ClickPlay() {
		Debug.Log ("play");
		Application.LoadLevel (4);
	}
	
	public void ClickSound() {
		Debug.Log ("Sound");
		//Application.LoadLevel (1);
	}

	public void ClickMenu() {
		Debug.Log ("Menu");
		Application.LoadLevel (2);
	}

	public void ClickGame() {
		Debug.Log ("Game");
		Application.LoadLevel (1);
	}

	public void ClickReturn() {
		Debug.Log ("Return");
		Application.LoadLevel (0);
	}
	public void ClickExit() {
		Debug.Log ("Exit");
		Application.LoadLevel (0);
	}

	public void ClickRestart() {
		Debug.Log ("Restart");
		Application.LoadLevel (1);
	}

	public void ClickLoose() {
		Debug.Log ("Loose");
		Application.LoadLevel (3);
	}

	public void ClickRewards() {
		Debug.Log ("Rewards");
		Application.LoadLevel (6);
	}

	public void ClickLogin() {
		Debug.Log ("Login");
		Application.LoadLevel (7);
	}

	public void ClickCharacter() {
		Debug.Log ("Character");
		Application.LoadLevel (8);
	}
}

