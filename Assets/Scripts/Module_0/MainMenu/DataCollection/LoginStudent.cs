using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using DataCollection.Models;
using UnityEngine.UI;

public class LoginStudent : MonoBehaviour
{

    public Dictionary<string, string> Players = new Dictionary<string, string>();
    GroupManager PlayerStore;

    public void Login()
    {
        PlayerStore = GameObject.FindGameObjectWithTag("GroupManager").GetComponent<GroupManager>();
        string SelectedName = GetComponent<Text>().text;
        string PlayerID;

        LogManager.LogMessage($"{SelectedName} + {Players.ToString()}");
        foreach (string key in PlayerStore.players.Keys)
        {
            LogManager.LogMessage($"{key} - {PlayerStore.players[key]}");
        }

        try
        {
            PlayerID = PlayerStore.players[SelectedName];
        }
        catch (KeyNotFoundException _)
        {
            PlayerID = "Unregistered Student";
        }

        DcDataLogging.Student = new Student(PlayerID, null);



    }

    public void BeginSession()
    {
        DcDataLogging.BeginSession();
    }

}
