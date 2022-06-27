using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class user : MonoBehaviour
{
    public TMP_InputField adjName;
    public TMP_InputField nounName;
    

    //public gameObject character;
    public void Onclick()
    {
        Debug.Log("Starting");

        
        string json = File.ReadAllText(Application.dataPath);
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("UserName: " + loadedPlayerData.username);
    }
}

   public class PlayerData
    {

        public TMP_InputField adjName;
        public TMP_InputField nounName;
        public string username;
        //public gameObject character;
        public void Start()
    {
        username = adjName.text + " " + nounName.text;
        
    }

    }
