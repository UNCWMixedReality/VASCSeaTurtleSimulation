using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public class SerializedType
{
    [SerializeField]
    private string m_Name;

    public string Name
    {
        get { return m_Name; }
    }

    [SerializeField]
    private string m_AssemblyQualifiedName;

    public string AssemblyQualifiedName
    {
        get { return m_AssemblyQualifiedName; }
    }

    [SerializeField]
    private string m_AssemblyName;

    public string AssemblyName
    {
        get { return m_AssemblyName; }
    }

    private System.Type type;
    public System.Type Type
    {
        get
        {
            if (type == null)
            {
                GetSystemType();
            }
            return type;
        }
    }

    private void GetSystemType()
    {
        type = System.Type.GetType(m_AssemblyQualifiedName);
    }

    public SerializedType(System.Type _SystemType)
    {
        type = _SystemType;
        m_Name = _SystemType.Name;
        m_AssemblyQualifiedName = _SystemType.AssemblyQualifiedName;
        m_AssemblyName = _SystemType.Assembly.FullName;
    }

    public override bool Equals(System.Object obj)
    {
        SerializedType temp = obj as SerializedType;
        if ((object)temp == null)
        {
            return false;
        }
        return this.Equals(temp);
    }

    public bool Equals(SerializedType _Object)
    {
        //return m_AssemblyQualifiedName.Equals(_Object.m_AssemblyQualifiedName);
        return _Object.Type.Equals(Type);
    }

    public static bool operator ==(SerializedType a, SerializedType b)
    {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(SerializedType a, SerializedType b)
    {
        return !(a == b);
    }
}