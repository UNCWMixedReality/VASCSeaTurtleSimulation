using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

namespace VASCDataCollection
{
    public class UserConfig : MonoBehaviour
    {
        public static void generateUserConfig()
        {

            string pathToPlayer = Application.persistentDataPath + "/player.json";
            string pathToTeacher = Application.persistentDataPath + "/teacher.json";

            string teacherjson = File.ReadAllText(pathToTeacher);
            string playerjson = File.ReadAllText(pathToPlayer);

            JSONNode teacherData = JSON.Parse(teacherjson);
            JSONNode playerData = JSON.Parse(playerjson);

            string pathToUsers = Path.Combine(Application.persistentDataPath, "Users");
            Directory.CreateDirectory(pathToUsers);

            string pathToUserConfigFolder = Path.Combine(pathToUsers, playerData["ID"]);
            Directory.CreateDirectory(pathToUserConfigFolder);

            string pathToUserConfig = pathToUserConfigFolder + "/UserConfig.json";

            if (!File.Exists(pathToUserConfig))
            {
                Config user = new Config(playerData["ID"], 0, teacherData["teacherid"], teacherData["sitting"], teacherData["demo"]);
                File.WriteAllText(pathToUserConfig, JsonUtility.ToJson(user));
            }
            else
            {
                string configData = File.ReadAllText(pathToUserConfig);
                Config config = JsonUtility.FromJson<Config>(configData);

                Config user = new Config(playerData["ID"], config.session + 1, teacherData["teacherid"], teacherData["sitting"], teacherData["demo"]);
                File.WriteAllText(pathToUserConfig, JsonUtility.ToJson(user));
            }
        }

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