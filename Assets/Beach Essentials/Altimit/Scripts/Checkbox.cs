using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Altimit.UI {
    public class Checkbox : MonoBehaviour
    {
        public Image TargetImage;
        Sprite onSprite;
        Sprite offSprite;
        Toggle toggle;

        void Start()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnValueChanged);
            onSprite = AUI.GetSprite("Check");
            offSprite = AUI.GetSprite("None");
        }

        public virtual void OnValueChanged(bool value)
        {
            TargetImage.sprite = value ? onSprite : offSprite;
        }
    }
}