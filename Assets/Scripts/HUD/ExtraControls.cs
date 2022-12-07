using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Devices;
using UnityEngine.SceneManagement;
using UltimateXR.Core;


public class ExtraControls : MonoBehaviour
{
    [SerializeField] private UxrControllerInput input;
    [SerializeField] private GameObject instructionCanvas;

    private bool deactivated;

    void Start()
    {
        deactivated = false;
    }


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
            if (e.Button == UxrInputButtons.Joystick && e.HandSide == UxrHandSide.Left)
            {
                instructionCanvas.SetActive(deactivated);
                deactivated = !deactivated;
            }
            else if (e.Button == UxrInputButtons.Joystick && e.HandSide == UxrHandSide.Right)
            {
                StartCoroutine(JoystickHold());
            }

        }
        else if (e.ButtonEventType == UxrButtonEventType.PressUp)
        {
            if (e.Button == UxrInputButtons.Joystick && e.HandSide == UxrHandSide.Right)

            {
                StopAllCoroutines();
            }
        }
    }

    IEnumerator JoystickHold()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("DemoMain");
    }

}
