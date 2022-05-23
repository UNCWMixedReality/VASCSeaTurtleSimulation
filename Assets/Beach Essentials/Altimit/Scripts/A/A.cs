using UnityEngine;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using Meridian;

namespace Altimit.UI
{
    public static partial class A
    {
        /*
        //Shortcut for adding action when panel shows
        public static GameObject OnShow(this GameObject go, object value)
        {
            go.AddOrGet<Panel>().onShowPanel += new Action<GameObject>(x => x.Set(value));
            return go;
        }
        */

        public class Class
        {
            public Action<GameObject> Render;

            public Class() { }
        }

        public static GameObject Show(this GameObject go)
        {
            go.Get<Panel>().Show();
            return go;
        }
        public static GameObject Show(this GameObject go, object value, HistoryType historyType = HistoryType.ClearHistory)
        {
            go.Get<Panel>().Show(value, historyType);
            return go;
        }
        public static GameObject OnAwake(this GameObject go, Action<GameObject> onAwake)
        {
            return go.Hold<View>(x=> { x.onAwake += onAwake; });
        }

        //Shortcut for adding action when panel shows
        public static GameObject OnEnable(this GameObject go, Action<GameObject> func)
        {
            go.AddOrGet<View>().onEnable += func;
            return go;
        }

        //Shortcut for accesing binder set method
        public static GameObject Set(this GameObject go, object value)
        {
            go.AddOrGet<ClassBinder>().Set(value);
            return go;
        }

        //Dummy function
        public static GameObject Hold(this GameObject go)
        {
            return go;
        }
        public static GameObject Hold(this GameObject go, Action<GameObject> func)
        {
            func(go);
            return go;
        }
        //Adds or gets a component
        public static GameObject Hold<T>(this GameObject go) where T : Component
        {
            T t;
            return go.Hold<T>(out t);
        }

        //Adds or gets a component
        public static GameObject Hold<T>(this GameObject go, out T t) where T : Component
        {
            if (go == null)
                Debug.Log(go == null);
            t = go.AddOrGet<T>();
            return go;
            //return (t=go.AddOrGet<T>()).gameObject;
        }

        //Adds or gets a component and sets values for it
        //Example: New<HorizontalLayoutGroup>(x => { x.padding = new RectOffset(0,0,0,0); });
        public static GameObject Hold<T>(this GameObject go, Action<T> func) where T : Component
        {
            T t;
            return go.Hold<T>(func, out t);
        }

        //Adds or gets a component and sets values for it
        //Example: New<HorizontalLayoutGroup>(x => { x.padding = new RectOffset(0,0,0,0); });
        public static GameObject Hold<T>(this GameObject go, Action<T> func, out T t) where T : Component
        {
            return go.Hold(false, func, out t);
        }

        public static GameObject OnHeld(this GameObject go, Action<GameObject> func)
        {
            go.Hold<ParentObserver>(x =>
            {
                x.onUpdateSingle += func;
            });
            return go;
        }

        public static GameObject Hold<T>(this GameObject go, bool includeChildren, Action<T> func) where T : Component
        {
            T t;
            return go.Hold<T>(includeChildren, func, out t);
        }

        public static GameObject Hold<T>(this GameObject go, bool includeChildren, Action<T> func, out T t) where T : Component
        {
            t = go.AddOrGet<T>(includeChildren);
            //Holder holder = go.AddOrGet<Holder>();
            //holder.OnHold += ()=> { func(t); };
            func(t);
            return go;
        }

        //Sets a GameObject's children
        public static GameObject Hold(this GameObject go, params GameObject[] children)
        {
            children.ToList().ForEach(x => { x.SetParent(go.transform, true, true, true); });
            return go;
        }

        //Sets a GameObject's children
        public static GameObject HoldFirst(this GameObject go, params GameObject[] children)
        {
            children.ToList().ForEach(x => { x.SetParent(go.transform, true, true, true); x.transform.SetAsFirstSibling(); });
            return go;
        }

        //Sets a GameObject's children
        public static GameObject Switch(this GameObject go, GameObject target)
        {
            go.Release();
            go.Hold(target);
            return go;
        }

        //Sets a GameObject's children
        public static GameObject Release(this GameObject go)
        {
            go.transform.DetachChildren();
            return go;
        }

        public static GameObject Release<T>(this GameObject go) where T : Component
        {
            T comp = go.GetComponent<T>();
            if (comp != null)
                GameObject.DestroyImmediate(go.GetComponent<T>());
            return go;
        }

        //Returns an empty gameObject
        public static GameObject New()
        {
            GameObject go = new GameObject();
            go.name = UnityEngine.Random.Range(0, 1000).ToString();
            return go;
        }

        //Adds or gets a component
        public static T AddOrGet<T>(this GameObject go, bool includeChildren = false) where T : Component
        {
            T comp = go.Get<T>(includeChildren);

            if (comp == null)
            {
//                Debug.Log(typeof(T));
                return go.AddComponent<T>();
            }
            return comp;
        }

        //Returns if a gameObject has a component or not
        public static bool Has<T>(this GameObject go) where T : Component
        {
            return go.GetComponent<T>() != null;
        }


        //Gets a component
        public static T GetInParent<T>(this GameObject go) where T : Component
        {
            return (go == null ? null : go.GetComponentInParent<T>());
        }

        //Gets a component
        public static T[] GetInParents<T>(this GameObject go) where T : Component
        {
            return (go == null ? new T[0] : go.GetComponentsInParent<T>());
        }

        //Gets a component
        public static T Get<T>(this GameObject go, bool includeChildren = false) where T : Component
        {
            if (go == null)
                return null;
            if (includeChildren)
                return go.GetComponentInChildren<T>();

            return go.GetComponent<T>();
        }

        //Gets a component
        public static GameObject Get(this GameObject go)
        {
            if (go == null)
                go = New();
            return go;
        }

        //Gets a component. If present, performs an action on it.
        public static GameObject Get<T>(this GameObject go, Action<T> func, bool includeChildren = false) where T : Component
        {
            if (go.Has<T>())
            {
                func(go.Get<T>());
            } else
            {
                var comp = go.GetComponentInChildren<T>();
                if (comp != null)
                {
                    func(comp);
                }
            }
            return go;
        }

        public static GameObject SetParent(this GameObject go, Transform parent, bool setPosition = false, bool setRotation = false, bool setScale = false)
        {
            go.transform.SetParent(parent);
            if (setPosition)
                go.transform.localPosition = Vector3.zero;
            if (setRotation)
                go.transform.localEulerAngles = Vector3.zero;
            if (setScale)
                go.transform.localScale = Vector3.one;
            return go;
        }

        public static RectTransform GetRectTransform(this GameObject go)
        {
            return go.GetComponent<RectTransform>();
        }

        public static GameObject Call(this GameObject go, Action<GameObject> action)
        {
            if (action != null)
                action(go);
            return go;
        }
    }
}