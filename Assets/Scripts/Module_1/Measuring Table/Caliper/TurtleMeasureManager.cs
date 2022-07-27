using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataCollection;

public class TurtleMeasureManager : MonoBehaviour
{


    // Fin Measure Objects
    public GameObject calipers;
    public GameObject[] caliperColliders;

    // Shell Measure Objects
    public GameObject tapeMeasure;
    public GameObject[] tmPlaceholders;
    public GameObject[] tmColliders;
    public GameObject tmIndicators;

    // Shared Measure Objects
    public GameObject[] collidersPlaceholders;
    public GameObject[] arrows;

    // UI elements
    public GameObject[] checks;
    public GameObject[] outlineFill;
    public Sprite checkmark;

    public void prepareTools()
    {
        // prepare calipers
        calipers.transform.position = collidersPlaceholders[0].transform.position;
        calipers.transform.rotation = collidersPlaceholders[0].transform.rotation;
        calipers.GetComponent<NewCaliper>().caliperPlaceholder = collidersPlaceholders[0];
        calipers.GetComponent<NewCaliper>().ResetCaliper();
        calipers.GetComponent<DcGrabInteractable>().enabled = true;

        // prepare tape measure
        tapeMeasure.GetComponent<DcGrabInteractable>().enabled = false;
        tapeMeasure.transform.position = collidersPlaceholders[1].transform.position;
        tapeMeasure.transform.rotation = collidersPlaceholders[1].transform.rotation;
        tapeMeasure.GetComponent<NewTapeMeasure>().tmPlaceholder = collidersPlaceholders[1];
        tmIndicators.SetActive(false);
    }

    public void prepareFrontFins()
    {
        calipers.GetComponent<NewCaliper>().taskNum = 7;
        for (int i=0; i < 2; i++)
        {
            caliperColliders[i].transform.position = collidersPlaceholders[i+2].transform.position;
            caliperColliders[i].transform.rotation = collidersPlaceholders[i+2].transform.rotation;
        }
        arrows[0].SetActive(true);
    }
    public void prepareBackFins()
    {
        calipers.GetComponent<NewCaliper>().taskNum = 8;
        for (int i = 0; i < 2; i++)
        {
            caliperColliders[i].transform.position = collidersPlaceholders[i+4].transform.position;
            caliperColliders[i].transform.rotation = collidersPlaceholders[i+4].transform.rotation;
        }
        ChangeImage(checks[0]);
        outlineFill[0].SetActive(true);
        arrows[0].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        arrows[1].SetActive(true);
    }



    public void prepareShellLength()
    {
        tapeMeasure.GetComponent<DcGrabInteractable>().enabled = true;
        calipers.GetComponent<DcGrabInteractable>().enabled = false;
        tapeMeasure.GetComponent<NewTapeMeasure>().taskNum = 9;
        tmIndicators.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            tmColliders[i].transform.position = collidersPlaceholders[i + 6].transform.position;
            tmColliders[i].transform.rotation = collidersPlaceholders[i + 6].transform.rotation;
        }

        ChangeImage(checks[1]);
        outlineFill[1].SetActive(true);
        arrows[1].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        arrows[2].SetActive(true);
        tapeMeasure.GetComponent<NewTapeMeasure>().measureLength = "80 cm";
    }

    public void prepareShellWidth()
    {

        tapeMeasure.GetComponent<NewTapeMeasure>().taskNum = 10;
        for (int i = 0; i < 2; i++)
        {
            tmColliders[i].transform.position = collidersPlaceholders[i + 8].transform.position;
            tmColliders[i].transform.rotation = collidersPlaceholders[i + 8].transform.rotation;
        }

        outlineFill[2].SetActive(true);
        arrows[2].SetActive(false);
        arrows[3].SetActive(true);
        tapeMeasure.GetComponent<NewTapeMeasure>().measureLength = "65 cm";
    }

    public void finishMeasure()
    {
        outlineFill[3].SetActive(true);
        tmIndicators.SetActive(false);
        arrows[3].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
        ChangeImage(checks[2]);
    }

    public void ChangeImage(GameObject check)
    {
        check.GetComponent<Image>().sprite = checkmark;
        check.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        check.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 180, 0);
    }
    
    public void DisableTools()
    {
        tapeMeasure.SetActive(false);
        calipers.SetActive(false);
    }

}
