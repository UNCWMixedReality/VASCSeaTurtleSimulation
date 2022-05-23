using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCanvas : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;

    void Update()
    {

        if (canvas.activeSelf == true)
        {

            canvas.transform.LookAt(player.transform);
            canvas.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);

        }

    }
}
