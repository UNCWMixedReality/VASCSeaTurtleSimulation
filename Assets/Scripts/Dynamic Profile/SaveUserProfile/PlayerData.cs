using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerData
{

    public string playerDataName;
    public string playerDataCharName;
    public string playerDataCharNumber;

    public Player.PlayerDataList myPlayerDataList;

    
    public PlayerData (Player player)
    {

        
        player.newplayer.Username = player.adjName.text + " " + player.nounName.text;
        playerDataName = player.newplayer.Username;
        player.playerName = playerDataName;
        

        player.newplayer.CharacterSelected = player.characterName.text;
        playerDataCharName = player.newplayer.CharacterSelected;
        player.playerCharName = playerDataCharName;
        

        player.newplayer.CharacterNumber = player.characterNumber.text;
        playerDataCharNumber = player.newplayer.CharacterNumber;
        player.playerCharNumber = playerDataCharNumber;


        myPlayerDataList = player.GetList();


    }
}
