using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCollection;
using UltimateXR.Manipulation;
using cakeslice;

public class CageManager : MonoBehaviour
{
    public GameObject cage;
    public GameObject nestCageCollider;
        
    public void PrepareCage()
    {
        //outline and enable grabbing
        cage.GetComponent<Outline>().enabled = true;
        cage.GetComponent<UxrGrabbableObject>().IsGrabbable = true;
    }

    public void DisableCage()
    {
        //stop outlining the cage
        cage.GetComponent<Outline>().enabled = false;
        cage.GetComponent<UxrGrabbableObject>().IsGrabbable = false;

    }

    public void PrepareCovering()
    {
        nestCageCollider.SetActive(true);

    }

    public void EndCovering()
    {
        //stop grabbing the cagee
        cage.GetComponent<UxrGrabbableObject>().IsGrabbable = false;

        //put the cage where it belongs
        cage.transform.position = nestCageCollider.transform.position;
        cage.transform.rotation = nestCageCollider.transform.rotation;
        cage.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        cage.GetComponent<Rigidbody>().useGravity = false;
        cage.GetComponent<Collider>().enabled = false;
    }
}

