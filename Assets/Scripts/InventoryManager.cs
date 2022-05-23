using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject inventory;
    private bool inventoryEnabled;


    private void Start()
    {
        inventoryEnabled = false;
    }

    public void InventoryEnable()
    {

        if(inventoryEnabled == false)
        {

            inventory.SetActive(true);
            inventoryEnabled = true;
        }

        else
        {
            inventory.SetActive(false);
            inventoryEnabled = false;
        }

    }



}
