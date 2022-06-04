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
        public static GameObject SetMaterial(this GameObject go, Material material = null)
        {
            if (go.AddOrGet<Image>().material.shader.name == "UI/Default" && Default != null)
                go.AddOrGet<Image>().material = Default;

            if (material != null)
                go.AddOrGet<Image>().material = material;
            return go;
        }

        public static GameObject OnClick(this GameObject go, GameObject target, HistoryType historyType = HistoryType.ClearHistory)
        {
            return go.OnClick(() => { go.Show(target, historyType); });
        }

        public static GameObject Show(this GameObject go, GameObject target, HistoryType historyType = HistoryType.ClearHistory)
        {
            if (go.GetInParent<Panel>().PanelManager == target.GetInParent<Panel>().PanelManager)
                target.Get<Panel>().Show(historyType);

            return go;
        }

        public static GameObject OnValueChanged(this GameObject go, GameObject target, HistoryType historyType = HistoryType.ClearHistory)
        {
            go.OnValueChanged((x) => { if (x && !target.Get<Panel>().IsShowing()) target.Get<Panel>().Show(historyType); });
            target.Get<Panel>().onEnable += (x) => { go.Get<Toggle>().isOn = true; };
            return go;
        }

        public static GameObject OnValueChanged(this GameObject go, UnityEngine.Events.UnityAction<bool> action)
        {
            go.Toggle().Hold<Toggle>(x => x.onValueChanged.AddListener(action));
            return go;
        }

        public static GameObject OnClick(this GameObject go, UnityEngine.Events.UnityAction action, bool resetListeners = true)
        {
            go.Hold<Button>(x => {
                if (resetListeners)
                    x.onClick.RemoveAllListeners();
                x.onClick.AddListener(action);
                x.transition = Selectable.Transition.None;
            });
            return go;
        }

        public static GameObject Mask(this GameObject go, bool showMaskGraphic = false)
        {
            go.Hold<Image>();
            return go.Hold<Mask>(x=>x.showMaskGraphic = showMaskGraphic);
        }

        public static GameObject SoftMask(this GameObject go, bool showMaskGraphic = true)
        {
            go.Hold<Image>(x => x.enabled = showMaskGraphic);
            return go.Hold<SoftMask>();
        }

        public static GameObject ExpandWidth(this GameObject go, bool value = true)
        {
            return go.Get<HorizontalLayoutGroup>(x => { x.childForceExpandWidth = value; }).Get<VerticalLayoutGroup>(x=> { x.childForceExpandWidth = value; });
        }

        public static GameObject ExpandHeight(this GameObject go, bool value = true)
        {
            return go.Get<HorizontalLayoutGroup>(x => { x.childForceExpandHeight = value; }).Get<VerticalLayoutGroup>(x => { x.childForceExpandHeight = value; });
        }

        public static GameObject ChildControl(this GameObject go, bool childControlWidth = true, bool childControlHeight = true)
        {
            return go.Get<HorizontalOrVerticalLayoutGroup>(x =>
            {
                x.childControlWidth = childControlWidth;
                x.childControlHeight = childControlHeight;
            });
        }

        public static GameObject SetChildAlignment(this GameObject go, TextAnchor childAlignment)
        {
            return go.Get<HorizontalOrVerticalLayoutGroup>(x => { x.childAlignment = childAlignment; });
        }


        public static GameObject FitSize(this GameObject go)
        {
            return go.FitWidth().FitHeight();
        }

        public static GameObject FitWidth(this GameObject go)
        {
            return go.Hold<ContentSizeFitter>(x => x.horizontalFit = ContentSizeFitter.FitMode.PreferredSize);
        }

        public static GameObject FitHeight(this GameObject go)
        {
            return go.Hold<ContentSizeFitter>(x => x.verticalFit = ContentSizeFitter.FitMode.PreferredSize);
        }

        public static GameObject SetSprite(this GameObject go, Sprite sprite = null)
        {
            return go.Hold<Image>(x => x.sprite = sprite);
        }

        public static TMP_FontAsset GetFont(string name)
        {
            return Resources.Load<TMP_FontAsset>("Fonts/" + name);
        }

        public static Sprite GetSprite(string name)
        {
            if (name == null)
                return null;
            return Resources.Load<Sprite>("Sprites/" + name);
        }

        public static GameObject RoundImage(this GameObject go, Material material, Sprite sprite = null)
        {
            return go.Image(-1, sprite == null ? GetSprite("Circle") : sprite, material, UnityEngine.UI.Image.Type.Sliced);
        }

        public static GameObject RoundImage(this GameObject go, int size=-1, Material material = null, Sprite sprite = null)
        {
            return go.Image(size, sprite == null ? GetSprite("Circle") : sprite, material, UnityEngine.UI.Image.Type.Sliced);
        }

        public static Material GetMaterial(string name, bool isNew = true)
        {
            Material material = Resources.Load<Material>("Materials/" + name);
            if (material == null)
                material = new Material(Shader.Find("Standard"));
            if (isNew)
                return new Material(material);
            return material;
        }

        public static GameObject UI
        {
            get
            {
                var go = A.New();
                go.layer = LayerMask.NameToLayer("UI");
                return go.Hold<RectTransform>();
            }
        }

        public static GameObject SetAlpha(this GameObject go, float alpha)
        {
            return go.Hold<Image>(x => x.color = x.color.SetAlpha(alpha));
        }
        public static Color SetAlpha(this Color c, float alpha)
        {
            c.a = alpha;
            return c;
        }
    }
}