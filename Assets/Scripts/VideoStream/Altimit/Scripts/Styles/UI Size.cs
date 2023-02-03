using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Altimit.UI
{
    public partial class AUI
    {
        public static GameObject FlexibleWidth(this GameObject go, bool includePreferred = true)
        {
            return go.Hold<LayoutElement>(x => { x.flexibleWidth = 10000; x.preferredWidth = includePreferred  ? 10000 : -1; });
        }

        public static GameObject FlexibleHeight(this GameObject go)
        {
            return go.Hold<LayoutElement>(x => { x.flexibleHeight = 10000; });
        }

        public static GameObject SetSize(this GameObject go, float size = SmallSize)
        {
            return go.SetSize(Vector2.one * size);
        }

        public static GameObject SetSize(this GameObject go, float width, float height)
        {
            return go.SetSize(new Vector2(width, height));
        }

        public static GameObject SetSize(this GameObject go, Vector2 size)
        {
            return go.Hold<RectTransform>(x => { x.sizeDelta = size; }).
                Hold<LayoutElement>(x => { x.minHeight = size.y; x.minWidth = size.x; x.preferredHeight = size.y; x.preferredWidth = size.x; });
        }

        public static GameObject SetHeight(this GameObject go, float height)
        {
            return go.Hold<RectTransform>(x => { x.sizeDelta = new Vector2(x.sizeDelta.x, height); }).
                Hold<LayoutElement>(x => { x.preferredHeight = height; x.preferredWidth = -1; }).
                MinHeight(height);
        }

        public static GameObject SetWidth(this GameObject go, float width)
        {
            return go.Hold<RectTransform>(x => { x.sizeDelta = new Vector2(width, x.sizeDelta.y); }).
                Hold<LayoutElement>(x => { x.preferredWidth = width; x.preferredHeight = -1; }).
                MinWidth(width);
        }

        public static GameObject FitSize(this GameObject go, bool horizontalFit = true, bool verticalFit = true)
        {
            return go.Hold<ContentSizeFitter>(x =>
            {
                x.horizontalFit = horizontalFit ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
                x.verticalFit = verticalFit ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
            });
        }

        public static GameObject MinSize(this GameObject go, Vector2 size)
        {
            return go.MinWidth(size.x).MinHeight(size.y);
        }

        public static GameObject MinHeight(this GameObject go, float height)
        {
            return go.Hold<LayoutElement>(x => x.minHeight = height);
        }

        public static GameObject MinWidth(this GameObject go, float width)
        {
            return go.Hold<LayoutElement>(x => x.minWidth = width);
        }

        public static GameObject StretchHorizontal(this GameObject go, float minX = 0, float maxX = 0)
        {
            return go.SetAnchor(new Vector2(0, .5f), new Vector2(1, .5f)).Hold<RectTransform>(z =>
                {
                    z.offsetMin = new Vector2(minX, z.offsetMin.y);
                    z.offsetMax = new Vector2(maxX, z.offsetMax.y);
                });
        }

        public static GameObject Stretch(this GameObject go, float margin = 0)
        {
            return go.SetAnchor(Vector3.zero, Vector3.one).SetMargin(margin);
        }

        public static GameObject SetPadding(this GameObject go, int padding = SmallSpace)
        {
            return go.SetPadding(new RectOffset(padding,padding,padding,padding));
        }

        public static GameObject SetPadding(this GameObject go, RectOffset margin)
        {
            return go.Hold<LayoutGroup>(x =>
            {
                x.padding = margin;
            });
        }

        public static GameObject SetSpacing(this GameObject go, float spacing)
        {
            return go.Hold<HorizontalOrVerticalLayoutGroup>(x =>
            {
                x.spacing = spacing;
            });
        }

        // TODO: Add other anchors
        public static GameObject SetPivot(this GameObject go, Vector2 pivot)
        {
            return go.Hold<RectTransform>(x =>
            {
                x.pivot = pivot;
            });
        }

        public static GameObject SetPositionX(this GameObject go, float positionX)
        {
            return go.Hold<RectTransform>(x =>
            {
                x.anchoredPosition = new Vector2(positionX, x.anchoredPosition.y);
            });
        }
        public static GameObject SetPositionY(this GameObject go, float positionY)
        {
            return go.Hold<RectTransform>(x =>
            {
                x.localPosition = new Vector2(x.localPosition.x, positionY);
                UnityEngine.Canvas.ForceUpdateCanvases();
            });
        }

        public static GameObject SetPosition(this GameObject go, Vector2 position)
        {
            return go.Hold<RectTransform>(x =>
            {
                x.anchoredPosition = position;
            });
        }


        public static GameObject SetAnchor(this GameObject go, Vector2 anchorMin, Vector2 anchorMax)
        {
            return go.Hold<RectTransform>(x =>
            {
                x.anchorMin = anchorMin;
                x.anchorMax = anchorMax;
            });
        }

        // TODO: Add other anchors
        public static GameObject SetAnchor(this GameObject go, TextAnchor anchor, StretchType stretchType = StretchType.None)
        {
            return go.Hold<RectTransform>(x =>
            {
                if (anchor == TextAnchor.MiddleLeft)
                {
                    x.anchorMin = new Vector2(0, .5f);
                    x.anchorMax = new Vector2(0, .5f);
                }
                else if (anchor == TextAnchor.LowerCenter)
                {
                    x.anchorMin = new Vector2(.5f, 0);
                    x.anchorMax = new Vector2(.5f, 0);
                }
                //if (stretchType.HasFlag(StretchType.Horizontal))
                //    x.anchorMin = Vector2.zero;
            });
        }

        public static GameObject SetMargin(this GameObject go, float margin = SmallSpace)
        {
            return go.SetMargin(Vector2.one * margin, -Vector2.one * margin);
        }

        public static GameObject SetMargin(this GameObject go, Vector2 offsetMin, Vector2 offsetMax)
        {
            return go.Hold<RectTransform>(z =>
            {
                z.offsetMin = offsetMin;
                z.offsetMax = offsetMax;
            });
        }

        public static GameObject SetMargin(this GameObject go, float margin, StretchType stretchType)
        {
            return go.Hold<RectTransform>(x =>
                {
                    if (stretchType.HasFlag(StretchType.Horizontal))
                    {
                        x.offsetMin = new Vector2(margin, x.offsetMin.y);
                        x.offsetMax = -new Vector2(margin, x.offsetMax.y);
                        x.anchorMax = new Vector2(1, 0);
                    }
                });
        }

        public static GameObject IgnoreLayout(this GameObject go, bool ignoreLayout = true)
        {
            return go.Hold<LayoutElement>(x => { x.ignoreLayout = ignoreLayout; });
        }
    }
}