using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Altimit.Reflection;
using System.Runtime.InteropServices;

namespace Altimit
{
    /* BINDS A VALUE DERIVED FROM A CLASS PROPERTY */
    /* (the bound property is stored in some other class, as described by 'modelVariable') */

    public class PropertyBinder : Binder
    {
        public virtual void Init(UnityVariable modelVariable, UnityVariable viewVariable)
        {
            ModelVariable = modelVariable;
            ViewVariable = viewVariable;

            SetView(GetModel());
            SetChanged();
        }

        /* DEFINES MODEL USAGE */

        public UnityVariable ModelVariable = new UnityVariable();

        public override object GetModel()
        {
            return ModelVariable.Get();
        }

        public override void SetModel(object value)
        {
            ModelVariable.Set(value);
        }

        public override bool GetModelChanged()
        {
            return ModelVariable.GetChanged();
        }

        public override void SetModelChanged()
        {
            ModelVariable.SetChanged();
        }

        /* DEFINES VIEW USAGE */

        public UnityVariable ViewVariable = new UnityVariable();

        public override object GetView()
        {
            return ViewVariable?.Get();
        }

        public override void SetView(object value)
        {
            if (value != null)
                ViewVariable?.Set(value);
        }

        public override bool GetViewChanged()
        {
            return (ViewVariable != null && ViewVariable.GetChanged());
        }

        public override void SetViewChanged()
        {
            ViewVariable?.SetChanged();
        }
    }
}