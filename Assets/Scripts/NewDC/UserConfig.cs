using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

namespace VASCDC
{
    public class UserConfig : MonoBehaviour
    {
        /// <summary>
        /// Function used to generate a User Config which stores 
        ///     player settings and session information
        ///     
        /// Written by Nicholas Brunsink
        /// </summary>
        public static void generateUserConfig()
        {
            Debug.Log("andrew was here");
            string tempteacherjson = "{\"teacherid\":\"123456\",\"sitting\":0,\"demo\":0}";
            // grabbing info from player and teacher json files created from database and user profile creation
            string pathToPlayer = Application.persistentDataPath + "/player.json";
            string pathToTeacher = Application.persistentDataPath + "/teacher.json";

            File.WriteAllText(pathToTeacher, tempteacherjson);

            string teacherjson = File.ReadAllText(pathToTeacher);
            string playerjson = File.ReadAllText(pathToPlayer);
            JSONNode teacherData = JSON.Parse(teacherjson);
            JSONNode playerData = JSON.Parse(playerjson);

            // Creating a Users directory if needed
            string pathToUsers = Path.Combine(Application.persistentDataPath, "Users");
            Directory.CreateDirectory(pathToUsers);
            // Creating a user directory based off id obtained from player.json
            string pathToUserConfigFolder = Path.Combine(pathToUsers, playerData["ID"]);
            Directory.CreateDirectory(pathToUserConfigFolder);

            string pathToUserConfig = pathToUserConfigFolder + "/UserConfig.json";

            // Creates userconfig file with initial conditions if none exists
            if (!File.Exists(pathToUserConfig))
            {
                Config user = new Config(playerData["ID"], 0, teacherData["teacherid"], teacherData["sitting"], teacherData["demo"]);
                File.WriteAllText(pathToUserConfig, JsonUtility.ToJson(user));
            }
            // Updates information in userconfig file if needed
            else
            {
                string configData = File.ReadAllText(pathToUserConfig);
                Config config = JsonUtility.FromJson<Config>(configData);

                Config user = new Config(playerData["ID"], config.session + 1, teacherData["teacherid"], teacherData["sitting"], teacherData["demo"]);
                File.WriteAllText(pathToUserConfig, JsonUtility.ToJson(user));
            }
        }

        /// <summary>
        /// Class used to easily convert JSON to an easily usable C# object
        /// </summary>
        [Serializable]
        public class Config
        {
            public string id;
            public int session;
            public string teacher = "None";
            public int sitting = 0;
            public int demo = 0;

            public Config(string id, int session, string teacher, int sitting, int demo)
            {
                this.id = id;
                this.session = session;
                this.teacher = teacher;
                this.sitting = sitting;
                this.demo = demo;
            }
        }

    }
}