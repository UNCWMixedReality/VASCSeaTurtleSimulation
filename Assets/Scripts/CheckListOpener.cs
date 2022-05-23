using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckListOpener : MonoBehaviour
{

    public GameObject checkList;
    private bool checkListEnabled;
    // Start is called before the first frame update
    void Start()
    {
        checkListEnabled = false;
    }

    public void CheckListEnable()
    {
        if(checkListEnabled == false)
        {
            checkList.SetActive(true);
            checkListEnabled = true;
        }

        else 
        {
            checkList.SetActive(false);
            checkListEnabled = false;
        }
    }
}
