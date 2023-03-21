using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using Altimit.Reflection;

namespace Altimit.UI
{
    public class ListBinder : VarBinder
    {
        public Type ChildType
        {
            get
            {
                return Type.GetGenericArguments()[0];
            }
        }

        Func<object, GameObject> ChildGO;
        Func<object, bool> IsBindable = x => true;
        //Type Type;
        List<GameObject> Binders = new List<GameObject>();

        public void Init(Type type, Func<object, GameObject> childGO, Func<object, bool> isBindable = null)
        {
            Type = type;
            ChildGO = childGO;
            if (isBindable != null)
                IsBindable = isBindable;
        }

        public void Init(Type type, Func<object, GameObject> childGO, Action<Action<object>> callback)
        {
            Init(type, childGO);
            Init(callback);
        }


        public void Init(object value)
        {
            if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(BoundList<>))
            {
                //Debug.Log("GOT BOUND LIST");
                var boundList = value as IBoundList;
                boundList.ItemAdded += ItemAdded;
                boundList.ItemRemoved += ItemRemoved;
            }
            Set(value);

            //x=>Manager.Instance.Server.GetModels(someValue, y=>x(y))
            //new ListBinder().Init(x => x(null));
        }

        Action<Action<object>> callback;

        public void Init(Action<Action<object>> callback)
        {
            this.callback = callback;
        }

        private void OnEnable()
        {
            if (isLoading || callback == null)
                return;

            isLoading = true;
            callback(x => { isLoading = false; Set(x); });
        }

        public override void SetView(object value)
        {
            while (Binders.Count > 0)
                RemoveBinder(Binders[0]);

            if (value != null)
            {
                List<object> list = (value as IEnumerable<object>).Cast<object>().ToList();
                //Debug.Log(list.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    AddBinder(list[i]);
                }
            }
            base.SetView(value);
        }

        public override void OnDestroy()
        {
            var boundList = GetModel() as IBoundList;
            if (boundList != null)
            {
                boundList.ItemAdded -= ItemAdded;
                boundList.ItemRemoved -= ItemRemoved;
            }
        }

        public GameObject CreateBinder(object value)
        {
            if (!IsBindable(value))
                return null;

            GameObject childBinder = ChildGO(value);
            childBinder.SetParent(transform);
            childBinder.transform.localPosition = Vector3.zero;
            childBinder.transform.localEulerAngles = Vector3.zero;
            childBinder.transform.localScale = Vector3.one;

            return childBinder;
        }

        public override void Bind(Binder binder)
        {
        }

        public override void Unbind(Binder binder)
        {
        }

        private void ItemRemoved(object source, ListChangedEventArgs e)
        {
            RemoveBinder(e.index);
        }

        private void ItemAdded(object source, ListChangedEventArgs e)
        {
            AddBinder(e.index, e.item);
        }

        void RemoveBinder(GameObject binder)
        {
            RemoveBinder(Binders.IndexOf(binder));
        }

        void RemoveBinder(int index)
        {
            GameObject binder = Binders[index];
            Destroy(binder);
            Binders.RemoveAt(index);
        }

        void AddBinder(object data)
        {
            AddBinder(Binders.Count, data);
        }

        void AddBinder(int index, object value)
        {
            GameObject binder = CreateBinder(value);
            binder?.Get<ClassBinder>(x => x.Set(value));
            Binders.Insert(index, binder);
        }



        /*
        PBinding Binding
        {
            get
            {
                return Bindings[0];
            }
        }
        GameObject ChildPrefab
        {
            get
            {
                if (!Binding.Element.isAssigned)
                    return null;

                return (GameObject)Binding.Element.target;
            }
        }
        Type listType
        {
            get
            {
                var pListType = typeof(PList<>);
                var listType = pListType.MakeGenericType(Type);
                return listType;
            }
        }
        List<BinderBase> ChildElements = new List<BinderBase>();

        public override void Awake()
        {
            if (ChildPrefab != null)
                ChildPrefab.gameObject.SetActive(false);
            base.Awake();
        }

        public override object CreateData ()
        {

            //  Debug.Log(constructedListType.ToString());
            return Activator.CreateInstance(typeof(PList));
        }

        public override void SetAutoBinding()
        {
            if (Type == null)
                return;

            Bindings = new List<PBinding>() { new PBinding(Type.Name, Type) };

            if (AutoBind && Bindings != null)
            {
                BinderBase childElement = gameObject.GetComponentsInChildren<BinderBase>().Where(
                    x => x.Type == Type && x.GetType() != typeof(BinderList)).SingleOrDefault();

                if (childElement != null)
                    Binding.Element = new UnityVariable(childElement.GetType().Name, "Value", childElement.gameObject);
            }
        }

        public override void OnValueChanged(PList value)
        {
            //Debug.Log("HELLO");
            ChildElements.ForEach(x => Destroy(x.gameObject));
            ChildElements.Clear();

            value.ToList().ForEach(x => {
                AddElement(value.IndexOf(x),x);
                });
            value.ItemAdded += ItemAdded;
            value.ItemRemoved += ItemRemoved;
            //GetField(binding.Name);
            // Bindings[0]
        }

        private void ItemRemoved(object source, PList<object>.ListChangedEventArgs e)
        {
            RemoveElement(e.index);
        }

        private void ItemAdded(object source, PList<object>.ListChangedEventArgs e)
        {
            AddElement(e.index, e.item);
        }

        void RemoveElement (int index)
        {
            BinderBase element = ChildElements[index];
            Destroy(element.gameObject);
            ChildElements.RemoveAt(index);
        }

        void AddElement (object data)
        {
            AddElement(ChildElements.Count, data);
        }

        void AddElement (int index, object data)
        {
            //Debug.Log(data == null);
            BinderBase element = CreateDataElement();
            element.SetValue(data);
            ChildElements.Insert(index, element);
            //Debug.Log(((Window)data).Icon.name);
            //Debug.Log(JsonUtility.ToJson(data).ToString());
        }

        public BinderBase CreateDataElement()
        {
            GameObject childGO = Instantiate(ChildPrefab, transform);
            childGO.SetActive(true);
            return childGO.GetComponent<BinderBase>();
        }
        */
    }
}