using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class InputEntry : MonoBehaviour
{
    
    public string playerName;
    

    public InputEntry(String name)
    {
        
        playerName = name;
        
    }

    
}
