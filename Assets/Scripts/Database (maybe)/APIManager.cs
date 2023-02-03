//#define DEV

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// APIManager aims to abstract away the details of the api from other developers
/// It offers static functions to check for network connectivity, send data back home, 
/// and request the registration and group assignment status of a given headset.
/// </summary>
public class APIManager : MonoBehaviour
{
    [Serializable]
    public class Headset
    {
        public string device_id;
        public string device_nickname;
        public int org_id;

        public Headset(string DeviceID)
        {
            device_id = DeviceID;
        }
    }

    [Serializable]
    public class Student
    {
        public int id;
        public string name;
        public int course_id;
    }

    [Serializable]
    public class Group
    {
        public int course_id;
        public int headset_id;
        public List<Student> members;
    }

#if DEV
    static string API_KEY = "5geExWWA.quPbv9Mtup3ARYGZlVDc5SpRPaogsmim";
    static string API_ENDPOINT = "http://localhost:8009/api/v1/";
#else
    static string API_KEY = "5gjPQ5pR.0qkIOviB2jL37e1AUi2MLBx36FVPlqUS";
    static string API_ENDPOINT = "https://vr.uncw.edu/VASC/api/v1/";
#endif
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ===================================================================== //
    // ======================= API Abstractions ============================ //

    /// <summary>
    /// A simple check to investigate whether or not the system has internet access. Pings UNCW.edu 
    /// as we assume if UNCW.edu is up, there is most likely also access to our vr server.
    /// </summary>
    /// <returns>
    /// Task<bool> representing whether or not the connection attempt succeded
    /// </returns>
    // https://answers.unity.com/questions/567497/how-to-100-check-internet-availability.html?childToView=744803#answer-744803
    public static async Task<bool> HeadsetIsConnectedToInternet()
    {
        using (var www = UnityWebRequest.Get("uncw.edu"))
        {
            www.timeout = 5;
            www.SendWebRequest();

            while (!www.isDone)
                await Task.Delay(100);

            if (www.responseCode == 200)
            {
                return true;
            } else
            {
                LogManager.LogMessage($"Failed Network Check Response Code: {www.responseCode}");
                return false;
            }
            
        }
    }


    /// <summary>
    /// Given the headset's unique ID, attempts to register the headset with the VASC Teacher Portal
    /// </summary>
    /// <param name="DeviceID">The Unique Device Identifier for the currently running hardware</param>
    /// <returns>
    /// Task<(bool, bool, string)>:
    ///     bool Item1:     Whether the request was succsefully sent or not (HTTP_200 v. HTTP_4/5xx)
    ///     bool Item2:     Whether the headset registration succedded
    ///     string Item3:   The message from the server
    /// </returns>
    public static async Task<(bool, bool, string)> RegisterHeadset(string DeviceID)
    {
        string HeadsetRegistrationEndpoint = $"{API_ENDPOINT}headset/";
        string NewHeadset = JsonUtility.ToJson(new Headset(DeviceID));

        (bool, string) results = await GenericPostRequest(HeadsetRegistrationEndpoint, NewHeadset);

        return (results.Item1, !results.Item1, results.Item2);
    }


    /// <summary>
    /// Given the headset's Unique ID, checks whether a given headset is already registered with the Teacher portal
    /// </summary>
    /// <param name="DeviceID">The Unique Device Identifier for the currently running hardware</param>
    /// <returns>
    /// Task<(bool, bool, string)>:
    ///     bool Item1:     Whether the request was succsefully sent or not (HTTP_200 v. HTTP_4/5xx)
    ///     bool Item2:     Whether the headset is registered or not
    ///     string Item3:   The message from the server
    /// </returns>
    public static async Task<(bool, bool, string)> CheckHeadsetRegistrationStatus(string DeviceID)
    {
        string HeadsetRegistrationStatusEndpoint = $"{API_ENDPOINT}headset/exists/{DeviceID}/";

        (bool, string) results = await GenericGetRequest(HeadsetRegistrationStatusEndpoint);


        if (results.Item1)
        {
            return (false, false, results.Item2);
        }
        else
        {
            if (results.Item2.ToLower().Contains("true"))
            {
                return (true, true, results.Item2);
            } else
            {
                return (true, false, results.Item2);
            }
        }

    }


    /// <summary>
    /// Given the headset's Unique ID, grab the headsets nickname for streaming purposes
    /// </summary>
    /// <param name="DeviceID">The Unique Device Identifier for the currently running hardware</param>
    /// <returns>
    /// Task<(bool, bool, string)>:
    ///     bool Item1:     Whether the request was succsefully sent or not (HTTP_200 v. HTTP_4/5xx)
    ///     string Item2:   the devices nickname for stream url generation
    /// </returns>
    public static async Task<(bool, string)> GetHeadsetNickname(string DeviceID)
    {
        string HeadsetDetailEndpoint = $"{API_ENDPOINT}headset/{DeviceID}/";

        (bool, string) results = await GenericGetRequest(HeadsetDetailEndpoint);

        if (results.Item1)
        {
            LogManager.LogMessage(results.Item2, true);
            return (false, results.Item2);
        }
        else
        {
            Headset HeadsetDetails = JsonUtility.FromJson<Headset>(results.Item2);
            return (true, HeadsetDetails.device_nickname);
        }

    }


    /// <summary>
    /// Given a device identifier, attempts to retrieve said device's assigned group from the server
    /// </summary>
    /// <param name="DeviceID"></param>
    /// <returns>
    /// Task<(bool, bool, string, Group)>:
    ///     bool Item1:     Represents whether the call itself succeded (HTTP_200 v. HTTP_4/5xx)
    ///     bool Item2:     Represents whether a group was able to be retrieved for this headset
    ///     string Item3:   In the event of failed retrieval or error, provides clarity on why the call failed
    ///     Group Item4:    In the event of succsesful retrieval, contains the retrieved group 
    /// </returns>
    public static async Task<(bool, bool, string, Group)> RetrieveHeadsetGroupAssignmentFromServer(string DeviceID)
    {
        string GroupRetrievalEndpoint = $"{API_ENDPOINT}group/{DeviceID}/";
        LogManager.LogMessage(GroupRetrievalEndpoint);

        (bool, string) results = await GenericGetRequest(GroupRetrievalEndpoint);

        LogManager.LogMessage($"{results.Item1.ToString()} - {results.Item2}");

        if (results.Item1)
        {
            return (false, false, results.Item2, null);
        } 
        else if (results.Item2.Contains("Error - Headset"))
        {
            return (true, false, "This headset is not registered!", null);
        } 
        else if (results.Item2.Contains("Error - Group"))
        {
            return (true, false, "This headset has not been assigned a Group!", null);
        } else
        {
            return (true, true, null, JsonUtility.FromJson<Group>(results.Item2));
        }
    }

    /// <summary>
    /// Attempts to submit new session data to a __very__ forgiving endpoint which accepts JSON data
    /// </summary>
    /// <param name="SessionDataJsonString">A json representation of the data in String form</param>
    /// <returns>
    /// Task<(bool, string)>:
    ///     bool Item1:     Represents whether the call itself was succseful (HTTP_200 v. HTTP_4/5xx)
    ///     string Item2:   Contains the response body of the call from the server or it's accompanying error message
    /// </returns>
    public static async Task<(bool, string)> SubmitNewSessionDataToServer(string SessionDataJsonString)
    {

        string SubmissionEndpoint = $"{API_ENDPOINT}new-session-data/";

        (bool, string) results = await GenericPostRequest(SubmissionEndpoint, SessionDataJsonString);

        return (!results.Item1, results.Item2);

    }



    // ===================================================================== //
    // ==================== Helper Functions =============================== //
    /// <summary>
    /// Asynchronous, generic get request with API key auth.
    /// </summary>
    /// <param name="url">The specified URL to hit</param>
    /// <returns>
    /// Task<(bool, string)>:
    ///     bool Item1:     Represents whether an error was encountered while attempting to execute this Network Request
    ///     string Item2:   In the event of an error, contains the error message.
    ///                     In the event of success, contains the response from the server
    /// </returns>
    private static async Task<(bool, string)> GenericGetRequest(string url)
    {
        using (var www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", $"Api-Key {API_KEY}");
            www.SendWebRequest();

            while (!www.isDone)
                await Task.Delay(100);

            if (www.isNetworkError)
            {
                LogManager.LogMessage($"Failed GET Request. Error Message: {www.error}", true);
                return (true, www.error);
            }
            else
            {
                while (!www.downloadHandler.isDone)
                {
                    await Task.Delay(100);
                }

                LogManager.LogMessage($"Output from request to {url} - {www.downloadHandler.text}");

                return (false, www.downloadHandler.text);

            }
        }
    }

    /// <summary>
    /// Asynchrounous, Generic Post request with API Key Auth
    /// </summary>
    /// <param name="url"></param>
    /// <returns>
    /// Task<(bool, string)>:
    ///     bool Item1:     Represents whether an error was encountered while attempting to execute this Network Request
    ///     string Item2:   In the event of an error, contains the error message. 
    ///                     In the event of success, contains the response from the server
    ///</returns>
    private static async Task<(bool, string)> GenericPostRequest(string url, string JsonBody)
    {
        using (UnityWebRequest www = UnityWebRequest.Put(url, JsonBody))
        {
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.SetRequestHeader("Authorization", $"Api-Key {API_KEY}");
            www.SetRequestHeader("Content-Type", "application/json");

            www.SendWebRequest();

            while (!www.isDone)
                await Task.Delay(100);

            if (www.isNetworkError)
            {
                LogManager.LogMessage($"Failed POST Request. Error Message: {www.error}", true);
                return (true, www.error);
            }
            else
            {
                while (!www.downloadHandler.isDone)
                {
                    await Task.Delay(100);
                }

                LogManager.LogMessage($"Output from request to {url} - {www.downloadHandler.text}");

                return (false, www.downloadHandler.text);

            }
        }
    }
}

 



