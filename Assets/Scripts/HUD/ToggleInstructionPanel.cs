using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Devices;
using UltimateXR.Core;

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
        Debug.Log("toggle panel");

        if (UxrAvatar.LocalAvatarInput.GetButtonsEvent(UxrHandSide.Left, UxrInputButtons.Button1, UxrButtonEventType.PressDown))
        {
            Debug.Log("toggle panel");
            instructionCanvas.SetActive(deactivated);
            deactivated = !deactivated;
        }
    }
}
