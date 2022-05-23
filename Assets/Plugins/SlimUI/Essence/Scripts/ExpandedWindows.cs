using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpandedWindows : MonoBehaviour {
	public bool hasNotification = false;
	public GameObject notificationIcon;
	public TMP_Text notificationText;
	public int notifications = 0;
	public SubWindowControl windowManager;

	// Use this for initialization
	void Start () {
		if(hasNotification){
			notificationIcon.SetActive(true);
			notificationText.text = "" + notifications;
		}else if(!hasNotification){
			notificationIcon.SetActive(false);
		}
	}

	public void SwitchWindowCall(int num){
		windowManager.SwitchWindow(num);

		if(hasNotification){
			hasNotification = false;
			notificationIcon.SetActive(false);
		}
	}
}
