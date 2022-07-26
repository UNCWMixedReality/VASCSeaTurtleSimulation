using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleMeasureManager : MonoBehaviour
{
    // Fin Measure Objects
    public GameObject caliper;
    public GameObject[] collidersPlaceholders;
    public GameObject[] caliperColliders;
    public GameObject[] finArrows;

    // Shell Measure Objects
    public GameObject tapeMeasure;
    public GameObject[] tmPlaceholders;
    public GameObject[] tmColliders;
    public GameObject[] shellArrows;

    // UI elements
    public GameObject[] checks;
    public GameObject[] outlineFill;
    public Sprite checkmark;

    public void prepareCaliper()
    {
        caliper.transform.position = collidersPlaceholders[0].transform.position;
        caliper.transform.rotation = collidersPlaceholders[0].transform.rotation;
        caliper.GetComponent<NewCaliper>().caliperPlaceholder = collidersPlaceholders[0];
        caliper.GetComponent<NewCaliper>().ResetCaliper();
    }

    public void prepareFrontFins()
    {
        caliper.GetComponent<NewCaliper>().taskNum = 7;
        for (int i=0; i < 2; i++)
        {
            caliperColliders[i].transform.position = collidersPlaceholders[i+1].transform.position;
            caliperColliders[i].transform.rotation = collidersPlaceholders[i+1].transform.rotation;
        }
        finArrows[0].SetActive(true);
    }
    public void prepareBackFins()
    {
        caliper.GetComponent<NewCaliper>().taskNum = 8;
        for (int i = 0; i < 2; i++)
        {
            caliperColliders[i].transform.position = collidersPlaceholders[i+3].transform.position;
            caliperColliders[i].transform.rotation = collidersPlaceholders[i+3].transform.rotation;
        }
        ChangeImage(checks[0]);
        outlineFill[0].SetActive(true);
        finArrows[0].SetActive(false);
        finArrows[1].SetActive(true);
    }

    public void prepareShellLength()
    {
        tapeMeasure.GetComponent<NewTapeMeasure>().taskNum = 9;
        for (int i = 0; i < 2; i++)
        {
            tmColliders[i].transform.position = tmPlaceholders[i + 3].transform.position;
            tmColliders[i].transform.rotation = tmPlaceholders[i + 3].transform.rotation;
        }

        ChangeImage(checks[1]);
        outlineFill[1].SetActive(true);
        finArrows[1].SetActive(false);
        shellArrows[0].SetActive(true);
    }

    public void prepareShellWidth()
    {
        tapeMeasure.GetComponent<NewTapeMeasure>().taskNum = 10;
        for (int i = 0; i < 2; i++)
        {
            tmColliders[i].transform.position = tmPlaceholders[i + 3].transform.position;
            tmColliders[i].transform.rotation = tmPlaceholders[i + 3].transform.rotation;
        }

        ChangeImage(checks[2]);
        outlineFill[2].SetActive(true);
        shellArrows[0].SetActive(false);
        shellArrows[1].SetActive(true);
    }

    public void finishMeasure()
    {
        outlineFill[3].SetActive(true);
    }

    public void ChangeImage(GameObject check)
    {
        check.GetComponent<Image>().sprite = checkmark;
        check.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        check.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 180, 0);
    }

}
