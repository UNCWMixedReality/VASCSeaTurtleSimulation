using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

namespace Altimit.UI
{
    [RequireComponent(typeof(UIElement))]
    public class HighlightEffect : MonoBehaviour
    {
        Image image;
        void Start()
        {
            image = gameObject.AddOrGet<Image>();
            SetHighlight(false);
            gameObject.Hold<UIElement>(x => {
                x.onHoverStart += OnHoverStart;
                x.onHoverEnd += OnHoverEnd;
            });
        }

        private void OnDestroy()
        {
            gameObject.Hold<UIElement>(x => {
                x.onHoverStart -= OnHoverStart;
                x.onHoverEnd -= OnHoverEnd;
            });
        }

        void OnHoverStart(PointerEventData eventData)
        {
            SetHighlight(true);
        }

        void OnHoverEnd(PointerEventData eventData)
        {
            SetHighlight(false);
        }

        void SetHighlight(bool value) {
            image.enabled = value;
        }
    }
}
