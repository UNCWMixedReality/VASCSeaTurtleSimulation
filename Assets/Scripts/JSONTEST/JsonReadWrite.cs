
using System.IO;
using UnityEngine;
using TMPro;


public class JsonReadWrite : MonoBehaviour
{
    public TMP_InputField adjInputField;
    public TMP_InputField nounInputField;
    public TMP_InputField characterInputField;
    public TMP_InputField whichCharacterNumber;

   

    public void SaveToJson()
    {
        savefile data = new savefile();

        data.username = adjInputField.text + " "+ nounInputField.text;
        data.character = characterInputField.text;
        data.characterNumber = whichCharacterNumber.text;


        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/userInfo.json", json);

    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/userInfo.json");
        savefile data = JsonUtility.FromJson<savefile>(json);

        //usernameInputField.text = data.username;
        //characterSelected.text = data.character;
    }
}
