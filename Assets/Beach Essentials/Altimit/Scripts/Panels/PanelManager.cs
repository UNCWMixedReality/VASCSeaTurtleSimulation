using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using System.Linq;

namespace Altimit.UI
{
    [ExecuteInEditMode]
    public class PanelManager : MonoBehaviour
    {
        public List<Panel> PanelHistory = new List<Panel>();
        public UnityEngine.UI.Button BackButton;
        public bool AllowSnapping
        {
            get
            {
                return allowSnapping;
            }
            set
            {
                allowSnapping = value;
                if (!allowSnapping)
                    SizeFitter.enabled = false;
            }
        }

        bool allowSnapping = true;

        ContentSizeFitter SizeFitter
        {
            get
            {
                if (sizeFitter == null)
                {
                    sizeFitter = gameObject.AddOrGet<ContentSizeFitter>();
                    sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                return sizeFitter;
            }
        }
        ContentSizeFitter sizeFitter;
        Panel currentPanel;
        GameObject lastSelectedGO = null;

        // Use this for initialization
        void Awake()
        {

            if (!Application.isPlaying)
            {
#if UNITY_EDITOR
                Selection.selectionChanged += UpdateSelectedPanel;
#endif
                return;
            }

            ShowPanel(gameObject.AddOrGet<Panel>());
        }

        private void Start()
        {
            if (!Application.isPlaying) return;
                if (BackButton)
                    BackButton.onClick.AddListener(UI_ShowLastPanel);
        }

        public void OnEnable()
        {

        }

        void OnDisable ()
        {

#if UNITY_EDITOR
            Selection.selectionChanged -= UpdateSelectedPanel;
#endif
        }

        // Update is called once per frame
        void UpdateSelectedPanel()
        {
            GameObject selectedGO = null;

            #if UNITY_EDITOR
            selectedGO = Selection.activeGameObject;
            #endif

            if (selectedGO != null && selectedGO != lastSelectedGO)
            {
                Panel panel = GetPanel(selectedGO);
                //Debug.Log(panel.IsShowing());
                if (panel != null && (currentPanel == null || !(panel.GetComponentsInChildren<Panel>().Contains(currentPanel) || currentPanel.GetComponentsInChildren<Panel>().Contains(panel))))
                    ShowPanel(panel);
            }
            lastSelectedGO = selectedGO;
        }

        public Panel GetPanel (GameObject go)
        {
            Panel panel;
            Transform parent = go.transform;
            do
            {
                panel = parent.GetComponent<Panel>();
                parent = parent.parent;
            } while (panel == null && parent != null);

            return panel;
        }

        public Panel GetParentPanel(Panel go)
        {
            if (go.transform.parent == null)
                return null;

            return GetPanel(go.transform.parent.gameObject);
        }

        public void ShowPanel(Panel panel, HistoryType historyType = HistoryType.ClearHistory)
        {
            if (panel == null)
                return;

            //if (historyType.Equals(HistoryType.AddHistory) && currentPanel != null)
            //{
            //    if (PanelHistory.Count == 0 || PanelHistory[PanelHistory.Count-1] != currentPanel)
            //    PanelHistory.Add(currentPanel);
            //}

            //Debug.Log(gameObject.name + " is showing panel " + panel.gameObject.name);

            Panel lastPanel = panel;
            
            //Iterates through child panels. The first child panel found is set active by default, while all others are deactivated
            List<Panel> childPanels = GetChildPanels(lastPanel.gameObject);
            while (childPanels.Count > 0)
            {
                childPanels.ForEach(x => x.gameObject.SetActive(!x.IsSelective || childPanels.IndexOf(x) == 0));
                lastPanel = childPanels[0];
                childPanels = GetChildPanels(lastPanel.gameObject);
            }

            //Iterates through parent panels.
            Panel firstPanel = panel;
            Panel parentPanel = GetParentPanel(firstPanel);
            while (parentPanel != null)
            {
                parentPanel.gameObject.SetActive(true);
                GetChildPanels(parentPanel.gameObject).ForEach(x => x.gameObject.SetActive(!x.IsSelective || x == firstPanel));
                firstPanel = parentPanel;
                parentPanel = GetParentPanel(parentPanel);
            }
            //Debug.Log(lastPanel.gameObject.name + ", " + (contentSizeFitter == null).ToString() + ", " + lastPanel.SizeFit);

            SizeFitter.enabled = AllowSnapping && lastPanel.SizeFit;

            if (!SizeFitter.enabled && GetComponent<Canvas>().renderMode == RenderMode.WorldSpace)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(1500, 1000);
            }

            currentPanel = panel;
            if (Application.isPlaying)
            {
                if (historyType.Equals(HistoryType.ClearHistory))
                    ClearHistory();

                if (!historyType.Equals(HistoryType.IgnoreHistory))
                    PanelHistory.Add(panel);

                panel.OnShowPanel();
                OnShowPanel(panel);
            }
           
        }

        List<Panel> GetChildPanels (GameObject go)
        {
            List<Panel> subPanels = new List<Panel>();
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGO = go.transform.GetChild(i).gameObject;
                Panel subPanel = childGO.GetComponent<Panel>();
                if (subPanel != null)
                {
                    subPanels.Add(subPanel);
                } else
                {
                    subPanels.AddRange(GetChildPanels(childGO));
                }
            }
            return subPanels;
        }

        public void OnShowPanel (Panel panel)
        {
            Canvas.ForceUpdateCanvases();
            if (BackButton)
                BackButton.gameObject.SetActive(PanelHistory.Count > 1);
        }

        public void UI_ShowLastPanel ()
        {
            ShowLastPanel();
        }

        public void ClearHistory ()
        {
            PanelHistory.Clear();
        }

        public void ShowLastPanel ()
        {
            if (PanelHistory.Count <= 1)
                return;

           // if (PanelHistory[PanelHistory.Count-1].HistoryType.Equals(HistoryType.IgnoreHistory))
            PanelHistory.RemoveAt(PanelHistory.Count - 1);
            PanelHistory[PanelHistory.Count - 1].Show(HistoryType.IgnoreHistory);//.OnStart(()=>PanelHistory.RemoveAt(PanelHistory.Count-1));
        }
    }
}
