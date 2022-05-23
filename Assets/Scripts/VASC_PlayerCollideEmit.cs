using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VASC_PlayerCollideEmit : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        // Delegate to relevant parties
        string collidedWith = collision.collider.gameObject.tag;
        switch (collidedWith)
        {
            case ("waypoint"):
                break;
        }
    }

}
