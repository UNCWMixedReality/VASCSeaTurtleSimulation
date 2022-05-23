using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace DataCollection
{
    public class DcTeleportationArea : TeleportationArea
    {
        protected override void OnSelectExit(XRBaseInteractor baseInteractable)
        {
            base.OnSelectExit(baseInteractable);
            var position = baseInteractable.transform.position;
            DcDataLogging.LogMovement(new Models.Movement(
                new System.Numerics.Vector3(
                    position.x, 
                    position.y,
                    position.z
                    ),
                SceneManager.GetActiveScene().name,
                DateTime.Now
            ));
        }
    }
}