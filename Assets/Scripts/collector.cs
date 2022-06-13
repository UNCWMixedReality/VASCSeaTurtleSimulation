using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class collector : MonoBehaviour
{

    public UnityEvent OnCollected = new UnityEvent();

    public void OnTriggerEnter(Collider item)
    {

        Debug.Log("COLLIDED");

        if (item.gameObject.tag == "collectable")
        {

            OnCollected.Invoke();
            item.gameObject.SetActive(false);

        }

    }

}
