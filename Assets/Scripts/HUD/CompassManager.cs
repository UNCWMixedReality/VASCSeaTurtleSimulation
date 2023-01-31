using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompassManager : MonoBehaviour
{
    public GameObject compass;
    public GameObject activeObjective;
    public Image compassImage;
    public Transform player;
    public Sprite[] arrows;

    float compassUnit;

    void Start()
    {
        compassUnit = compass.GetComponent<Image>().rectTransform.rect.width / 360f;
    }

    void LateUpdate()
    {
        if (activeObjective != null)
        { 
            compassImage.rectTransform.anchoredPosition = GetPosOnCompass(activeObjective);
        }
    }


    Vector2 GetPosOnCompass (GameObject marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);
        Vector2 markerPos = new Vector2(marker.transform.position.x, marker.transform.position.z);

        float angle = Vector2.SignedAngle(markerPos - playerPos, playerFwd);

        if (angle > 30f)
            compassImage.sprite = arrows[0];
        else if (angle < -30f)
            compassImage.sprite = arrows[1];
        else
            compassImage.sprite = arrows[2];

        return new Vector2(compassUnit * angle, 0f);
    }

    public void DisableCompass()
    {
        compass.SetActive(false);
    }

    public void EnableCompass(GameObject objective)
    {
        compass.SetActive(true);
        activeObjective = objective;
    }


}

