using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataCollection;

public class ChangeImage : MonoBehaviour
{

    public GameObject playerChar;
    void Start()
    {
        if (DcDataLogging.Student == null)
        {
            DcDataLogging.Student = new DataCollection.Models.Student("NO NAME ENTERED", "");
        }
        if (DcDataLogging.Session == null)
        {
            DcDataLogging.BeginSession();
        }
        playerChar = GameObject.Find("Character Icon");
        if (playerChar != null)
        {
            gameObject.GetComponent<Image>().sprite = playerChar.GetComponent<Image>().sprite;
        }


    }

    public void UpdateImage()
    {
        gameObject.GetComponent<Image>().sprite = playerChar.GetComponent<Image>().sprite;

    }
}
