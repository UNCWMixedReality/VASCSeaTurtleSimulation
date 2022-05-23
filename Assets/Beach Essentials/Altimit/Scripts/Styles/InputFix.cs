using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Altimit.UI
{
    public class InputFix : MonoBehaviour
    {
        TMP_InputField input;

        // Use this for initialization
        void Start()
        {
            input = gameObject.AddOrGet<TMP_InputField>();
            StartCoroutine(CoFix());
        }

        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator CoFix()
        {
            TMP_InputField.LineType oldType = input.lineType;
            input.lineType = oldType.Equals(TMP_InputField.LineType.SingleLine) ? TMP_InputField.LineType.MultiLineNewline : TMP_InputField.LineType.SingleLine;
            yield return new WaitForEndOfFrame();
            input.lineType = oldType;
        }
    }
}