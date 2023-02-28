using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;


namespace VASCDataCollection
{
    public enum EventType
    {
        Other,
        Interaction,
        Movement,
        Decision,
        Activity
    }

    public class EventLog : MonoBehaviour
    {

        public static void logEvent(string msg, EventType eventType)
        {
            JSONNode playerData = JSON.Parse(File.ReadAllText(Application.persistentDataPath + "/player.json"));

            string pathToUser = Path.Combine(Application.persistentDataPath + "/Users", playerData["ID"]);

            string userConfig = File.ReadAllText(pathToUser + "/UserConfig.json");
            JSONNode data = JSON.Parse(userConfig);


            string pathToUserLogs = Path.Combine(pathToUser, "Logs");
            Directory.CreateDirectory(pathToUserLogs);

            string logMsg = data["id"] + "," + data["session"] + "," + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "," + eventType.ToString() + "," + SceneManager.GetActiveScene().name + "," + msg + "\n";

            logAll(logMsg, pathToUserLogs, eventType);

            string pathToLogs = Application.persistentDataPath + "/Logs/";
            Directory.CreateDirectory(pathToLogs);
            logAll(logMsg, pathToLogs, eventType);
        }


        public static void logAll(string logMsg, string path, EventType eventType) { 

            if (!File.Exists(path + "/log.csv"))
            {
                string logHeader = "User, Session, Timestamp, Event Type, Module, Message\n";

                File.WriteAllText(path + "/log.csv", logHeader);
                File.WriteAllText(path + "/InteractionLog.csv", logHeader);
                File.WriteAllText(path + "/ActivityLog.csv", logHeader);
                File.WriteAllText(path + "/DecisionLog.csv", logHeader);
                File.WriteAllText(path + "/MovementLog.csv", logHeader);
            }

            // Log event to allLogs file
            File.AppendAllText(path + "/log.csv", logMsg);

            // Log to Event Type specific log file
            switch (eventType)
            {
                case EventType.Interaction:
                    File.AppendAllText(path + "/InteractionLog.csv", logMsg);
                    break;
                case EventType.Movement:
                    File.AppendAllText(path + "/MovementLog.csv", logMsg);
                    break;
                case EventType.Activity:
                    File.AppendAllText(path + "/ActivityLog.csv", logMsg);
                    break;
                case EventType.Decision:
                    File.AppendAllText(path + "/DecisionLog.csv", logMsg);
                    break;
            }
        }

        public static void logInteractionEvent(string msg)
        {
            logEvent(msg, EventType.Interaction);
        }

        public static void logMovementEvent(string msg)
        {
            logEvent(msg, EventType.Movement);
        }

        public static void logDecisionEvent(string msg)
        {
            logEvent(msg, EventType.Decision);
        }

        public static void logActivityEvent(string msg)
        {
            logEvent(msg, EventType.Activity);
        }
    }
}



