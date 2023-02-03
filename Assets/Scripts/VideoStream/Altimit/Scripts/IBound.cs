using System.Collections;
using System.Collections.Generic;
using System;

public delegate void UpdateHandler(object source);

public interface IBound
{
    Action<object> onUpdateGeneric { get; set; }
    object Get();
    void Set(object value);
}