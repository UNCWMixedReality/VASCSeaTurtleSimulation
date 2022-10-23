using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UltimateXR.Avatar;
using UltimateXR.Core.Components;
using UltimateXR.Locomotion;



public class UxrCustomTeleportEvent : UxrComponent<UxrTeleportSpawnCollider>
{
    public UnityEvent onTeleport;

    private void RaiseTeleported(object sender, UxrAvatarMoveEventArgs e) 
    {
        Debug.Log($"teleported to {e}");
        onTeleport?.Invoke();
    }

}



