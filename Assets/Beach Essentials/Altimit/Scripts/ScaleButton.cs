using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ScaleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ArrowGO;
    Tweener tweener;
    const float defaultScale = .997f;

    void Start()
    {
        transform.localScale = Vector3.one * defaultScale;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        tweener?.Kill();
        tweener = transform.DOScale(Vector3.one * (defaultScale + .011f), .1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tweener?.Kill();
        tweener = transform.DOScale(Vector3.one * defaultScale, .1f);
    }
}
