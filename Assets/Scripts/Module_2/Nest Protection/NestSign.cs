using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestSign : MonoBehaviour
{
    public bool taskDone;
    public GameObject held;
    public GameObject sign;
    public GameObject placeholder;

    void Start()
    {
        taskDone = false;
    }

    void Update()
    {
        if (taskDone)
        {
            Destroy(held);
        }
    }

    //checks for when the nest has been set in place
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Sign" && !taskDone)
        {
            //sign.transform.position = placeholder.transform.position;
            //sign.transform.rotation = placeholder.transform.rotation;
            placeholder.transform.position = new Vector3(0, 0f, 0);
            taskDone = true;
            Debug.Log("Sign Placed");
        }
    }
}
