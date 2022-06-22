
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField adjInput;
    [SerializeField] TMP_InputField nounInput;
    [SerializeField] string filename;

    List<InputEntry> entries = new List<InputEntry>();

    
    private void Start()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);
    }

    public void AddNameToList()
    {
        Debug.Log(adjInput.text + " " + nounInput.text);
        entries.Add(new InputEntry(adjInput.text + " " +nounInput.text));
        

        FileHandler.SaveToJSON<InputEntry>(entries, filename);
    }
}
