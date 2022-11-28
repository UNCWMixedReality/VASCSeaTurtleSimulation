using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVr : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public GameObject[] pins;
    public GameObject[] pinsPlaceholders;

    Collider presser;
 
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.01f, 0);
            onPress.Invoke();
            presser = other;
            isPressed = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.03f, 0);
            onRelease.Invoke();
            isPressed = false;
        }

    }
    public void resetPins()
    {
        Debug.Log("pins resetting");

        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].transform.position = pinsPlaceholders[i].transform.position;
            pins[i].transform.rotation = pinsPlaceholders[i].transform.rotation;
        }
    }

    }


