using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocationTracker : MonoBehaviour
{
    public int eggsPlaced { get; set; }
    public TaskManagerM2_2 taskMan;

    private void Start()
    {
        eggsPlaced = 0;
    }

    public void UpdateEggCount()
    {
        eggsPlaced += 1;
        if (eggsPlaced == 6)
        {
            taskMan.MarkTaskCompletion();
        }
    }
}
