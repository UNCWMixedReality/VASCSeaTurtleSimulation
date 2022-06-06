using UnityEngine;
using System;

namespace Altimit
{
    public interface IBinder
    {
        Type Type { get; }
        void Set(object value);
        object Get();
        void SetView(object value);
        object GetView();
    }
}
