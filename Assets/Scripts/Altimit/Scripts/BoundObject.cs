using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Collections;

[System.Serializable]
public class BoundObject<T> : IBound {

    public T Value
    {
        get
        {
            return value;
        }
        set
        {
            if ((this.value == null && value != null) || !this.value.Equals(value))
            {
                this.value = value;
                //BindChildren(Value);

                OnUpdate(Value);
            }
        }
    }

    T value;

    public Action<object> onUpdateGeneric { get; set; }
    public Action<T> onValueChanged;

    public BoundObject ()
    {
        if (typeof(T).Equals(typeof(String)))
        {
            Value = (T)(object)String.Empty;
        }
        else
        {
            Value = (T)Activator.CreateInstance(typeof(T));
        }
    }

    public object Get ()
    {
        return (object)Value;
    }

    public void Set(object value)
    {
        Value = (T)value;
    }

    public BoundObject (T value)
    {
        Value = value;
    }

    public virtual void OnUpdate(object childValue)
    {
        onUpdateGeneric?.Invoke(Value);
    }

    public void BindChildren(object value)
    {
        // Debug.Log(value);
        List<IBound> boundObjects = value.GetType().GetFields().Select(field =>
            field.GetValue(value)).Where(x => x != null && x.GetType().IsSubclassOf(typeof(IBound))).Select(y => (IBound)y).ToList();

        boundObjects.ForEach(x => x.onUpdateGeneric += OnUpdate);
    }
}

public class BoundInt : BoundObject<int> {
    public BoundInt() : base() { }
    public BoundInt(int value) : base(value) { }
}

public class BoundFloat : BoundObject<float> {
    public BoundFloat() : base() { }
    public BoundFloat (float value) : base(value) { }
}

public class BoundBool : BoundObject<bool> {
    public BoundBool() : base() { }
    public BoundBool(bool value) : base(value) { }
}

public class BoundString : BoundObject<string>
{
    public BoundString() : base() { }
    public BoundString(string value) : base(value) { }
}

[Serializable]
public class BoundObject : BoundObject<Object>
{
    public BoundObject() : base() { }

    public BoundObject(Object value) : base(value) { }
}
