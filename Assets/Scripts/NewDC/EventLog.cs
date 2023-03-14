/*
 * This script is the main script behind the VASC data collection. 
 * 
 * It handles generating both general and user-specific log files 
 *      And logging into those files when called by another script
 * 
 * Log Types:
 *  Activity - Related to progression, typically called inside 
 *              Task Managers when a task is completed or a scene
 *              is loaded.
 *  Interation - Related to interacting with grabbale objects, 
 *                  useful when picking up, releasing, or placing a GameObject.
 *  Decision - Related to quizzes, useful when a user has to choose an answers.
 *  Movement - Related to teleportation, useful when needing to know where a user is moving.
 *  
 *  All event types are logged in their individual files, 
 *      but are also dumped into a main Log file where all events are logged as well.
 *      
 *  Written by Nichols Brunsink
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;


namespace VASCDC
{
    public enum EventType
    {
        Other,
        Interaction,
        Movement,
        Decision,
        Activity
    }

    public class VASCEventLog : MonoBehaviour
    {
        #region Logging Functions
        private static void logEvent(string msg, EventType eventType)
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


        private static void logAll(string logMsg, string path, EventType eventType) { 

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
        #endregion

        #region Public Event Logging Functions
        /// <summary>
        /// These are the public function for interfacing with logging system
        /// </summary>
        /// <param name="msg">The message to be logged</param>
        public static void logInteractionEvent(string msg)
        {
            //logEvent(msg, EventType.Interaction);
        }

        public static void logMovementEvent(string msg)
        {
            //logEvent(msg, EventType.Movement);
        }

        public static void logDecisionEvent(string msg)
        {
            //logEvent(msg, EventType.Decision);
        }

        public static void logActivityEvent(string msg)
        {
            //logEvent(msg, EventType.Activity);
        }
        #endregion
    }
}



