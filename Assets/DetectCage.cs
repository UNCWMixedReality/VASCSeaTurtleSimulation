using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCage : MonoBehaviour
{

    public GameObject successText;
    
    public void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "cage")
        {
            Debug.Log(name + " | HIT WITH: " + collider.name + " | TAG: " + collider.tag);
            successText.SetActive(true);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
