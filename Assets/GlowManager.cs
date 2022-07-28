using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowManager : MonoBehaviour
{
    /*
     * To access methods in this script, keep a reference to the GlowManager component attached
     * to either left or right controller object. Right controller has A/B buttons, left has A/B.
     * 
     * Constants below are used to reference button id when calling ToggleGlow()
     * ID_AX is the index of the A and X button, ID_BY is B and Y.
     * 
     * i.e. toggle glow on the A button by calling glowManagerInstance.ToggleGlow(GlowManager.ID_AX)
     * on the GlowManager instance attached to the right controller.
     */
    
    public const int ID_AX = 0;
    public const int ID_BY = 1;
    public const int ID_Oculus = 2;
    public const int ID_ThumbStick = 3;
    
    // buttons array should be populated in inspector, already done in Quest 2 controller prefabs
    public GameObject[] buttons;
    
    public void ToggleGlow(int id)
    {
        // toggles glow of button specified with id
        if (id is < 0 or > 3) return;
        
        var btn = buttons[id];
        btn.SetActive(!btn.activeInHierarchy);
    }

    public void ClearGlow()
    {
        // deactivates glow of all buttons in attached glowManager
        foreach (var btn in buttons)
        {
            btn.SetActive(false);
        }
    }
    
    public void NewGlow(int id)
    {
        // deactivates all glow, then activates glow of specified button
        if (id is < 0 or > 3) return;
        
        ClearGlow();
        ToggleGlow(id);
    }
}
