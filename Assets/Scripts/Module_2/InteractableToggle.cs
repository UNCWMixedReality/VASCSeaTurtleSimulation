using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

//this determines whether the player can interact with given objects
//separate game objects are used for objects the player can and cannot interact with
public class InteractableToggle : MonoBehaviour
{
    public GameObject Shovel;
    public GameObject Cage;
    public GameObject egg1;
    public GameObject egg2;
    public GameObject egg3;
    public GameObject egg4;
    public GameObject egg5;
    public GameObject egg6;
    public GameObject sign;

    public GameObject ShovelnoOutline;
    public GameObject CagenoOutline;
    public GameObject egg1noOutline;
    public GameObject egg2noOutline;
    public GameObject egg3noOutline;
    public GameObject egg4noOutline;
    public GameObject egg5noOutline;
    public GameObject egg6noOutline;
    public GameObject signnoOutline;

    public void toggle(int thing, int layer)
    {
        switch (thing)
        {
            case 0:
                Debug.Log("Shovel");
                Shovel.SetActive(true);
                ShovelnoOutline.SetActive(false);
                break;
            case 1:
                Debug.Log("egg1");
                egg1.SetActive(true);
                egg1noOutline.SetActive(false);
                break;
            case 2:
                Debug.Log("egg2");
                egg2.SetActive(true);
                egg2noOutline.SetActive(false);
                egg3.SetActive(true);
                egg3noOutline.SetActive(false);
                egg4.SetActive(true);
                egg4noOutline.SetActive(false);
                egg5.SetActive(true);
                egg5noOutline.SetActive(false);
                egg6.SetActive(true);
                egg6noOutline.SetActive(false);
                break;
            case 3:
                Debug.Log("Cage");
                Cage.SetActive(true);
                CagenoOutline.SetActive(false);
                break;
            case 4:
                Debug.Log("Sign");
                sign.SetActive(true);
                signnoOutline.SetActive(false);
                break;
            default:
                Debug.Log("no such object exists");
                break;
        }
    }
}
