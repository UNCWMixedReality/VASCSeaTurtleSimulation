using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageCollisionIgnore : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(!(collision.gameObject.CompareTag("Cage Placeholder")))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
