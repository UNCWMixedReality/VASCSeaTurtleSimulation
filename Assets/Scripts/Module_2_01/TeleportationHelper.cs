using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHelper : MonoBehaviour
{

    public TaskManager taskMan;
    public GameObject excavationWaypoint;
    public GameObject player;

    private bool taskTwoCompleted;
    private bool playerInRange;

    private void Start()
    {
        taskTwoCompleted = false;
    }

    public void CheckPosition()
    {

        playerInRange = (player.transform.position - excavationWaypoint.transform.position).magnitude < 5;

        if (playerInRange && !taskTwoCompleted)
        {
            taskTwoCompleted = true;
            taskMan.MarkTaskCompletion();
        }
    }
}
