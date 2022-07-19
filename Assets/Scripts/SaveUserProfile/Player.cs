using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DataCollection;


public class Player : MonoBehaviour
{
    
    [Serializable]

// Below is a class of all of the User's Information 
    public class PlayerInfo
    {
        public string Username;
        public string CharacterSelected;    
        public string CharacterNumber;

    }

// Below is a list all of all the players that have created a profile.
    [Serializable]
    public class PlayerDataList             
    {
        public List<PlayerInfo> myPlayerList = new List<PlayerInfo>();

    }

// Declare a new player and a list
    public PlayerInfo newplayer = new PlayerInfo();
    public PlayerDataList myPlayerData = new PlayerDataList();
    
// InputFields that we get user Information from
    public TMP_InputField adjName;
    public TMP_InputField nounName;
    public TMP_InputField characterName;
    public TMP_InputField characterNumber;

// Strings for what the Player name is, their character name and what number it is
    public string playerName;
    public string playerCharName;
    public string playerCharNumber;

// This number is used so that we don't add repeats of created profiles in TransferData()
    public int count;
    

// These are gameobjects use to set active aka the first 2 
// The other 2 game objects are to create new buttons with the User name and the sprite for their avatar.
    public GameObject profileBrowser;
    public GameObject interactiveMap;
    public GameObject buttonPrefab;
    public GameObject buttonParent;

// This creates a list of Buttons
    private List<GameObject> buttons = new List<GameObject>();

// This is a bunch of sprites aka the users avatar. We use these to match the avatar they chose with their user name in the login page.
    public Sprite monster1;
    public Sprite monster2;
    public Sprite monster3;
    public Sprite monster4;
    public Sprite monster5;
    public Sprite monster6;
    public Sprite monster7;
    public Sprite monster8;


// Below we save the player Progress as the new player who created a profile. Add it to the list and save it.
    public void SavePlayer()
    {
        //DcDataLogging.Student = new DataCollection.Models.Student(adjName.text, nounName.text);        <--- This is for you Blake
        newplayer.Username = playerName;
        newplayer.CharacterSelected = playerCharName;
        newplayer.CharacterNumber = playerCharNumber;

        AddToList();

        SaveSystem.SavePlayer(this);

    }

// We load in the list previously saved and all data such as username, character name, and which avatar.
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerName = data.playerDataName;
        playerCharName = data.playerDataCharName;
        playerCharNumber = data.playerDataCharNumber;

        myPlayerData = data.myPlayerDataList;

        
    }

// Add the new player to the list of profile previously created.
    public void AddToList()
    {
        myPlayerData.myPlayerList.Add(newplayer);

    }

// Get the list is used in the Player Data class inorder to ensure that our save file and load file is in the correct format.
    public PlayerDataList GetList()
    {
        return myPlayerData;
    }

    
// Here is the fun part. We get the length of the player profile list, run a for loop that obtains the username value, and creates a button for them to click on with the username text
// We also get the Character number. We convert the string to an int, and depending on what the int value is, we show that sprite above the button.
    public void transferData()
    {
        GetList();

        Debug.Log(myPlayerData.myPlayerList.Count);

        
        if (count < myPlayerData.myPlayerList.Count)
        {
            for (int i = count; i < myPlayerData.myPlayerList.Count; i++)
            {
                string playerinfo = myPlayerData.myPlayerList[i].Username;
                //Debug.Log(playerinfo);
                string monsterinfo = myPlayerData.myPlayerList[i].CharacterNumber;
                int intval = Int32.Parse(monsterinfo);

                //Create new button
                GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);

                // change the username
                newButton.GetComponent<ProfileButton>().profileText.text = playerinfo;

                buttons.Add(newButton);       

                //Display avatar
                if (intval == 1)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster1;
                }
                else if (intval == 2)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster2;
                }
                else if (intval == 3)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster3;
                }
                else if (intval == 4)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster4;
                }
                else if (intval == 5)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster5;
                }
                else if (intval == 6)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster6;
                }
                else if (intval == 7)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster7;
                }
                else if (intval == 8)
                {
                    newButton.GetComponent<ProfileButton>().profileSprite.sprite = monster8;

                }
                
                count++;

                // Create listener for buttons generated that can grab data and go to the interactive map.
                newButton.GetComponent<Button>().onClick.AddListener(() =>
                        SelectProfile(intval, playerinfo));

                buttons.Add(newButton);

            }
        }

    }
// show in the terminal the user playing and change the set active gameobjects.
    public void SelectProfile(int character_num, string username)
    {
        Debug.Log("Loaded profile - ( " + username + " )" + " with the profile picture - ( " + character_num + " )");
        profileBrowser.SetActive(false);
        interactiveMap.SetActive(true);
        /* PlayersMonster.SetActive(true);
         if(character_num == 1)
         {
             blueOct.SetActive(true);
         }
         else if(character_num == 2)
         {
             pinkOct.SetActive(true);
         }
         else if (character_num == 3)
         {
             turtle.SetActive(true);
         }
         else if (character_num == 4)          put the players character on iteractive map if we want
         {
             orangeOct.SetActive(true);
         }
         else if (character_num == 5)
         {
             jelly.SetActive(true);
         }
         else if (character_num == 6)
         {
             Dolphan.SetActive(true);
         }
         else if (character_num == 7)
         {
             stingray.SetActive(true);
         }
         else if (character_num == 8)
         {
             seahorse.SetActive(true);
         }*/

    }

}
