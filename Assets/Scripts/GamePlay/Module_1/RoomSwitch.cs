using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Core;
using UltimateXR.Avatar;

public class RoomSwitch : MonoBehaviour
{
    //this handles the logic for switching the tables the player will interact with
    public GameObject[] rooms;

    public Transform playerOrigin;
    public UxrAvatar player;

    private int current = 0;

    public void switchRoom()
    {
        rooms[current].SetActive(false);
        current++;
        rooms[current].SetActive(true);
        UxrManager.Instance.MoveAvatarTo(player, playerOrigin.position);
    }
}
