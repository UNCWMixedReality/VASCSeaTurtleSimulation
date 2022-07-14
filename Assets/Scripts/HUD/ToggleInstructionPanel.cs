using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInstructionPanel : MonoBehaviour
{
    public GameObject instructionCanvas;
    private bool deactivated;
    // Start is called before the first frame update
    void Start()
    {
        instructionCanvas.SetActive(true);
        deactivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            deactivated = !deactivated;
            instructionCanvas.SetActive(deactivated);
        }
    }
}
