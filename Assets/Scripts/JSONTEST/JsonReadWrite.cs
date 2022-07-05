using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class JsonReadWrite : MonoBehaviour
{
    public TMP_InputField adjInputField;
    public TMP_InputField nounInputField;
    public TMP_InputField characterInputField;
    public TMP_InputField whichCharacterNumber;

    private string path = "";
    private string persistentPath = "";
    private string filename = "/userInfo.json";

    Dictionary<int, savefile> player = new Dictionary<int, savefile>();
    

    private savefile playerdata;
    List<savefile[]> users = new List<savefile[]>();


    public void onclick()
    {
        CreatePlayerData();
        SetPath();
        SaveToJson();
    }

    private void CreatePlayerData()

    {
        
        playerdata = new savefile(adjInputField.text + " " + nounInputField.text, characterInputField.text,
            whichCharacterNumber.text);
        
        player.Add(1, playerdata);
        Debug.Log(player);
        
    }

    private void SetPath()
    {
        path = Application.dataPath + "/userInfo.json";
        persistentPath = Application.dataPath + "/userInfo.json";
    }
    public void SaveToJson()
    {
        if (!ReadFile(path))
        {
            string savePath = path;

            Debug.Log("Saving Data at " + savePath);
            string json = JsonUtility.ToJson(player);
            File.AppendAllText(Application.dataPath + "/userinfo.json", json);
        }

        else
        {

            string savePath = path;
            string json = JsonUtility.ToJson(player);

            File.AppendAllText(Application.dataPath + "/userinfo.json", json);

        }

    }

    public void LoadFromJson()
    {

        using StreamReader reader = new StreamReader(path);

        string json = reader.ReadToEnd();

        savefile data = JsonUtility.FromJson<savefile>(json);
        Debug.Log(data.ToString());

       
    }

    private bool ReadFile(string filename)
    {
        string path = Application.dataPath + "/userInfo.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return true;
            }
        }
        else
        {

            return false;
        }
    }
 
}
