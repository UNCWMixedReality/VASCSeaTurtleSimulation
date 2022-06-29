using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestSign : MonoBehaviour
{

    public TaskManagerM2_2 taskMan;

    //checks for when the nest has been set in place
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Sign")
        {
            //place the sign
            col.transform.position = transform.position;

            gameObject.SetActive(false);

            taskMan.MarkTaskCompletion();
        }
    }
}
