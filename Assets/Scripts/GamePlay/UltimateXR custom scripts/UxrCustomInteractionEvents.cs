using System;
using System.Collections;
using System.Collections.Generic;
using DataCollection.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UltimateXR.Manipulation;
using UltimateXR.Core.Components.Composite;
using VASCDataCollection;


class UxrCustomInteractionEvents : UxrGrabbableObjectComponent<UxrCustomInteractionEvents>
{
    public UnityEvent onGrab;
    public UnityEvent whileGrabbing;
    public UnityEvent whileReleasing;
    public UnityEvent onRelease;    
    public UnityEvent whilePlacing;
    public UnityEvent onPlaced;
    public string anchorTag;
    public UnityEvent onSpecificPlaced;
    public UnityEvent whileConstraintsApplying;
    public UnityEvent onConstraintsApplied;




    protected override void OnObjectGrabbing(UxrManipulationEventArgs e)
    {
        whileGrabbing.Invoke();
    }

    protected override void OnObjectGrabbed(UxrManipulationEventArgs e)
    {
        onGrab.Invoke();
        VASCDataCollection.EventLog.logInteractionEvent("Grabbed " + this.name);
    }

    protected override void OnObjectReleasing(UxrManipulationEventArgs e)
    {
        whileReleasing.Invoke();
    }

    protected override void OnObjectReleased(UxrManipulationEventArgs e)
    {
        onRelease.Invoke();
        VASCDataCollection.EventLog.logInteractionEvent("Released " + this.name);
    }

    protected override void OnObjectPlacing(UxrManipulationEventArgs e)
    {
        whilePlacing.Invoke();
    }

    protected override void OnObjectPlaced(UxrManipulationEventArgs e)
    {
        if (e.GrabbableAnchor.tag == anchorTag)
            onSpecificPlaced.Invoke();
        else
            onPlaced.Invoke();

        VASCDataCollection.EventLog.logInteractionEvent("Placing: " + this.name + " on anchor: " + e.GrabbableAnchor.name);
    }

    protected override void OnObjectConstraintsApplying(UxrApplyConstraintsEventArgs e)
    {
        whileConstraintsApplying.Invoke();
    }

    protected override void OnObjectConstraintsApplied(UxrApplyConstraintsEventArgs e)
    {
        onConstraintsApplied.Invoke();
    }
}
