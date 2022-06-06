using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using UnityEngine.UI;
using Altimit;

namespace Altimit.UI
{
    public class UIElement : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        public delegate void OnPointerEvent(PointerEventData eventData);
        // public delegate void ControllerInteractionEvent(ControllerInteractionEventArgs e);
        public OnPointerEvent onDrag;
        public OnPointerEvent onEndDrag;

        public OnPointerEvent onHoverStart;
        public OnPointerEvent onHoverEnd;

        public OnPointerEvent onClick;
        public OnPointerEvent onPressStart;
        public OnPointerEvent onPressEnd;

        public OnPointerEvent onGripStart;
        public OnPointerEvent onGrip;
        public OnPointerEvent onGripEnd;

        public OnPointerEvent onBeginGrab;
        public OnPointerEvent onGrab;
        public OnPointerEvent onEndGrab;

        public bool IncludeChildren = false;
        public bool IsHovering = false;

        [NonSerialized]
        public bool IsGripping = false;
        Rect oldRect;

        public RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        RectTransform _rectTransform;

        public bool IsGrabbing = false;

        public new void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Awake()
        {
            if (!Application.isPlaying)
                return;

            // oldRect = rectTransform.rect;
            base.Awake();
        }

        public new void Start()
        {

            if (IncludeChildren)
            {
                GetComponentsInChildren<RectTransform>().ToList().ForEach(x => RegisterElement(x.gameObject.AddOrGet<UIElement>()));
            }

            base.Start();
        }
        //eventually fill out with all necessary events inherited
        public void RegisterElement(UIElement element)
        {
            element.onGrab += OnGrab;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            IsGrabbing = true;

            //UIExtensions.GetUIPointers().Where(x=>x.pointerEventData.pre
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null)
                onDrag(eventData);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null)
                onEndDrag(eventData);
        }

        // Fires after the pointer is lifted
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
                onClick(eventData);
        }

        public virtual void OnBeginGrab(PointerEventData eventData)
        {
            IsGrabbing = true;
            if (onBeginGrab != null)
                onBeginGrab(eventData);
        }

        public virtual void OnGrab(PointerEventData eventData)
        {
            if (onGrab != null)
                onGrab(eventData);
        }

        public virtual void OnEndGrab(PointerEventData eventData)
        {
            IsGrabbing = false;
            if (onEndGrab != null)
                onEndGrab(eventData);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            IsHovering = true;
            //    Debug.Log("ENTERED " + gameObject.name);
            if (onHoverStart != null)
                onHoverStart?.Invoke(eventData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            IsHovering = false;
            //    Debug.Log("EXITED " + gameObject.name);
            if (onHoverEnd != null)
                onHoverEnd(eventData);
        }

        public void OnGripStart(PointerEventData e)
        {
            IsGripping = true;
            if (onGripStart != null)
                onGripStart(e);
        }
        public void OnGripEnd(PointerEventData e)
        {
            IsGripping = false;
            if (onGripEnd != null)
                onGripEnd(e);
        }

        public void OnGrip(PointerEventData e)
        {
            if (onGrip != null)
                onGrip(e);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            onPressEnd?.Invoke(eventData);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            onPressStart?.Invoke(eventData);
        }

        private void Update()
        {
            /*
            if (rectTransform.rect.size != oldRect.size && OnRectTransformDimensionsChanged != null)
            {
             //   Debug.Log(t.rect.size + ", " + oldRect.size);
                OnRectTransformDimensionsChanged(this, new EventArgs());
            }
            oldRect = rectTransform.rect;
            */
        }

        public Action onDimensionsChange;

        protected override void OnRectTransformDimensionsChange()
        {
            if (onDimensionsChange != null)
            {
                onDimensionsChange();
            }
        }
    }
}