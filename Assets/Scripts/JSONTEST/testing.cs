using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;


public class testing : MonoBehaviour
{
    // To Access text from the file
    public TextAsset UserInfo;


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
        UnityEditor.AssetDatabase.Refresh();



    } 
}











