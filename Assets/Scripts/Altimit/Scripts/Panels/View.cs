using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Altimit
{

    public class View : MonoBehaviour
    {
        public Action<GameObject> onAwake = delegate { };
        public Action<GameObject> onEnable = delegate { };

        bool isAwake = false;
        bool hasBeenDisabled = false;

        public void OnEnable()
        {
            if (!hasBeenDisabled)
            {
                gameObject.SetActive(false);
                hasBeenDisabled = true;
                return;
            }

            if (!isAwake)
            {
                isAwake = true;
                onAwake(gameObject);
            }

            onEnable(gameObject);
        }

        public void OnDisable()
        {
        }
    }
}