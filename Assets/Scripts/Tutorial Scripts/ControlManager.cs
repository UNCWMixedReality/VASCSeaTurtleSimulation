using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Devices;


public class ControlManager : MonoBehaviour
{
    [SerializeField] private UxrControllerInput input;
    [SerializeField] private TaskManagerTutorial taskMan;

    public GameObject[] buttonHighlights;


    protected void OnEnable()
    {
        input.ButtonStateChanged += Input_ButtonStateChanged;
    }

    protected void OnDisable()
    {
        input.ButtonStateChanged -= Input_ButtonStateChanged;
    }

    private void Input_ButtonStateChanged(object sender, UxrInputButtonEventArgs e)
    {
        if (e.ButtonEventType == UxrButtonEventType.PressDown)
        {
            Debug.Log($"Pressed {e.HandSide}, {e.Button}, {e.ButtonEventType}");

            if (e.Button == UxrInputButtons.Button1)
            {
                taskMan.MarkTaskCompletion(1);
            }
            else if (e.Button == UxrInputButtons.Button2)
            {
                taskMan.MarkTaskCompletion(2);
            }
            else if (e.Button == UxrInputButtons.DPadRight || e.Button == UxrInputButtons.DPadLeft)
            {
                taskMan.MarkTaskCompletion(3);
            }
            else if (e.Button == UxrInputButtons.Trigger)
            {
                taskMan.MarkTaskCompletion(4);
            }
            else if (e.Button == UxrInputButtons.Grip)
            {
                taskMan.MarkTaskCompletion(5);
            }
        }
    }

}
