using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TapeMeasureSize : MonoBehaviour
{
    public Transform endPos;
    public Transform bodyPos;
    
    public Text floatingText;
    public Transform text;
    public Transform playerhead;
    public GameObject line;

    private float length;
    private Vector3 textLocation;
    private Vector3 tapeStartingScale;


    // Start is called before the first frame update
    void Start()
    {
        tapeStartingScale = line.transform.localScale;
    }

    // Update is called once per frame
    public void Update()
    {
        // Updating measuring tape text
        length = Vector3.Distance(endPos.position, bodyPos.position);
        floatingText.text = ((int)(length*100)).ToString() + "cm";
        textLocation = Vector3.Lerp(bodyPos.position, endPos.position, 0.5f);
        text.position = new Vector3(textLocation.x, textLocation.y +.07f, textLocation.z);
        text.LookAt(playerhead);

        // Drawing line between body and end
        line.transform.LookAt(endPos);
        line.transform.localScale = new Vector3(tapeStartingScale.x, tapeStartingScale.y, tapeStartingScale.z + length*125);



    }
}
