using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using cakeslice;

public class CageManager : MonoBehaviour
{
    public GameObject cage;
    public GameObject nestCageCollider;
        
    public void PrepareCage()
    {
        //outline and enable grabbing
        cage.GetComponent<Outline>().enabled = true;
        cage.GetComponent<DcGrabInteractable>().enabled = true;
    }

    public void DisableCageHighlight()
    {
        //stop outlining the cage
        cage.GetComponent<Outline>().enabled = false;
    }

    public void PrepareCovering()
    {
        nestCageCollider.SetActive(true);

    }

    public void EndCovering()
    {
        //stop grabbing the cagee
        cage.GetComponent<DcGrabInteractable>().enabled = false;

        //put the cage where it belongs
        cage.transform.position = new Vector3(33.4f, 3.16f, -52.3f);
        cage.transform.rotation = new Quaternion(0, 0, 0, 0);
        cage.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        cage.GetComponent<Rigidbody>().useGravity = false;
        cage.GetComponent<Collider>().enabled = false;
    }
}

