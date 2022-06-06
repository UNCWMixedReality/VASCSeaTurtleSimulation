using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SwitchImageButton : MonoBehaviour
{

    public Sprite image1;
    public Sprite image2;
    // Use this for initialization
    private bool on = true;

    void Start()
    {
        this.GetComponent<Image>().sprite = image1;
    }

    public void ChangeButtonImage()
    {
        if (on)
        {
            this.GetComponent<Image>().sprite = image2;
            on = false;
        }
        else
        {
            this.GetComponent<Image>().sprite = image1;
            on = true;
        }
    }
}