using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Altimit.UI
{
    public class PanelButton : MonoBehaviour
    {
        public Panel Panel;
        public HistoryType HistoryType = HistoryType.ClearHistory;
        public ClassBinder Element;
        UnityEngine.UI.Button button;

        // Use this for initialization
        void Start()
        {
            button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(OnClick);
        }

        void OnClick ()
        {
            if (Panel != null)
                Panel.Show(Element, HistoryType);
        }
    }
}
