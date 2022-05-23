using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityObserver : MonoBehaviour
{
    public bool IsVisible = false;

    private void OnBecameVisible()
    {
        IsVisible = true;
    }
}
