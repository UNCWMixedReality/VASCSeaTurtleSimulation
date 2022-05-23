using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script that handles logging of objects placed in truck and the movement of the tailgate.

public class EquipmentLoading : MonoBehaviour
{
    private int itemsInTruck = 0;
    public int truckCapacity = 4;

    public Text uiCounter;
    public GameObject truckModel;
    public GameObject tailgate;
    private bool sceneStart = true;

    public int tailgateSpeed;
    public GameObject player;

    public int sceneIndex;

    void Start()
    {
        uiCounter.text = "0/" + truckCapacity;
        Physics.IgnoreCollision(truckModel.GetComponent<Collider>(), GetComponent<Collider>()); //Don't let truck trigger collision.
        Physics.IgnoreCollision(tailgate.GetComponent<Collider>(), GetComponent<Collider>()); //Don't let tailgate trigger collision.
    }

    void OnTriggerEnter(Collider col)
    {
        itemsInTruck = itemsInTruck + 1;
        uiCounter.text = itemsInTruck + "/" + truckCapacity;
    }

    void Update()
    {
        if (sceneStart && tailgate.transform.rotation.x < .5) //Open the Tailgate
        {
            tailgate.transform.Rotate(Vector3.right * (tailgateSpeed * Time.deltaTime));
        }
        if (tailgate.transform.rotation.x >= .5)
        {
            sceneStart = false;
        }


        if (itemsInTruck >= truckCapacity)
        {
            uiCounter.enabled = false; //Hide Counter

            if (tailgate.transform.rotation.x > 0) //Close the Tailgate.
            {
                tailgate.transform.Rotate(Vector3.left * (tailgateSpeed * Time.deltaTime));
            }

            if (tailgate.transform.rotation.x <= 0) //Transition to inside truck.
            {
                Destroy(player);
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}

