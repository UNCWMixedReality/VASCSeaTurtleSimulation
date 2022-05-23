using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInTruck : MonoBehaviour
{
    private GameObject truck;

    /*
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            truck = GameObject.Find("Truck");

            transform.position = truck.transform.position;
            transform.parent = truck.transform;
        }
    }
    */

    void OnSceneLoad()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            truck = GameObject.Find("Truck");

            transform.position = truck.transform.position;
            transform.parent = truck.transform;
        }
    }
}
