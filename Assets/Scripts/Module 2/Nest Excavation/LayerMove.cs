using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMove : MonoBehaviour
{
    public GameObject previousLayer;
    private Vector3 position;

    //moves the final sand layer into position without destroying it
    void OnTriggerEnter(Collider col)
    {
        if (previousLayer == null)
        {
            gameObject.transform.position = position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        position = previousLayer.transform.position;
    }

}
