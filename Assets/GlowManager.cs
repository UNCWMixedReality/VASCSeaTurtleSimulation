using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowManager : MonoBehaviour
{
    /*
     * Constants below are used to reference button id when calling ToggleGlow()
     * i.e. toggle glow on the X button by calling glowManagerInstance.ToggleGlow(GlowManager.ID_AX)
     * A/B and X/Y are left/right controller dependent
     */
    
    public const int ID_AX = 0;
    public const int ID_BY = 1;
    public const int ID_Oculus = 2;
    public const int ID_TriggerFront = 3;
    public const int ID_TriggerGrip = 4;
    
    public GameObject[] buttons;

    public void ToggleGlow(int id)
    {
        if (id is < 0 or > 3) return;
        
        var btn = buttons[id];
        btn.SetActive(!btn.activeInHierarchy);
    }

    public void ClearGlow()
    {
        foreach (var btn in buttons)
        {
            btn.SetActive(false);
        }
    }
}
