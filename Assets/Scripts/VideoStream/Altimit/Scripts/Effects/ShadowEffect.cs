using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Altimit.UI
{
    [ExecuteInEditMode]
    public class ShadowEffect : MonoBehaviour
    {
        [SerializeField]
        bool isInitialized = false;
        [SerializeField]
        SetParent setParent;
        [SerializeField]
        Image shadowImage;
        [SerializeField]
        Image image;

        protected void Awake()
        {
            if (isInitialized)
                return;
            isInitialized = true;
            GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Shadow"), transform.parent);
            setParent = go.GetComponent<SetParent>();
            setParent.Parent = transform;
            shadowImage = go.GetComponent<Image>();
            image = gameObject.AddOrGet<Image>();
            UpdateShadow();
        }

        // Update is called once per frame
        void Update()
        {
            shadowImage.type = image.type;
            setParent.Padding = new Edge(image.type.Equals(Image.Type.Sliced) ? 7 : 7);
        }

        public void OnTransformParentChanged()
        {
            UpdateShadow();
        }

        void UpdateShadow()
        {
            if (!shadowImage)
                return;
            shadowImage.transform.SetParent(transform.parent);
            shadowImage.transform.SetAsFirstSibling();
            shadowImage.enabled = image.sprite == AUI.GetSprite("Circle");
        }
    }
}
