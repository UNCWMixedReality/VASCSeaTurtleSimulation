using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Altimit.UI
{
    [RequireComponent(typeof(UIElement))]
    public class HoverEffect : MonoBehaviour
    {
        Vector3 defaultScale = Vector3.one * .95f;
        Tweener tweener;

        void Start()
        {
            gameObject.Hold<UIElement>(x => {
                x.onHoverStart += HoverUp;
                x.onHoverEnd += HoverDown;
                x.onPressStart += OnPressStart;
                x.onPressEnd += OnPressEnd;
            });
            transform.localScale = defaultScale;
        }

        private void OnDestroy()
        {
            gameObject.Hold<UIElement>(x => {
                x.onHoverStart -= HoverUp;
                x.onHoverEnd -= HoverDown;
                x.onPressStart -= OnPressStart;
                x.onPressEnd -= OnPressEnd;
            });
        }

        void OnPressStart(PointerEventData eventData)
        {
            if (gameObject.AddOrGet<UIElement>().IsHovering)
            {
                HoverDown(eventData);
            }
        }

        void OnPressEnd(PointerEventData eventData)
        {
            if (gameObject.AddOrGet<UIElement>().IsHovering)
            {
                HoverUp(eventData);
            } else
            {
                HoverDown(eventData);
            }
        }

        void HoverUp(PointerEventData eventData)
        {
            tweener?.Kill();
            tweener = transform.DOScale(Vector3.one, .1f);
        }

        void HoverDown(PointerEventData eventData)
        {
            tweener?.Kill();
            tweener = transform.DOScale(defaultScale, .1f);
        }
    }
}
