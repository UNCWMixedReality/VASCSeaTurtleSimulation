using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetModule : MonoBehaviour
{
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            
            Debug.Log("button Pressed");

            SceneManager.LoadScene("Tutorial");


        }
    }
}


