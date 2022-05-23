using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[ExecuteInEditMode]
public class ParentObserver : MonoBehaviour {

    public Action<GameObject> onUpdate;
    public Action<GameObject> onUpdateSingle;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void OnTransformParentChanged () {
		if (onUpdate != null)
            onUpdate(gameObject);
        if (onUpdateSingle != null)
            onUpdateSingle(gameObject);
            onUpdateSingle = null;
    }
}
