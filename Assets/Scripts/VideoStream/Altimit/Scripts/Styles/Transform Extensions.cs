using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using SoftMasking;

namespace Altimit.UI
{
    public static partial class AUI
    {
        public static GameObject RotateZ(this GameObject go, float rotationZ)
        {
            UnityEngine.Canvas.ForceUpdateCanvases();
            go.Hold<RectTransform>(x => x.localEulerAngles = new Vector3(x.localEulerAngles.x, x.localEulerAngles.y, rotationZ));
            //go.Hold<Transform>(x=> x.localEulerAngles = new Vector3(x.localEulerAngles.x, x.localEulerAngles.y, rotationZ));
            return go;
        }
    }
}