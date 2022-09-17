using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CalipSize : MonoBehaviour
{
    public Text displayText;
    public Text floatingText;
    public Transform movingPart;
    private float length;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        length = (movingPart.localPosition.x - 0.14f)*(16f/(-0.0155f - 0.14f));

        displayText.text = ((int)length).ToString() + "cm";
        floatingText.text = ((int)length).ToString() + "cm";

    }
}
