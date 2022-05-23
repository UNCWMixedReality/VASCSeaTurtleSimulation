using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject player;

    public GameObject current;
    //switches panels

    void Start()
    {
        current.SetActive(true);
        //orient the player in the right direction
        player.transform.rotation = Quaternion.Euler(0,0,0);
    }
    
    //change the panel
    public void ChangePanel(GameObject nextPanel)
    {
        current.SetActive(false);
        nextPanel.SetActive(true);
        current = nextPanel;
    }

    //quit game
    public void Exit()
    {
        Application.Quit();
    }
}
