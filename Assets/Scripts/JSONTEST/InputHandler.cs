using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


public class InputHandler : MonoBehaviour
{

    public TMP_InputField adjInputField;
    public TMP_InputField nounInputField;
    public TMP_InputField characterInputField;
    public TMP_InputField whichCharacterNumber;

    [System.Serializable]
    public class Player
    {
        public string Name;
        public string Character;
        public string CharacterNumber;

        public void saveJason()
        {
            
            string json = JsonUtility.ToJson(thePlayerList);
            File.AppendAllText(Application.dataPath + "/userinfo.json", json);

        }

        [System.Serializable]
        public class PlayerList
        {

            public Player[] myPlayer;
        }

        public PlayerList thePlayerList = new PlayerList();
        public Player [] myPlayer = new Player[5];


        
    }
}