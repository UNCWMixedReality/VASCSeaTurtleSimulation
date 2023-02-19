using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using VASCDataCollection;


public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player";
        
        FileStream stream = new FileStream(path+".fun", FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        File.WriteAllText(path+".json", JsonUtility.ToJson(player.newplayer));
        UserConfig.generateUserConfig();

    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player";
        if (File.Exists(path+".fun"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path+".fun", FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found In" + path+".fun");
            return null;
        }
    }

}
