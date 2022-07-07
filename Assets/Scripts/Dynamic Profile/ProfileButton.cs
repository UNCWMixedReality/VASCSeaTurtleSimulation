using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    // declare anything attached to the button prefab here that you want to  access
    
    // when using text mesh pro you must specify it otherwise it will access legacy text instead
    public TextMeshProUGUI profileText;
    
    
    /* you can add any number of images (maybe add a background image?) if the sizing gets messed up then adjust the grid
     layout settings on scroll view > viewport > content */
    public Image profileSprite;
}
