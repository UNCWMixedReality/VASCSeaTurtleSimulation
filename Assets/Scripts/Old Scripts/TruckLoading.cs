using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Older Script for equipment loading.

/*
public class Equipment
{
    public GameObject EquipmentObject;
}
*/

public class TruckLoading : MonoBehaviour
{
    public int itemsInTruck = 0;
    public int truckCapacity = 4;

    public Text uiCounter;
    public GameObject truckModel;
    public GameObject tailgate;

    public int tailgateSpeed;
    public bool tailgateOpen = true;

    public GameObject player;
    public GameObject truckParent;
    //private BoxCollider truckBoxCollider;
    public float heightOffset = 1;

    //public List<Equipment> EquipmentNumber;

    void Start()
    {
        //truckBoxCollider = truckParent.GetComponent<BoxCollider>();
        //truckBoxCollider.enabled = false; //Now equipment won't collide with truck box collider.

        uiCounter.text = "0/" + truckCapacity;
        Physics.IgnoreCollision(truckModel.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(truckParent.GetComponent<Collider>(), GetComponent<Collider>());

        //foreach(Equipment x in EquipmentNumber)
        //{
        //    Physics.IgnoreCollision(truckParent.GetComponent<Collider>(), x.GameObject.GetComponent<Collider>());
        //}
    }

    void OnTriggerEnter(Collider col)
    {
        print("Collision");

        /*
        if (col.gameObject.CompareTag("TruckEquipment") || col.gameObject.name == "Throwable (Newton) 1")
        {
            itemsInTruck = itemsInTruck + 1;
        }
        */
        
        itemsInTruck = itemsInTruck + 1;

        print(itemsInTruck);

        uiCounter.text = itemsInTruck + "/" + truckCapacity;
    }

    void Update()
    {
        if (tailgateOpen && tailgate.transform.rotation.x < .5)
        {
            tailgate.transform.Rotate(Vector3.right * (tailgateSpeed * Time.deltaTime));
        }
        else if (tailgate.transform.rotation.x >= .5)
        {
            tailgateOpen = false;
        }

        if (itemsInTruck == truckCapacity && tailgate.transform.rotation.x > 0)
        {
            tailgate.transform.Rotate(Vector3.left * (tailgateSpeed * Time.deltaTime));
        }

        if (itemsInTruck == truckCapacity)
        {
            player.transform.position = new Vector3(truckParent.transform.position.x, truckParent.transform.position.y - heightOffset, truckParent.transform.position.z);
            player.transform.SetParent(truckParent.transform);
            //truckBoxCollider.enabled = true; //Reenable truck's box collider.
        }
    }
}
