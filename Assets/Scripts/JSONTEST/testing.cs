using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;
using OVRSimpleJSON;
using UnityEngine.UI;
using DataCollection;

public class testing : MonoBehaviour
{
    // To Access text from the file
    public TextAsset UserInfo;
    
    public GameObject buttonPrefab;
    public GameObject buttonParent;
    



    public Sprite monster1;
    public Sprite monster2;
    public Sprite monster3;
    public Sprite monster4;
    public Sprite monster5;
    public Sprite monster6;
    public Sprite monster7;
    public Sprite monster8;

    public GameObject mainMenu;
    // storing all game objects created here to destroy() later, otherwise they will keep replicating
    private int count;




    // Everytime we make a selection we make sure that we update the list so that it can be seen later for login selection.
    public void Update()
    {
        myPlayerData = JsonUtility.FromJson<PlayerData>(UserInfo.text);
    }

    [System.Serializable]
    public class Player
    {
        public string Username;
        public string CharacterSelected;   // create a player
        public string CharacterNumber;

    }

    [System.Serializable]
    public class PlayerData             // create a list of players
    {
        public Player[] player;

    }



    // create new player data
    public PlayerData myPlayerData = new PlayerData();


    // Access user selection for name choice, what character they want to be, and the number that character is.
    public TMP_InputField adjname;
    public TMP_InputField nounname;
    public TMP_InputField charactername;
    public TMP_InputField whatNum;

    // On start load all previous data(if any) 
    public void Start()
    {
        if (File.Exists(Application.dataPath + "/UserInfo.txt"))
        {
            myPlayerData = JsonUtility.FromJson<PlayerData>(UserInfo.text);
        }

        
    }
    



    // on confirm selection in "Create Profile" --> "UsernameSelect" --> "Confirm" gameobject
    public void getData()
    {
        // If the file doesnt exist create one 
        if (!File.Exists(Application.dataPath + "/UserInfo.txt"))
        {
            // Get all of the players choices.

            Player player = new Player();
            DcDataLogging.Student = new DataCollection.Models.Student(adjname.text, nounname.text); //
            string username = adjname.text + " " + nounname.text;
            string CharacterSelected = charactername.text;
            string CharacterNumber = whatNum.text;

            // Write to the File username, character, character number 

            File.WriteAllText(
            Application.dataPath + "/UserInfo.txt", "{\n" + "\"player\"" + ":[\n" + "{"
            + "\"Username\":" + "\"" + username + "\"" + "," +

            "\"CharacterSelected\":" + "\"" + CharacterSelected + "\"" + "," +

            "\"CharacterNumber\":" + "\"" + CharacterNumber + "\"" + "}]}");

        }

        // File allready exists so were going to add to it
        else
        {
            Player player = new Player();
            string username = adjname.text + " " + nounname.text;
            string CharacterSelected = charactername.text;
            string CharacterNumber = whatNum.text;

            // Here we get rid of the last 2 characters "]}" because we are adding something to the list. We also add a comma for Format

            string fileContent = File.ReadAllText(Application.dataPath + "/UserInfo.txt");
            fileContent = fileContent.Remove(fileContent.Length - 2) + ",";
            File.WriteAllText(Application.dataPath + "/UserInfo.txt", fileContent);

            // Write to the file again

            File.AppendAllText(
            Application.dataPath + "/UserInfo.txt", "{"
            + "\"Username\":" + "\"" + username + "\"" + "," +

            "\"CharacterSelected\":" + "\"" + CharacterSelected + "\"" + "," +

            "\"CharacterNumber\":" + "\"" + CharacterNumber + "\"" + "}]}");


        }

        
        // Refresh everything so that our file and list are up to date with each other in real time.
        //UnityEditor.AssetDatabase.Refresh();

       

    }
    public void transferData()
    {

        if(count< myPlayerData.player.Length)
        {
            for (int i = count; i < myPlayerData.player.Length; i++)
                    {

                        string playerinfo = JsonConvert.SerializeObject(myPlayerData.player[i]);
                        string monsterinfo = JsonConvert.SerializeObject(myPlayerData.player[i]);
                        playerinfo = playerinfo.Remove(0, 13);
                        int monIndex = monsterinfo.LastIndexOf("}") - 2;

                        while (playerinfo.Contains("\"") == true)
                        {
                            int index = playerinfo.LastIndexOf("\"");
                            if (index > 0)
                            {
                                playerinfo = playerinfo.Substring(0, index);
                            }
                        }
                        GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
                        // change the username
                        newButton.GetComponent<ProfileButton>().profileText.text = playerinfo;

                        char whichMonster = monsterinfo[monIndex];
                        int intval = (int)char.GetNumericValue(whichMonster);

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

                    } 
        }

        
    }
     public void SelectProfile(int character_num, string username)
        {
            

        }


    
    public void BackButton()
    {
        
      
           
    }
}











