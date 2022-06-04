using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GroupManager : MonoBehaviour
{

    // Gameobject Connections
    public GameObject ButtonPrefab;
    public LoginStudent LoginManager;
    public Transform ContentPanel;
    public GameObject HeadsetErrorState;
    public GameObject GroupErrorState;

    // Globals
    private Tuple<bool, string> ErrorState;
    public Dictionary<string, string> players = new Dictionary<string, string>();


    async void OnEnable()
    {
        if (await APIManager.HeadsetIsConnectedToInternet())
        {
            LogManager.LogMessage("Connected to internet! pulling from server");
            await PullGroupFromServer();
        } else
        {
            LogManager.LogMessage("Not Connected to internet. Pulling from local.");
            ReadPlayersFromLocalGameSave();
        }

        PopulateContentPanelWithStudentsOrErrorState();
    }



    async Task PullGroupFromServer()
    {
        LogManager.LogMessage("Pulling values from server now");
        APIManager.Group NewGroup = new APIManager.Group();

        (bool, bool, string, APIManager.Group) results = await APIManager.RetrieveHeadsetGroupAssignmentFromServer(SystemInfo.deviceUniqueIdentifier);

        LogManager.LogMessage($"{results.Item1.ToString()} - {results.Item2.ToString()} - {results.Item3}");
        // Check if the call failed in the network layer
        if (results.Item1 is false)
        {
            LogManager.LogMessage(results.Item3, true);
        }
        // Check if call returned anything other than HTTP_200
        else if (results.Item2 is false)
        {
            if (results.Item3.Contains("registered"))
            {
                LogManager.LogMessage("Unregistered Headset!", true);
                ErrorState = new Tuple<bool, string>(true, "Headset");
            } else
            {
                LogManager.LogMessage("No assigned group!");
                ErrorState = new Tuple<bool, string>(true, "Group");
            }
        }
        // At this point, we assume everything has succeded
        else
        {
            LogManager.LogMessage(results.Item3);
            NewGroup = results.Item4;
            foreach (APIManager.Student member in NewGroup.members)
            {
                players[member.name] = member.id.ToString();
            }

            WritePlayersToLocalGameSave();

            ErrorState = new Tuple<bool, string>(false, null);
        }

    }


    // ===================================================================== //
    // ==================== PlayerPrefs Management ========================= //
    void WritePlayersToLocalGameSave()
    {
        List<String> StringToSave = new List<String>();

        foreach (String key in players.Keys)
        {
            StringToSave.Add($"{key}-{players[key].ToString()}");
        }

        string PlayerList = String.Join(",", StringToSave.ToArray());
        LogManager.LogMessage("Player List: " + PlayerList);

        PlayerPrefs.SetString("Players", PlayerList);
        PlayerPrefs.Save();
    }

    void ReadPlayersFromLocalGameSave()
    {
        Debug.Log("Reading from local player save");
        if (PlayerPrefs.HasKey("Players")) {
            String StoredString = PlayerPrefs.GetString("Players");
            String[] EachPlayer = StoredString.Split(',');

            foreach (string Player in EachPlayer)
            {
                String[] PlayerInfo = Player.Split('-');
                players[PlayerInfo[0]] = PlayerInfo[1];

            }

        }
        else
        {
            LogManager.LogMessage("Unable To find the players stored in PlayerPrefs!", true);
        }
        
    }

    // ===================================================================== //
    // ==================== Instantiation Tasks ============================ //
    void NewStudentButton(string StudentName)
    {
        GameObject NewButton = Instantiate(ButtonPrefab, ContentPanel);
        NewButton.GetComponentInChildren<Text>().text = StudentName;
    }

    void PopulateContentPanelWithStudentsOrErrorState()
    {
        if ((ErrorState != null) && (ErrorState.Item1))
        {
            if (ErrorState.Item2.Contains("set"))
            {
                HeadsetErrorState.SetActive(true);
            } else
            {
                GroupErrorState.SetActive(true);
            }
        }
        else
        {
            foreach (string Student in players.Keys)
            {
                NewStudentButton(Student);
            }
        }
        
    }
}
