/*
*   This Script is used to generate custom 
*   interaction events using the UltimateXR Framework. 
*   
*   Attatch this script to a GameObject with 
*   a UXRGrabbableObject script on it to enable custom events.
* 
*   Events can be triggered both during and after a desired action
* 
*   Events available: 
*   OnGrab - Triggered when a grabbable object is grabbed
*   OnRelease - Triggered when a grabbable object is release
*   OnPlace - Triggered when a grabbable object is placed in a UXRGrabbableObjectAnchor
*   OnConstraintsApply - Not sure when this is triggered (?)
*   
*   OnPlace has the ability to be tag specific using the onSpecificPlaced event
*   
*   Written by: Nicholas Brunsink
*/


using System;
using System.Collections;
using System.Collections.Generic;
using DataCollection.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UltimateXR.Manipulation;
using UltimateXR.Core.Components.Composite;
using VASCDC;


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
        VASCEventLog.logInteractionEvent("Grabbed " + this.name);
        onGrab.Invoke();
    }

    protected override void OnObjectReleasing(UxrManipulationEventArgs e)
    {
        whileReleasing.Invoke();
    }

    protected override void OnObjectReleased(UxrManipulationEventArgs e)
    {
        VASCEventLog.logInteractionEvent("Released " + this.name);        
        onRelease.Invoke();

    }

    protected override void OnObjectPlacing(UxrManipulationEventArgs e)
    {
        whilePlacing.Invoke();
    }

    protected override void OnObjectPlaced(UxrManipulationEventArgs e)
    {
        VASCEventLog.logInteractionEvent("Placing: " + this.name + " on anchor: " + e.GrabbableAnchor.name);

        if (e.GrabbableAnchor.tag == anchorTag)
            onSpecificPlaced.Invoke();
        else
            onPlaced.Invoke();
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
