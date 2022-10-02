using System;
using System.Collections;
using System.Collections.Generic;
using DataCollection.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UltimateXR.Locomotion;
using UltimateXR.Core.Components.Composite;


class UxrCustomTeleportEvent : MonoBehavior
{
    private void OnEnable()
    {
        UxrManager.AvatarMoved += UxrManager_AvatarMoved;
    }

    private void OnDisable()
    {
        UxrManager.AvatarMoved -= UxrManager_AvatarMoved;
    }

    private void UxrManager_AvatarMoved(object sender, UxrAvatarMoveEventArgs e)
    {
        Debug.Log($"Avatar moved from {e.OldPosition} to {e.NewPosition}");
    }

}