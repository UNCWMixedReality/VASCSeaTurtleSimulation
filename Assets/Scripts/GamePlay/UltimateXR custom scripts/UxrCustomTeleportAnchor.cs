using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Locomotion;


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
        _onTeleport.Invoke();
    }
}


