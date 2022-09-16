using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPopup : MonoBehaviour
{
    public GameObject helpCanvas;
    public Camera camera;
    private bool helpActive = false;
    
    public void toggleHelp()
    {
        helpActive = !helpActive;
        helpCanvas.SetActive(helpActive);
    }

    void FixedUpdate()
    {

        transform.LookAt(camera.transform);

    }


}
