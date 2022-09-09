﻿using System;
using System.Collections;
using System.Collections.Generic;
using DataCollection.Models;
//using UnityEditor.Recorder.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace DataCollection
{
    public class DcGrabInteractable : XRGrabInteractable
    {
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            DcDataLogging.LogInteraction(new Interaction(
                DateTime.Now, 
                true,
                this.name
            ));
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            DcDataLogging.LogInteraction(new Interaction(
                DateTime.Now, 
                false,
                this.name
            ));
        }
    }
}

