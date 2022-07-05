using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHelperM3_1 : MonoBehaviour
{
    /*
     * I couldn't get a teleportation anchor working for some reason (i have no idea why) so I made this script instead.
     * 
     * This just checks when the player teleports near the nest for the first time and marks that task as completed
     */


    public TaskManagerM3_1 taskMan; //used so we can MarkTaskCompleted()

    //used to calculate distance between player and various waypoints
    public GameObject relocationWaypoint;
    public GameObject player;


    private bool taskTwoCompleted; //we only want to call MarkTaskCompleted() if the task isn't completed

    //helper variables for readability
    private bool playerInRange;
    private float range = 3.5f;


    private void Start()
    {
        taskTwoCompleted = false;
    }

    public void CheckPosition()
    {
        /*
         * This function is called every time the player teleports on the terrain.
         * 
         * if the player is in range of the nest and task two hasn't been completed yet, this marks task two as completed.
         */

        //determine whether the player is in range by comparing distance b/w player and excavation point to some arbitrary range
        playerInRange = (player.transform.position - relocationWaypoint.transform.position).magnitude < range;

        if (playerInRange && !taskTwoCompleted)
        {
            taskTwoCompleted = true;
            taskMan.MarkTaskCompletion(1);
        }
    }
}
