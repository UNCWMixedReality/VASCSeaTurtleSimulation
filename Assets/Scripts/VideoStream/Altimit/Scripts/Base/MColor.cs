using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using Altimit.Serialization;

namespace Altimit.UI
{
    [ExecuteInEditMode]
    public class MColor : MonoBehaviour
    {

        public Material DefaultMaterial;
        [ReadOnly]
        public Color DefaultColor;
        public bool IgnoreParentGroups = false;
        bool isInitialized = false;
        Image image;
        TextMeshProUGUI text;
        HierarchyObserver childObserver;

        void OnEnable()
        {
            Init();

            //Debug.Log(DefaultColor.ToString());
        }

        void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (isInitialized)
                return;

            image = GetComponent<Image>();
            text = GetComponent<TextMeshProUGUI>();
            childObserver = gameObject.AddOrGet<HierarchyObserver>();
            childObserver.onAddChild += (x) => UpdateChildren();

            if (Application.isPlaying)
            {

                isInitialized = true;

                if (image != null)
                {
                    //if (IsDefault(image.material))
                    //{
                    //   image.material.color = Color.white;
                    //}
                    //else
                    //{
                    // if (image.material.color != Color.white)
                    //   image.color = image.material.color;
                    //image.material = new Material(image.material);
                    //}
                }
                UpdateChildren();
            }

            if (DefaultMaterial != null)
            {
                DefaultColor = DefaultMaterial.color;
            }
            else
            {

                DefaultColor = GetColor(transform);
            }
        }

        void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            UpdateDefaultMaterial();
            ShiftChildren();
        }

        void ShiftChildren()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, 0);
            }
        }

        void UpdateDefaultMaterial()
        {
            if (DefaultMaterial != null)
            {
                if (text != null)
                {
                    text.color = DefaultMaterial.color;
                }
            }
        }

        static bool IsDefault(Material material)
        {
            //return material.shader.name.Contains("Default");
            return material == null;
        }

        public static Color GetColor(Transform t)
        {
            TextMeshProUGUI text = t.GetComponent<TextMeshProUGUI>();
            if (text != null)
                return text.color;

            Image image = t.GetComponent<Image>();
            if (image != null)
            {
                if (!IsDefault(image.material))
                    return image.material.color;

                return image.color;
            }
            return Color.white;
        }

        public Tweener SetColor(Color c, bool immediate = false)
        {
            return SetColor(transform, c, immediate);
        }

        public static Tweener SetColor(Transform t, Color c, bool immediate = false)
        {
            if (!Application.isPlaying)
                return default(Tweener);

            //float duration = immediate ? 0 : .1f;

            TextMeshProUGUI text = t.GetComponent<TextMeshProUGUI>();
            if (text != null)
                text.color = c;//return text.DOColor(c, duration);

            Image image = t.GetComponent<Image>();
            if (image != null)
            {
                if (!IsDefault(image.material))
                    image.material.color = c;//return image.material.DOColor(c,duration);

                //return image.DOColor(c, duration);
            }

            return null;
        }

        public void ChangeColor(Color oldColor, Color newColor, bool includeSelf = true)
        {
            MColor[] ts = MColor.GetColors(transform, includeSelf).ToArray();
            Debug.Log(ts.Length);
            //  Debug.Log(GetComponent<PrimerColor>().DefaultColor == ColorA);
            for (int i = 0; i < ts.Length; i++)
            {

                if (SameColors(ts[i].DefaultColor, oldColor))
                    SetColor(ts[i].transform, newColor, true);
            }
        }


        public static bool SameColors(Color a, Color b)
        {
            return (Mathf.Abs(a.r - b.r) < .1f && Mathf.Abs(a.g - b.g) < .1f && Mathf.Abs(a.b - b.b) < .1f);
        }

        public static List<MColor> GetColors(Transform t, bool includeSelf = true)
        {
            List<MColor> colors = new List<MColor>();

            if (includeSelf)
            {
                MColor color = t.GetComponent<MColor>();
                if (color != null)
                {
                    colors.Add(color);
                }
            }

            for (int i = 0; i < t.childCount; i++)
            {
                MColor c = t.GetChild(i).GetComponent<MColor>();
                if ((c != null && !c.IgnoreParentGroups && c.isInitialized) || c == null)
                {
                    colors.AddRange(GetColors(t.GetChild(i)));
                }
            }
            return colors;
        }

        public void UpdateChildren()
        {
            GetComponentsInChildren<TextMeshProUGUI>().Select(x => x.gameObject).ToList().
            Union(GetComponentsInChildren<Image>().Select(x => x.gameObject).ToList()).
            ToList().ForEach(x => x.AddOrGet<MColor>());
        }
    }
}