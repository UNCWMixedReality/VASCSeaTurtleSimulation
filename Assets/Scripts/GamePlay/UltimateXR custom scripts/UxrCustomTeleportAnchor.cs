/*
*   This Script is used to generate custom 
*   teleportation events using the UltimateXR Framework. 
*   
*   Attatch this script to a GameObject on the ground layer to enable custom teleprot events.
* 
*   Events are triggered after the teleport process concludes
* 
*   Events available: 
*   OnTeleport - Triggers when the UxrAvatar is teleported to 
*                   a Gameobject with this script on it
*   
*   Written by: Nicholas Brunsink
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Locomotion;
using VASCDC;


public class UxrCustomTeleportAnchor : UxrTeleportSpawnCollider
{
    [SerializeField] private UnityEvent _onTeleport;

    protected override void OnEnable()
    {
        base.OnEnable();
        base.Teleported += HandleTeleport;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        base.Teleported -= HandleTeleport;
    }

    private void HandleTeleport(object sender, UxrAvatarMoveEventArgs e)
    {
        VASCEventLog.logMovementEvent("Teleported to " + this.name);
        _onTeleport.Invoke();
    }
}


