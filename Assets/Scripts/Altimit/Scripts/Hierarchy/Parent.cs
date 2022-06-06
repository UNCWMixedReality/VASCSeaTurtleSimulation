using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using UnityEngine.UI;

namespace Altimit.UI
{
    [ExecuteInEditMode]
    public class Parent : UIBehaviour
    {

        public RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public Action onDimensionsChange;
        public Action onDisable;
        public Action onEnable;

        RectTransform _rectTransform;

        new void OnDisable()
        {
            base.OnDisable();

            if (onDisable != null)
                onDisable();
        }

        new void OnEnable()
        {
            base.OnEnable();

            if (onEnable != null)
                onEnable();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            if (onDimensionsChange != null)
            {
                onDimensionsChange();
            }
        }
    }
}