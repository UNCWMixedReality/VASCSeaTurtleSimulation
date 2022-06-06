using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Altimit.Reflection;
using System.Runtime.InteropServices;

namespace Altimit
{
    public abstract class Binder : MonoBehaviour
    {
        public Type ParentType;
        public Type Type;

        public string Name;
        [System.NonSerialized]
        public Binder ParentBinder;

        public Action<GameObject, object> OnGet = (x,y) =>{};
        public Action<GameObject, object> OnSet = (x, y) => {};
        protected bool isLoading = false;

        public virtual void BindGet(Action<GameObject, object> action)
        {
            OnGet += action;
        }

        public virtual void BindSet(Action<GameObject, object> action)
        {
            OnSet += action;
        }

        public virtual void Bind(Action<GameObject, object> getAction, Action<GameObject, object> setAction)
        {
            BindGet(getAction);
            BindSet(setAction);
        }

        public virtual void Bind(Binder binder)
        {
        }

        public virtual void Unbind(Binder binder)
        {
        }

        public virtual void Init(Type type)
        {
            Type = type;
        }

        private void OnTransformParentChanged()
        {
            Binder parentBinder = GetComponentsInParent<Binder>(true).Where(x => (x.Type == ParentType)
                && x.gameObject != gameObject).FirstOrDefault();

            if (ParentBinder != parentBinder)
            {
                if (parentBinder != null)
                    parentBinder.Unbind(this);

                ParentBinder = parentBinder;

                if (ParentBinder != null)
                    ParentBinder.Bind(this);
            }
        }

        public virtual void Update ()
        {
            if (GetViewChanged())
            {
                SetModel(GetView());
                SetChanged();
            }
            if (GetModelChanged())
            {
                SetView(GetModel());
                SetChanged();
            }
        }

        public virtual void OnDestroy()
        {
        }

        //Sets both the view and model to a specific value. Also calls any actions defined in SetActions
        public virtual void Set (object value)
        {
            //Debug.Log(gameObject.name + ", " + SetActions.Count + ", " + JsonUtility.ToJson(value));
            OnSet(gameObject, value);

            SetModel(value);
            SetView(value);
            SetChanged();
        }

        //Gets the value
        public virtual object Get()
        {
            object value = GetModel();
            OnGet(gameObject, value);
            return value;
        }

        public abstract object GetModel();

        public abstract object GetView();

        public abstract void SetModel(object value);

        public abstract void SetView(object value);

        public abstract bool GetModelChanged();

        public abstract bool GetViewChanged();

        public virtual void SetChanged()
        {
            SetModelChanged();
            SetViewChanged();
        }

        public abstract void SetModelChanged();

        public abstract void SetViewChanged();
    }
}

/*
public void Init(Type type)
{
    Type = type;
    OnTransformParentChanged();
}

public void Init(Type parentType, string name, UnityVariable viewVariable)
{
    ParentType = parentType;
    Name = name;
    ViewVariable = viewVariable;

    OnTransformParentChanged();
}

public virtual void Init(object value, UnityVariable variable)
{
    ViewVariable = variable;
    Set(value);

    OnTransformParentChanged();
}*/
