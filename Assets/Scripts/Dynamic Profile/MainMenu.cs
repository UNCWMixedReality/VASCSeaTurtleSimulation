using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject profileBrowser;

    public void ShowProfiles()
    {
        profileBrowser.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
