using System;
using System.Collections;
using System.Collections.Generic;
using Altimit.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Build.Content;


public class ProfileBrowser : MonoBehaviour
{
    // these are used to assign gameobjects within the unity editor
    // button prefab is a a prefab of a button
    public GameObject buttonPrefab;
    /* when using the scroll view method attach "scroll view > viewport > content" as the buttonParent otherwise it wont
     work */
    public GameObject buttonParent;
    
    // mainMenu is the game object you want BackButton to take you to
    public GameObject mainMenu;
    // storing all game objects created here to destroy() later, otherwise they will keep replicating
    private List<GameObject> buttons = new List<GameObject>();
    private void OnEnable()
    {
        {
            for (int i = 0; i < ProfileManager.Instance.userProfile.Length; i++)
            {
                // for some reason 'i' was over stepping the bounds of the array so the 'j' variable fixes that issue
                // it works but i couldn't tell you why
                int j = i;
                
                GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
                // change the username
                newButton.GetComponent<ProfileButton>().profileText.text = ProfileManager.Instance.userProfile[j].username;
                
                // change the picture
                if (ProfileManager.Instance.userProfile[j].character_num == 0)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = ProfileManager.Instance.monster0;
                }
                else if (ProfileManager.Instance.userProfile[j].character_num == 1)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = ProfileManager.Instance.monster1;
                }
                else if (ProfileManager.Instance.userProfile[j].character_num == 2)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = ProfileManager.Instance.monster2;
                }
                else if (ProfileManager.Instance.userProfile[j].character_num == 3)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = ProfileManager.Instance.monster3;
                }
                else
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = ProfileManager.Instance.monster4;
                }
                
                // this activates whenever a profile button is clicked, and sends the profile information of the button clicked
                // if you want something to happen put it in SelectProfile()
                newButton.GetComponent<Button>().onClick.AddListener(() => 
                    SelectProfile(ProfileManager.Instance.userProfile[j].character_num,ProfileManager.Instance.userProfile[j].username));
                buttons.Add(newButton);
            }
        }
    }

    private void SelectProfile(int character_num, string username)
    {
        // here is where you can make buttons do stuff
        Debug.Log("Loaded profile - ( " + username + " )" + " with the profile picture - ( " + character_num + " )");


    }

    public void BackButton()
    {
        /*
         this method is very important if the back button on the profile browser is not assigned this function then
         bad things will probably happen
        */
        GameObject[] ProfileButtons = buttons.ToArray();
        mainMenu.SetActive(true);
        foreach (GameObject profile in ProfileButtons)
        {
            Destroy(profile);
            this.gameObject.SetActive(false);
        }
    }
}
