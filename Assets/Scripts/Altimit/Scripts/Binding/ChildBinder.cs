using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Altimit.Reflection;
using System.Runtime.InteropServices;

namespace Altimit
{
    /* BINDS A PROPERTY TO SOME MANAGING CLASS */
    /* (the 'global' veriable that is stored in some other class) */

    public class ChildBinder : Binder
    {

        public virtual void Init(IBound bound, UnityVariable viewVariable)
        {
            bound.onUpdateGeneric += OnUpdate;
            
            Type = viewVariable.GetVariableType();
            ViewVariable = viewVariable;
        }

        void OnUpdate (object value)
        {
            SetView(value);
        }

        public override void Init (Type parentType)
        {
            ParentType = parentType;
        }

        public virtual void Init(Type parentType, string name, UnityVariable viewVariable)
        {
            ParentType = parentType;
            Name = name;
            Type = viewVariable.GetVariableType();
            ViewVariable = viewVariable;
        }

        /* DEFINES MODEL USAGE */

        object modelValue = null;
        object oldModelValue;

        public override object GetModel()
        {
            return modelValue;
        }

        public override void SetModel(object value)
        {
            modelValue = value;
        }

        public override bool GetModelChanged()
        {
            return (modelValue != oldModelValue);
        }

        public override void SetModelChanged()
        {
            oldModelValue = modelValue;
        }

        /* DEFINES VIEW USAGE */

        public UnityVariable ViewVariable = new UnityVariable();

        public override object GetView()
        {
            return ViewVariable?.Get();
        }

        public override void SetView(object value)
        {
            if (ViewVariable.isAssigned)
                ViewVariable.Set(value);
        }
       
        public override bool GetViewChanged()
        {
            return ViewVariable.isAssigned && ViewVariable.GetChanged();
        }
        
        public override void SetViewChanged()
        {
            if (ViewVariable.isAssigned)
                ViewVariable.SetChanged();
        }
    }
}