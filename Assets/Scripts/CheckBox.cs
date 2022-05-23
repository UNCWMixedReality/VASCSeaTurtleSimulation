using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject tool;
    public GameObject check;
    public GameObject check1;
    private void OnTriggerEnter(Collider tool)
    {
        check.SetActive(true);
        check1.SetActive(true);
    }
}
