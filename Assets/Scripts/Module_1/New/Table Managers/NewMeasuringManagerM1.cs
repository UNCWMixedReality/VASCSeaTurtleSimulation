using System.Collections;
using System.Collections.Generic;
using Altimit.UI;
using UnityEngine;
using UnityEngine.UI;

public class NewMeasuringManagerM1 : MonoBehaviour
{
    // Class Variables
    #region Class Variables
    //tools
    public GameObject tapeMeasure;
    public GameObject calliper;
    //Front Fin
    public GameObject FrontFin;
    public SpriteRenderer FrontFinArrow;
    public Image frontFinsX;
    public Image frontFinsCheck;
    public Image topFins;
    //Back Fin
    public GameObject BackFin;
    public SpriteRenderer BackFinArrow;
    public Image backFinsX;
    public Image backFinsCheck;
    public Image bottomFins;
    //Shell
    public Image verticalLine;
    public Image horizontalLine;
    public Image shellImage;
    public Image ShellX;
    public Image ShellCheck;
    //Shell Length
    public GameObject ShellLength;
    public SpriteRenderer ShellVerticalArrow;
    //Shell Width
    public GameObject ShellWidth;
    public SpriteRenderer ShellHorizontalArrow;
    //Yellow Arrow Color
    private Color arrowYellow = new Color(1, 1, 0, 1);

    #endregion
    
    public void PrepareFrontFin() //Enables FrontFin for measuring
    {
        FrontFinArrow.color = arrowYellow;                     //Sets Front Fin arrow to yellow
        FrontFin.SetActive(true);                              //Turns FrontFin on
    }

    public void PrepareBackFin() //Enables BackFin for measuring
    {
        topFins.color = new Color(1, 1, 1, 1);
        frontFinsX.color = new Color(1, 1, 1, 0);
        frontFinsCheck.color = new Color(1, 1, 1, 1);
        FrontFin.SetActive(false);                             //Turns FrontFin off
        BackFinArrow.color = arrowYellow;                      //Sets Back Fin arrow to yellow
        BackFin.SetActive(true);                               //Turns BackFin on
    }

    public void PrepareShellLength() //Enables Shell length measuring
    {
        calliper.SetActive(false);
        tapeMeasure.SetActive(true);
        bottomFins.color = new Color(1, 1, 1, 1);
        backFinsX.color = new Color(1, 1, 1, 0);
        backFinsCheck.color = new Color(1, 1, 1, 1);
        BackFin.SetActive(false);                              //Turns BackFin off
        ShellVerticalArrow.color = arrowYellow;                //Sets Shell Length arrow to yellow
        ShellLength.SetActive(true);                           //Turns ShellLength on
    }

    public void PrepareShellWidth() //Enables Shell Width measuring
    {
        verticalLine.color = new Color(1, 1, 1, 1);
        ShellLength.SetActive(false);                            //Turns ShellLength off
        ShellHorizontalArrow.color = arrowYellow;                //Set Shell Width arrow to yellow
        ShellWidth.SetActive(true);                              //Turns ShellWidth on
    }

    public void FinishTable()
    {
        ShellWidth.SetActive(false);
        verticalLine.color = new Color(1, 1, 1, 0);
        horizontalLine.color = new Color(1, 1, 1, 0);
        shellImage.color = new Color(1, 1, 1, 1);
        ShellX.color = new Color(1, 1, 1, 0);
        ShellCheck.color = new Color(1, 1, 1, 1);
    }
}


