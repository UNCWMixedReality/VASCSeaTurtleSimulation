using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Altimit.UI
{
    public partial class AUI
    {
        public static Action<GameObject> OnToggle = (x) => { };
        public static Action<GameObject> OnButton = (x) => { };
        public static Action<GameObject> OnText = (x) => { };
        public static Action<GameObject> OnCanvas = (x) => { };

        public static GameObject PanelManager(this GameObject go, GameObject back = null)
        {
            return go.
                Hold<PanelManager>(x => { x.BackButton = back.Get<Button>(); });
        }

        public static GameObject Canvas(this GameObject go)
        {
            OnCanvas?.Invoke(go);
            return go.Hold<Canvas>().Hold<CanvasScaler>().Hold<GraphicRaycaster>();
        }

        public static GameObject ToggleGroup(this GameObject go)
        {
            return go.
                Hold<ToggleGroup>(x => { });
        }

        public static GameObject Panel(this GameObject go)
        {
            return go.
                Hold<Panel>(x => { });
        }

        public static GameObject VList(this GameObject go, TextAnchor alignment)
        {
            return go.VList(SmallSpace, SmallSpace, alignment);
        }

        public static GameObject VList(this GameObject go, int padding = SmallSpace, int spacing = SmallSpace, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            go.Release<HorizontalLayoutGroup>();
            return go.Hold<VerticalLayoutGroup>(x =>
            {
                x.childAlignment = alignment;
                x.childControlHeight = true;
                x.childControlWidth = true;
                x.childForceExpandHeight = false;
                x.childForceExpandWidth = true;
                x.padding = new RectOffset(padding, padding, padding, padding);
                x.spacing = spacing;
            });
        }

        public static GameObject RowList(this GameObject go, TextAnchor alignment)
        {
            return go.HList(0, 0, alignment);
        }

        public static GameObject HList(this GameObject go, TextAnchor alignment)
        {
            return go.HList(SmallSpace, SmallSpace, alignment);
        }

        public static GameObject HList(this GameObject go, int padding = 0, int spacing = 0, TextAnchor alignment = TextAnchor.UpperLeft)
        {
            go.Release<VerticalLayoutGroup>();
            return go.Hold<HorizontalLayoutGroup>(x =>
            {
                x.childAlignment = alignment;
                x.childControlHeight = true;
                x.childControlWidth = true;
                x.childForceExpandHeight = false;
                x.childForceExpandWidth = false;
                x.padding = new RectOffset(padding, padding, padding, padding);
                x.spacing = spacing;
            });
        }

        public static GameObject Image(this GameObject go, Sprite sprite, Material material = null, UnityEngine.UI.Image.Type type = UnityEngine.UI.Image.Type.Simple)
        {
            return go.Image(AUI.SmallSize, sprite, material, type);
        }

        public static GameObject Image(this GameObject go, float size = AUI.SmallSize, Sprite sprite = null, Material material = null, UnityEngine.UI.Image.Type type = UnityEngine.UI.Image.Type.Simple)
        {
            return go.Hold<Image>(x =>
            {
                x.sprite = sprite;
                x.type = type;
            }).SetMaterial(material).SetSize(size);
        }
        
        public static GameObject Image(this GameObject go, Sprite sprite, UnityEngine.UI.Image.Type type)
        {
            return go.Image(sprite, null, type);
        }

        public static GameObject Bar(this GameObject go, Material frontMaterial = null, Material backMaterial = null)
        {
            return go.RoundImage(backMaterial).SetHeight(AUI.SmallSpace).Hold(
                    AUI.UI.RoundImage(frontMaterial).Stretch().Hold<RectTransform>(x=>x.pivot = new Vector2(0,.5f))
                );
        }

        public static GameObject Search(this GameObject go)
        {
            return go.RoundImage().HList().SetHeight(AUI.SmallSize).SetSprite(AUI.InputFieldBackground).Hold(
                    UI.Input("Search", TMP_InputField.InputType.Standard, 0).SetSprite(null),
                    UI.Image(GetSprite("Search"), Colored).SetSize(TinySize)
                );
        }

        public static GameObject Input(this GameObject go, string placeholder, bool isSingleLine)
        {
            return Input(go, placeholder, TMP_InputField.InputType.Standard, AUI.SmallSpace, isSingleLine);
        }

        public static GameObject Input(this GameObject go, string placeholder = null, TMP_InputField.InputType inputType = TMP_InputField.InputType.Standard, int padding = SmallSpace, bool isSingleLine = true)
        {
            GameObject placeholderGO, textGO, viewportGO;

            var textAlignment = isSingleLine ? TextAlignmentOptions.Left : TextAlignmentOptions.TopLeft;
            go.Hold<TMP_InputField>(x => {
                x.transition = Selectable.Transition.None;
                x.caretWidth = 1;
                x.ForceLabelUpdate();
                x.inputType = inputType;
                x.lineType = (isSingleLine ? TMP_InputField.LineType.SingleLine : TMP_InputField.LineType.MultiLineNewline);
            }).
                Hold<InputFix>().Image(AUI.InputFieldBackground,AUI.Default,UnityEngine.UI.Image.Type.Sliced).
                SetHeight(AUI.TinySize + (padding * 2)).
                FlexibleWidth().
                Hold(
                    viewportGO = UI.Hold<RectMask2D>().IgnoreLayout().SetMargin(padding).Hold(
                        placeholderGO = UI.Text(placeholder, Colored, textAlignment, true).Stretch(),
                        textGO = UI.Text(null, Colored, textAlignment).Stretch()
                    )
                );
            go.Get<TMP_InputField>().textViewport = viewportGO.Get<RectTransform>();
            go.Get<TMP_InputField>().textComponent = textGO.Get<TextMeshProUGUI>();
            go.Get<TMP_InputField>().placeholder = placeholderGO.Get<TextMeshProUGUI>();
            go.SetActive(false);
            go.SetActive(true);
            return go;
        }


        public static GameObject LabeledButton(this GameObject go, string text, Material material, Sprite sprite)
        {
            return go.Button(text, material, null, sprite);
        }
        public static GameObject Shadow(this GameObject go)
        {
            return go.IgnoreLayout().OnHeld(x => x.SetAnchor(Vector2.zero, Vector2.one).SetMargin(-8)).RoundImage(AUI.Dark, AUI.GetSprite("Shadow")).SetAlpha(.33f);
        }

        public static GameObject Button(this GameObject go, string text = null, Material material = null, Material textMaterial = null,
            Sprite buttonSprite = null, bool useShadow = true, bool useChildSize = true)
        {
            Image image;
            if (useShadow)
                go.Hold(AUI.UI.Shadow());
            if (useChildSize)
                go.HList(0, 0, TextAnchor.MiddleCenter).ExpandWidth(true);
            GameObject contentGO, textGO;
            go.Button().SetHeight(SmallSize).MinWidth(SmallSize).Hold(
                contentGO = AUI.UI.RoundImage(-1, material, buttonSprite).MinWidth(SmallSize).OnHeld(x=>x.Stretch()).SetHeight(SmallSize).Hold(out image).
                    Hold(
                        textGO = UI.Text(text, textMaterial, TextAlignmentOptions.Center)
                    )
            );
            if (useChildSize)
            {
                contentGO.HList(TextAnchor.MiddleCenter);
            } else
            {
                contentGO.Stretch();
                textGO.Stretch();
            }

            go.Hold<Button>(x => x.image = image);
            return go;
        }

        public static GameObject Checkbox(this GameObject go, string text = null, Material material = null,
            Material textMaterial = null,
            Sprite buttonSprite = null, bool useShadow = true)
        {
            Image image;
            if (useShadow)
                go.Hold(AUI.UI.Shadow());
            go.HList(0, 0, TextAnchor.MiddleCenter).ExpandWidth(true);
            GameObject contentGO, textGO;
            go.Toggle().SetHeight(SmallSize).MinWidth(SmallSize).Hold(
                contentGO = AUI.UI.RoundImage(-1, material, buttonSprite).MinWidth(SmallSize).OnHeld(x => x.Stretch()).SetHeight(SmallSize).
                    Hold(
                        AUI.UI.RoundImage().MinSize(Vector2.one * AUI.TinySize).Hold(
                            AUI.UI.Image().SetSprite(AUI.GetSprite("None")).SetMaterial(material).OnHeld(x=>x.Stretch(7)).Hold(out image)
                        ),
                        textGO = UI.Text(text, textMaterial, TextAlignmentOptions.Center)
                    )
            );

            go.Hold<Checkbox>(x => x.TargetImage = image);

            contentGO.HList(TextAnchor.MiddleCenter);

            return go;
        }

        public static GameObject ScaleButton(this GameObject go, string text = null, Material material = null, bool useChildSize = true)
        {
            return go.Button(text, material, null, null, true, useChildSize).Hold<ScaleButton>();
        }

        public static GameObject Scale(this GameObject go)
        {
            return go.Hold<ScaleButton>();
        }
        
        public static GameObject Toggle(this GameObject go, string text = null, Material material = null, Sprite sprite = null)
        {
            if (material == null)
                material = Colored;

            Image image = go.RoundImage(material).Get<Image>();

            GameObject spriteGO;
            go.Toggle().Hold<Toggle>(x => { x.image = image; }).
                SetHeight(SmallSize).
                HList().
                Hold(
                    spriteGO = UI.Image(sprite).SetSize(TinySize),
                    UI.Text(text, null, TextAlignmentOptions.Center).SetMargin().FlexibleWidth(false)
                );
            spriteGO.SetActive(sprite != null);
            return go;
        }

        public static GameObject Toggle(this GameObject go)
        {
            return go.Hold<Toggle>(x => x.transition = Selectable.Transition.None).Call(OnToggle);
        }

        public static GameObject Button(this GameObject go)
        {
            return go.Hold<Button>(x=>x.transition = Selectable.Transition.None).Call(OnButton);
        }

        public static GameObject Text(this GameObject go, Material material, TextAlignmentOptions alignment = TextAlignmentOptions.Left)
        {
            return go.Text(null, material, alignment, false);
        }

        public static GameObject Text(this GameObject go, TextAlignmentOptions alignment, Material material = null)
        {
            return go.Text(null, material, alignment, false);
        }

        public static GameObject Text(this GameObject go, string text, bool includeChildren, Material material = null)
        {
            return go.Text(text, material, TextAlignmentOptions.Left, false, includeChildren);
        }

        public static GameObject Text(this GameObject go, string text = null, Material material = null, TextAlignmentOptions alignment = TextAlignmentOptions.Left, bool isClear = false, bool includeChildren = false)
        {
            if (material == null)
                material = Default;

            return go.Hold<TextMeshProUGUI>(includeChildren, x =>
            {
                x.alignment = alignment;
                x.margin = new Vector4(0, -5, 0, -5);
                x.text = text;
                x.color = material.color.SetAlpha(isClear ? .5f : 1);
                x.font = Font;
                //x.fontSize = 20;
            }).Call(OnText);
        }
    }
}