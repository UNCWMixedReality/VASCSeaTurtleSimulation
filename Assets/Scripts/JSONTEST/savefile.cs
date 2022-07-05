using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class savefile
{
    public string username;
    public string character;
    public string characterNumber;
    





    public savefile(string username, string character, string characterNumber)
    {
       
        
        this.username = username;
        this.character = character;
        this.characterNumber = characterNumber;

        
    }

}
