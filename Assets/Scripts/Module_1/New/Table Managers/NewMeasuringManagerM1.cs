using System.Collections;
using System.Collections.Generic;
using Altimit.UI;
using UnityEngine;

public class NewMeasuringManagerM1 : MonoBehaviour
{
    // Class Variables
    #region Class Variables
    
    //Front Fin
    public GameObject FrontFin;
    public SpriteRenderer FrontFinArrow;
    //Back Fin
    public GameObject BackFin;
    public SpriteRenderer BackFinArrow;
    //Shell Length
    public GameObject ShellLength;
    public SpriteRenderer ShellVerticalArrow;
    //Shell Width
    public GameObject ShellWidth;
    public SpriteRenderer ShellHorizontalArrow;
    //Yellow Arrow Color
    private Color arrowTransparentYellow = new Color(1, 1, 0, 0);

    #endregion
    
    public void PrepareFrontFin() //Enables FrontFin for measuring
    {
        FrontFinArrow.color = arrowTransparentYellow;          //Sets Front Fin arrow to yellow
        FrontFin.SetActive(true);                              //Turns FrontFin on
    }

    public void PrepareBackFin() //Enables BackFin for measuring
    {
        FrontFin.SetActive(false);                             //Turns FrontFin off
        BackFinArrow.color = arrowTransparentYellow;           //Sets Back Fin arrow to yellow
        BackFin.SetActive(true);                               //Turns BackFin on
    }

    public void PrepareShellLength() //Enables Shell length measuring
    {
        BackFin.SetActive(false);                              //Turns BackFin off
        ShellVerticalArrow.color = arrowTransparentYellow;     //Sets Shell Length arrow to yellow
        ShellLength.SetActive(true);                           //Turns ShellLength on
    }

    public void PrepareShellWidth() //Enables Shell Width measuring
    {
        ShellLength.SetActive(false);                            //Turns ShellLength off
        ShellHorizontalArrow.color = arrowTransparentYellow;     //Set Shell Width arrow to yellow
        ShellWidth.SetActive(true);                              //Turns ShellWidth on
    }
}


