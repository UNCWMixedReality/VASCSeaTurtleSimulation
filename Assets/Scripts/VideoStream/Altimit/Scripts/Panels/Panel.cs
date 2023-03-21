using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Altimit.UI
{
    public enum HistoryType
    {
        ClearHistory,
        AddHistory,
        IgnoreHistory
    }

    public class Panel : View
    {

        //Panels by default hide other panels
        public bool IsSelective = true;
 //       [ReadOnly]
        public Panel ParentPanel;
        public bool Snap = true;
        public bool SizeFit = false;
        public Button SubmitButton;
        
        //bool isCreated = false;
        public PanelManager PanelManager
        {
            get
            {
                if (panelManager == null)
                {
                    Transform parent = transform;
                    while (parent.GetComponent<PanelManager>()==null && parent.parent != null)
                    {
                        parent = parent.parent;
                    }
                    panelManager = parent.GetComponent<PanelManager>();
                }
                    

                return panelManager;
            }
            set
            {
                panelManager = value;
            }
        }
        PanelManager panelManager;

        public UnityEvent OnSubmit;

        public void Awake()
        {
           
        }

        private void Start()
        {
            //if (Element == null)
            //    Element = GetComponent<Binder>();
            //if (SubmitButton)
            //    SubmitButton.onClick.AddListener(Submit);
        }

        public virtual void Submit ()
        {

        }
        /*
        public void OnEnable()
        {
            if (!Application.isPlaying)
                return;

            OnShow();
        }
        */

        public virtual void Show(object value, HistoryType historyType = HistoryType.ClearHistory)
        {
            Show(historyType);
            gameObject.AddOrGet<Binder>().Set(value);
        }

        /*
        public virtual void Show(Binder element, HistoryType historyType = HistoryType.ClearHistory)
        {
            if (Binder != null && element != null)
                Binder.Set(element.Value);

            Show(historyType);
        }*/

        public virtual void UI_ShowPanel ()
        {
            Show();
        }

        public virtual void UI_ShowPanel(HistoryType historyType = HistoryType.ClearHistory)
        {
            Show(historyType);
        }

        public virtual void Show(HistoryType historyType = HistoryType.ClearHistory)
        {
            PanelManager.ShowPanel(this, historyType);
        }

        /*
        public void OnShow ()
        {
                SetMessage("");
                OnShowPanel();

                PanelManager.OnPanelShow(this, true);
                //if (onShowPanel != null)
                  //  onShowPanel(this, true);
        }
        */

        public virtual void OnShowPanel ()
        {
            //SetMessage("");
            onEnable(gameObject);
        }

        public bool IsShowing ()
        {
            Transform parent = transform;
            do
            {
                if (!parent.gameObject.activeSelf)
                    return false;

                parent = parent.parent;
            }
            while (parent != null);

            return true;
        }
    }
}