using System;
using UnityEngine;
using System.Collections;
using TMPro;
using System.Threading.Tasks;

namespace Altimit.UI
{
    [ExecuteInEditMode]
    public class TMPFix  : MonoBehaviour
    {
        TextMeshProUGUI text;
        /*
        public async void Start()
        {
            text = gameObject.AddOrGet<TextMeshProUGUI>();
            /*
            text.enableCulling = true;
            await Task.Delay(100);
            //text.fontMaterial = new Material(text.fontMaterial);
            text.fontSharedMaterial.EnableKeyword("UNDERLAY_ON");
            await Task.Delay(100);
            text.fontSharedMaterial.DisableKeyword("UNDERLAY_ON");
            
        }
        */
    }
}
