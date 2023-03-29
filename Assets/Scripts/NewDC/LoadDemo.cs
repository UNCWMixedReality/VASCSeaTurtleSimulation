using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VASCDC
{

    public class LoadDemo : MonoBehaviour
    {
        /// <summary>
        /// Function used to generate a User Config which stores 
        ///     player settings and session information
        ///     
        /// Written by Nicholas Brunsink
        /// </summary>
        public static void loadDemo()
        {
            string json = "{\"ID\":\"demo\",\"Username\":\"Demo Dolphin\",\"CharacterSelected\":\"Dolphin\",\"CharacterNumber\":\"6\"}";
            string pathToPlayer = Application.persistentDataPath + "/player.json";
            File.WriteAllText(pathToPlayer, json);
            UserConfig.generateUserConfig();
            SceneManager.LoadScene("Module_01");
        }
    }

}