using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meridian {
    /*
    public class DataPanel<T> : Panel where T : new()
    {
        
        public Panel ViewPanel;
        public Panel EditPanel;

        public DataPanel<T> EditDataPanel
        {
            get
            {
                return (DataPanel<T>)EditPanel;
            }
        }
        public DataPanel<T> ViewDataPanel
        {
            get
            {
                return (DataPanel<T>)ViewPanel;
            }
        }
        

        //public Button EditButton;
        public T Element;

        public virtual void Start ()
        {
            //if (EditButton != null)
            //    EditButton.onClick.AddListener(Edit);
        }

        public override void Show(HistoryType historyType = HistoryType.ClearHistory)
        {
            if (historyType.Equals(HistoryType.IgnoreHistory))
            {
                base.Show(historyType);
                return;
            }

            ShowPanel(new T(), historyType);
        }

        public void ShowPanel(T element, HistoryType historyType = HistoryType.ClearHistory)
        {
            D data = element.Data;
            ShowPanel(data, historyType);
        }
        
        public virtual void ShowPanel(D data, HistoryType historyType = HistoryType.ClearHistory)
        {
            if (Application.isPlaying)
            {
                Element.SetData(ref data);

                if (EditButton != null)
                    EditButton.gameObject.SetActive(IsMine());
            }

            base.Show(historyType);
        }

        public virtual void Edit ()
        {
            DataPanel<T,D> panel = EditPanel as DataPanel<T, D>;
            if (panel != null)
                panel.ShowPanel(Element.Data, HistoryType.AddHistory);
        }
        
        public virtual bool IsMine ()
        {
            return true;
        }
    }
    */
}