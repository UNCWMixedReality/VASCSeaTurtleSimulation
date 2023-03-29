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
    private Transform homePos;

    private bool deactivated;

    void Start()
    {
        deactivated = false;
        homePos = this.gameObject.transform;
    }


    protected void OnEnable()
    {
        input.ButtonStateChanged += ExtraInput_ButtonStateChanged;
    }

    protected void OnDisable()
    {
        input.ButtonStateChanged -= ExtraInput_ButtonStateChanged;
    }

    private void ExtraInput_ButtonStateChanged(object sender, UxrInputButtonEventArgs e)
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
            else if (e.Button == UxrInputButtons.Menu)
            {
                StartCoroutine(MenuHold());
            }

        }
        else if (e.ButtonEventType == UxrButtonEventType.PressUp)
        {
            if ((e.Button == UxrInputButtons.Joystick && e.HandSide == UxrHandSide.Right) || e.Button == UxrInputButtons.Menu)
            {
                StopAllCoroutines();
            }
        }
    }

    
    void Update()
    {
        if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Joystick))
        {
            instructionCanvas.SetActive(deactivated);
            deactivated = !deactivated;
        }
        else if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Joystick))
        {
            StartCoroutine(JoystickHold());
        }
        else if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Menu))
        {
            StartCoroutine(MenuHold());
        }
        else if (UxrAvatar.LocalAvatarInput.GetButtonsPressUp(UxrHandSide.Right, UxrInputButtons.Joystick) || UxrAvatar.LocalAvatarInput.GetButtonsPressUp(UxrHandSide.Left, UxrInputButtons.Menu))
        {
            StopAllCoroutines();
        }
    }
    

    IEnumerator JoystickHold()
    {
        yield return new WaitForSeconds(3);
        UxrManager.Instance.MoveAvatarTo(UxrAvatar.LocalAvatar, homePos.position);
    }

    IEnumerator MenuHold()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("NewMain");
    }

}
