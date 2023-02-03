using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageCollisionIgnore : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sign") || collision.gameObject.CompareTag("Shovel"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
