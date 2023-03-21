using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using Object = System.Object;
using UnityObject = UnityEngine.Object;
using System.Reflection;
using Altimit.Reflection;

namespace Altimit
{
    public class ClassBinder : VarBinder
    {
        public List<Binder> SubBinders = new List<Binder>();

        public virtual void Awake()
        {
        }

        public override void Update()
        {
        }

        public override object GetView ()
        {
            object value = Activator.CreateInstance(Type);
            if (value != null)
            {
                //Debug.Log("2 " + (value == null).ToString());

                foreach (Binder binder in SubBinders)
                {
                    FieldInfo field = Type.GetField(binder.Name);
                    if (field != null)
                    {
                        object childValue = binder.Get();
                        //Debug.Log("3 " + (childValue == null).ToString());
                        field.SetValue(value, childValue);
                    }
                }
            }
            return value;
        }
        
        public override void SetView(object value)
        {
            if (value != null)
            {
                foreach (Binder binder in SubBinders)
                {
                    object childValue = value;
                    if (binder.Name != null)
                    {
                        FieldInfo field = Type.GetField(binder.Name);
                        if (field != null)
                            childValue = field.GetValue(value);

                        PropertyInfo property = Type.GetProperty(binder.Name);
                        if (property != null)
                            childValue = property.GetValue(value, null);
                    }
                    binder.Set(childValue);
                }
            }
            //base.SetView(value);
        }

        public override void Bind(Binder subBinder)
        {
            if (SubBinders.Contains(subBinder)) return;

            SubBinders.Add(subBinder);
        } 

        public override void Unbind(Binder subBinder)
        {
            SubBinders.Remove(subBinder);
        }

        public override void Set(object value)
        {
            base.Set(value);
        }

        public override object Get()
        {
            return base.Get();
        }
    }
}

/*
public override void SetBindings ()
{
    if (Type == null)
        return;

    Bindings = Type.GetFields().Where(field => field.IsPublic).Select(field =>
        new PBinding(field.Name, field.FieldType)).ToList();

    base.SetBindings();
}
*/
/*
public override void SetAutoBinding()
{

   if (Bindings != null)
   {
        GameObject[] gameObjects = gameObject.GetComponentsInChildren<Transform>().Select(x=>x.gameObject).ToArray();

        foreach (GameObject go in gameObjects)
        {
            PBinding similarBinding = Bindings.Where(x=> x.PublicName.ToLower() == go.name.ToLower()).SingleOrDefault();
            if (similarBinding != null)
            {
                UnityVariable element = similarBinding.Element;
                Type type = similarBinding.Type.Type;
                Component[] components = go.GetComponents(typeof(Component));
                foreach (Component component in components)
                {
                    FieldInfo[] fields = component.GetType().GetFields();
                    foreach (FieldInfo fieldInfo in fields)
                    {

                    }
                }
                if (type == typeof(string))
                {
                    TMP_Text text = go.GetComponent<TMP_Text>();
                    if (text != null)
                        element = new UnityVariable(text.GetType().Name,"text",go);

                    TMP_InputField inputField = go.GetComponent<TMP_InputField>();
                    if (inputField != null)
                        element = new UnityVariable(inputField.GetType().Name, "text", go);
                }
                if (type.IsEnum)
                {
                    TMP_Dropdown component = go.GetComponent<TMP_Dropdown>();
                    if (component != null)
                        element = new UnityVariable(component.GetType().Name, "value", go);
                }


                similarBinding.Element = element;
            }
        }
   }
}
*/
/*
[Serializable]
public class PBinding
{
    public string PublicName;
    public string Name;
    public string PublicTypeName;
    public SerializedType Type;
    public UnityVariable Element;

    public PBinding (string name, Type type)
    {
        Name = name;
        Type = new SerializedType(type);
        PublicName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SplitCamelCase(Name));
    }

    public string SplitCamelCase(string name)
    {
        string[] words = Regex.Matches(name, "(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
            .OfType<Match>()
            .Select(m => m.Value)
            .ToArray();
        string result = string.Join(" ", words);
        return result;
    }
}
*/
/*
public class PElement<T> : PElement where T : IPData
{
    public virtual void SetData (T data)
    {
        base.SetData(data);
    }
}
*/
/*
public class BinderBase : MonoBehaviour
{
    public bool IsActive
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
        }
    }
    [HideInInspector]
    public bool AutoBind = true;
    //[HideInInspector]
    //[ClassImplements(typeof(IBindable))]
    //public ClassTypeReference TypeRef;
    [HideInInspector]
    public List<PBinding> Bindings;
    public virtual Type Type { get; set; }

    public virtual void SetBindings()
    {
        if (AutoBind)
            SetAutoBinding();
    }

    public virtual void SetAutoBinding()
    {

    }

    public virtual void Set(object value)
    {
    }

    public virtual object GetValue()
    {
        return null;
    }

    public virtual void SetBinding(string name, UnityVariable element)
    {
        PBinding binding = Bindings.Where(x => x.Name == name).SingleOrDefault();
        binding.Element = element;
    }
}
*/
