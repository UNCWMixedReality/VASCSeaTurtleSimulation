using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UltimateXR.Avatar;
using UltimateXR.Core.Components;
using UltimateXR.Core;
using UltimateXR.Locomotion;


public class UxrCustomTeleportAnchor : MonoBehaviour
{
    public UnityEvent onTeleport;
    public UxrTeleportSpawnCollider anchor;
    

    private void OnEnable()
    {
        anchor.Teleported += UxrManager_AvatarMoved;
    }

    private void OnDisable()
    {
        anchor.Teleported -= UxrManager_AvatarMoved;
    }

    private void UxrManager_AvatarMoved(object sender, UxrAvatarMoveEventArgs e)
    {
        Debug.Log($"Avatar moved from {e.OldPosition} to {e.NewPosition}");
        onTeleport.Invoke();      
    }

}



