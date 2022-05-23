using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;
using System.Linq; 

public class Waypoint : MonoBehaviour
{
    /*
    public GameObject truck;
    public int sceneIndex;

    void OnTriggerEnter(Collider col)
    {
        Destroy(truck);
        SceneManager.LoadScene(sceneIndex);
    }
    */
    bool wasTriggered = false;
    public Action<Waypoint> OnTriggered;
    Tweener tweener;

    public void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public void Update()
    {
        if (wasTriggered)
            return;

        var cols = Physics.OverlapSphere(transform.position, 2);
        if (cols != null && cols.SingleOrDefault(x=>x.gameObject.tag=="Player"))
        {
            Debug.Log("asdf");
            OnTriggered?.Invoke(this);
            wasTriggered = true;
        }
    }

    public Tweener SetVisibility(bool value)
    {
        float targetScale = value ? 1 : 0;

        tweener?.Kill();
        tweener = transform.DOScale(targetScale, .5f).SetEase(Ease.InQuad);
        return tweener;
    }
}
