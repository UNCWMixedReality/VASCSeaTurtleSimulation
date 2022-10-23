using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataCollection;
using UltimateXR.Manipulation;

public class TurtleMeasureManager : MonoBehaviour
{


    // Fin Measure Objects
    public GameObject calipers;
    public GameObject[] caliperColliders;

    // Shell Measure Objects
    public GameObject tapeMeasure;
    public GameObject[] tmColliders;
    public GameObject tmIndicators;

    // Shared Measure Objects
    public GameObject[] collidersPlaceholders;
    public GameObject[] arrows;

    public GameObject Shiny;
    public GameObject Turtle;

    // UI elements
    public GameObject[] checks;
    public GameObject[] outlineFill;
    public Sprite checkmark;

    public CompassManager compMan;

    public void prepareTools()
    {
        float shiny = Random.Range(1, 8192);
        if (shiny == 1)
        {
            Shiny.SetActive(true);
            Turtle.SetActive(false);
        }

        compMan.EnableCompass(calipers);

        // prepare calipers
        //      calipers.transform.position = collidersPlaceholders[0].transform.position;
        UxrGrabManager.Instance.PlaceObject(calipers.GetComponent<UxrGrabbableObject>(), collidersPlaceholders[0].GetComponent<UxrGrabbableObjectAnchor>(), 0, false);
        calipers.GetComponent<UxrGrabbableObject>().IsGrabbable = true;

        // prepare tape measure
        tapeMeasure.GetComponent<UxrGrabbableObject>().IsGrabbable = true;
        UxrGrabManager.Instance.PlaceObject(tapeMeasure.GetComponent<UxrGrabbableObject>(), collidersPlaceholders[1].GetComponent<UxrGrabbableObjectAnchor>(), 0, false);
        tmIndicators.SetActive(false);
    }

    public void prepareFrontFins()
    {
        compMan.EnableCompass(collidersPlaceholders[2]);
        calipers.GetComponent<CalipSize>().taskNum = 7;

        for (int i=0; i < 2; i++)
        {
            caliperColliders[i].transform.position = collidersPlaceholders[i+2].transform.position;
            caliperColliders[i].transform.rotation = collidersPlaceholders[i+2].transform.rotation;
        }
        arrows[0].SetActive(true);
    }
    public void prepareBackFins()
    {
        compMan.EnableCompass(collidersPlaceholders[4]);
        calipers.GetComponent<CalipSize>().taskNum = 8;
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
        compMan.DisableCompass();
        //tapeMeasure.GetComponent<DcGrabInteractable>().enabled = true;
        //calipers.GetComponent<DcGrabInteractable>().enabled = false;
        tapeMeasure.GetComponent<TapeMeasureSize>().taskNum = 9;
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
        //tapeMeasure.GetComponent<TapeMeasureSize>().floatingText.text = "80 cm";
    }

    public void prepareShellWidth()
    {
        tapeMeasure.GetComponent<TapeMeasureSize>().taskNum = 10;

        for (int i = 0; i < 2; i++)
        {
            tmColliders[i].transform.position = collidersPlaceholders[i + 8].transform.position;
            tmColliders[i].transform.rotation = collidersPlaceholders[i + 8].transform.rotation;
        }

        outlineFill[2].SetActive(true);
        arrows[2].SetActive(false);
        arrows[3].SetActive(true);
        //tapeMeasure.GetComponent<TapeMeasureSize>().floatingText.text = "60 cm";
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
