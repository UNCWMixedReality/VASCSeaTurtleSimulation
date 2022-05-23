using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSand : MonoBehaviour
{
    public ParticleSystem particle;

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Shovel" || col.name == "LetfHand Controller" || col.name == "RightHand Controller")
            {
                particle.Play();
                Destroy(gameObject);
        }
    }
}
