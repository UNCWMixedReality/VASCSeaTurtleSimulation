using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Altimit.UI {

    public class ShuffleEffect : MonoBehaviour
    {
        public GameObject BackGO, BackContentGO;
        Vector2 contentSize;
        //bool isPlaying = false;
        bool isVisible = false;
        Sequence sequence;

        // Start is called before the first frame update
        void Start()
        {
            contentSize = BackContentGO.GetRectTransform().sizeDelta;
            BackGO.MinSize(Vector2.zero);
            BackContentGO.IgnoreLayout();
            BackContentGO.SetAnchor(TextAnchor.MiddleLeft).SetPivot(new Vector2(0, .5f)).SetPosition(Vector2.zero);
        }

        // Update is called once per frame
        public void Update()
        {
        }

        public void ToggleVisibility()
        {
            SetVisibility(!isVisible);
        }

        public virtual Sequence SetVisibility(bool value)
        {
            Canvas.ForceUpdateCanvases();

            sequence?.Kill();
            sequence = DOTween.Sequence();

            if (value)
            {
                //Get the on content size
                BackGO.MinSize(-Vector2.one);
                BackContentGO.IgnoreLayout(false);
                Canvas.ForceUpdateCanvases();
                contentSize = BackContentGO.GetRectTransform().sizeDelta;
                BackContentGO.IgnoreLayout();
                BackGO.MinSize(Vector2.zero);

                sequence.AppendCallback(()=> {

                    //Return to off state
                    BackContentGO.SetAnchor(TextAnchor.MiddleLeft).SetPivot(new Vector2(0, .5f)).SetPosition(Vector2.zero);
                    Canvas.ForceUpdateCanvases();
                    GetRoot().SetPivot(new Vector2(0, .5f));
                }).Append(BackGO.AddOrGet<LayoutElement>().DOMinSize(contentSize, .5f))
                .AppendCallback(() =>
                {
                    BackContentGO.IgnoreLayout(false);
                    BackGO.MinSize(-Vector2.one);
                    GetRoot().SetPivot(Vector2.one*.5f);
                });
                
            } else
            {
                Vector2 contentSize = BackContentGO.GetRectTransform().sizeDelta;

                sequence.AppendCallback(() =>
                {
                    BackContentGO.IgnoreLayout();
                    BackGO.MinSize(contentSize);
                    GetRoot().SetPivot(new Vector2(0, .5f));
                    BackContentGO.SetAnchor(TextAnchor.MiddleLeft).SetPivot(new Vector2(0, .5f)).SetPosition(Vector2.zero);
                }).Append(BackGO.AddOrGet<LayoutElement>().DOMinSize(Vector2.zero, .5f)).AppendCallback(() =>
                {
                    BackGO.MinSize(-Vector2.one);
                    BackContentGO.IgnoreLayout();
                    GetRoot().SetPivot(Vector2.one * .5f);
                });
            }
            isVisible = value;
            return sequence.Play();
        }

        public GameObject GetRoot()
        {
            var rootGO = gameObject;
            var rootGOParent = rootGO.transform.parent.gameObject;
            while (rootGOParent != null && rootGOParent.Has<RectTransform>() && rootGOParent.Has<LayoutGroup>()
                && rootGOParent.Has<ContentSizeFitter>() && !rootGOParent.Has<Canvas>())
            {
                rootGO = rootGOParent;
                rootGOParent = rootGO.transform.parent.gameObject;
            }
            return rootGO;
        }

    }
}