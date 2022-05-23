using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletSliderRefreash : MonoBehaviour
{

    public GameObject button1;
    public GameObject button2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refreash()
    {
        button1.SetActive(false);
        button2.SetActive(false);
    }
}
