using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Altimit.Reflection;
using System.Runtime.InteropServices;

namespace Altimit
{
    public class VarBinder : Binder
    {
        object modelValue = null;
        object oldModelValue = null;

        public override void Init(Type type)
        {
            base.Init(type);
            modelValue = Activator.CreateInstance(Type);
            viewValue = Activator.CreateInstance(Type);
        }

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

        object viewValue = null;
        object oldViewValue = null;

        public override bool GetViewChanged()
        {
            return (oldViewValue != viewValue);
        }

        public override void SetViewChanged()
        {
            oldViewValue = viewValue;
        }

        public override object GetView()
        {
            return viewValue;
        }

        public override void SetView(object value)
        {
            viewValue = value;
        }
    }
}