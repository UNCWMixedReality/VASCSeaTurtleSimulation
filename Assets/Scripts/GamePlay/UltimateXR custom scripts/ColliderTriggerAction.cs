using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTriggerAction : MonoBehaviour
{
    /// <summary>
    /// Added to an object with a collider. Triggers events when a collider enteres 
    /// collider of object script is attatched to. specificTag allows ability to specify if only certain tags trigger events.
    /// 
    /// Can specify an event to trigger when a specific collider is enteres using the specificColliderEntered/Exited events
    /// 
    /// - Nicholas Brunsink
    /// </summary>
    public UnityEvent colliderEntered;
    public UnityEvent colliderExited;
    public string specificTag;
    public UnityEvent specificColliderEntered;
    public UnityEvent specificColliderExited;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == specificTag)
        {
            specificColliderEntered.Invoke();
        }
        colliderEntered.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == specificTag)
        {
            specificColliderExited.Invoke();
        }
        colliderExited.Invoke();
    }
}
