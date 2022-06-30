using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;

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
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            col.GetComponent<DcGrabInteractable>().enabled = false;

            gameObject.SetActive(false);

            taskMan.MarkTaskCompletion();
        }
    }
}
