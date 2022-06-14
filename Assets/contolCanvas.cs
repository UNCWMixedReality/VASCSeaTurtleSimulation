using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class contolCanvas : MonoBehaviour
{
    [SerializeField] XRController controller;
    public GameObject canvas;

    private void Update()
    {

        bool isPressed;
        bool wasPressed;
        
        controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressed);
        if (isPressed)
        {
            canvas.SetActive(false);

        }
        controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out wasPressed);
        if (wasPressed)
        {
            canvas.SetActive(true);
        }

    }
}