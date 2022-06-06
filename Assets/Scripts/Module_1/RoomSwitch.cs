using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    //this handles the logic for switching the tables the player will interact with

    public GameObject Room0;
    public GameObject Room1;
    public GameObject Room2;
    public GameObject Room3;

    public GameObject PlayerSpawn;
    public GameObject PlayerTracker;

    public GameObject RoomPlaceholder;
    public GameObject RoomAnchor;

    private GameObject[] rooms = new GameObject[4];
    private int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        rooms[0] = Room0;
        rooms[1] = Room1;
        rooms[2] = Room2;
        rooms[3] = Room3;
    }

    public void switchRoom()
    {
        rooms[current].transform.position = RoomPlaceholder.transform.position;
        PlayerTracker.transform.position = PlayerSpawn.transform.position;
        current++;
        rooms[current].transform.position = RoomAnchor.transform.position;
    }
}
