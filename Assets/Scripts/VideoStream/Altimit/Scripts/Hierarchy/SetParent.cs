using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Altimit.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class SetParent : UIBehaviour
    {

        public Transform Parent;
        Transform oldParent;
        public bool SetPosition = false;
        public bool SetY = false;
        public bool SetSize = false;
        public bool SetWidth = false;
        public bool SetHeight = false;
        public bool SetRotation = false;

        public Edge Padding;

        Parent parent;
        RectTransform rectTransform;
        bool wasParent = false;

        protected override void OnEnable()
        {
            oldParent = null;
            base.OnEnable();
        }

        // Use this for initialization
        protected override void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            OnDimensionsChange();
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            //transform.localEulerAngles = Vector3.zero;
            if (Parent != null && oldParent == null)
            {
                //Parent.onDimensionsChange += OnDimensionsChange;
                oldParent = Parent;
                setParent(Parent.gameObject);

                /*
                Vector3 targetPos = Parent.rectTransform.position;
                bool changed = false;
                if (SetSize)
                {
                    Vector2 targetScale = Parent.rectTransform.sizeDelta + Padding.Size;
                    if (targetScale != rect.sizeDelta)
                    {
                        rect.sizeDelta = targetScale;
                        changed = true;
                    }
                    targetPos += new Vector3(Padding.Right - Padding.Left, Padding.Top - Padding.Bottom,0);
                }
                //  rect.anchorMin = Parent.anchorMin;
                //  rect.anchorMax = Parent.anchorMax;
                // rect.pivot = Parent.pivot;
                if (SetPosition && rect.position != targetPos)
                {
                    rect.position = targetPos;
                    changed = true;
                }
                if (changed)
                {
                    Canvas.ForceUpdateCanvases();
                }
                wasParent = true;
                */
            }

            if (Parent != null)
            {
                OnDimensionsChange();
            }

            if (Parent == null && oldParent != null)
            {
                //  oldParent.onDimensionsChange -= OnDimensionsChange;
                if (Application.isPlaying)
                    Destroy(gameObject);
            }
        }

        void setParent(GameObject go)
        {
            parent = go.AddOrGet<Parent>();
            parent.onEnable += OnParentEnable;
            parent.onDisable += OnParentDisable;
        }

        void OnParentEnable()
        {
            gameObject.SetActive(true);
        }

        void OnParentDisable()
        {
            if (Application.isPlaying)
                gameObject.SetActive(false);
        }

        new void OnDestroy()
        {
            if (parent != null)
            {
                parent.onDisable -= OnParentDisable;
                parent.onEnable -= OnParentEnable;
                // Parent.onDimensionsChange -= OnDimensionsChange;
            }
            base.OnDestroy();
        }

        void OnDimensionsChange()
        {
            if (parent == null || parent.rectTransform == null)
                return;

            transform.localScale = parent.transform.localScale;

            if (SetRotation)
                transform.eulerAngles = parent.transform.eulerAngles;

            Vector3 targetPos = parent.rectTransform.position;
            bool changed = false;

            Vector2 targetScale = GetAbsoluteScale(parent.rectTransform) + Padding.Size;

            if (SetSize)
            {
                if (targetScale != rectTransform.sizeDelta)
                {
                    rectTransform.sizeDelta = targetScale;
                    changed = true;
                }
                targetPos += new Vector3(Padding.Right - Padding.Left, Padding.Top - Padding.Bottom, 0);
            }
            if (SetWidth)
            {
                rectTransform.sizeDelta = new Vector2(targetScale.x, rectTransform.sizeDelta.y);
            }


            //  rectTransform.anchorMin = parent.rectTransform.anchorMin;
            // rectTransform.anchorMax = parent.rectTransform.anchorMax;
            // rectTransform.pivot = parent.rectTransform.pivot;

            if (SetPosition && rectTransform.position != targetPos)
            {
                rectTransform.position = targetPos;
                changed = true;
            }
            if (changed)
            {
                // Canvas.ForceUpdateCanvases();
            }
        }

        Component CopyComponent(Component original, GameObject destination)
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            // Copied fields can be restricted with BindingFlags
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy;
        }

        Vector2 GetAbsoluteScale(RectTransform rectTransform)
        {
            Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
            Rect pixelRect = RectTransformUtility.PixelAdjustRect(rectTransform, canvas);
            return pixelRect.size;
        }
    }

    [System.Serializable]
    public struct Edge
    {
        public float Top, Bottom, Left, Right;

        public float Height
        {
            get
            {
                return Top + Bottom;
            }
        }
        public float Width
        {
            get
            {
                return Left + Right;
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
            }
        }

        public Edge(float top, float bottom, float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public Edge(float size)
        {
            Top = size;
            Bottom = size;
            Left = size;
            Right = size;
        }
    }
}