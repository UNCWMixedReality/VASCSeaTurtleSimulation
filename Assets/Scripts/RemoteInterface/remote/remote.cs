/*

    Daniel Vaughn
    UNCW VR_Research_Team

    remote.cs

*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

[Serializable]
public class RemoteConfigurable
{
    public string name;
    public bool allowModification = false;
    public Type returnType = null;
    public string[][] parameters;

    public override string ToString()
    {
        StringBuilder outBuilder = new StringBuilder();
        outBuilder.Append(returnType.ToString());
        outBuilder.Append(" " + name + "(");

        if (parameters.Length > 0)
        { 
            foreach (string[] s in parameters)
            {
                outBuilder.Append(s[1] + ' ' + s[0]);
                outBuilder.Append(", ");
            }
            outBuilder.Remove(outBuilder.Length - 2, 2);
        }

        outBuilder.Append(")");
        return outBuilder.ToString();
    }

}

[Serializable]
public class RemoteComponents
{
    public Component component;
    public RemoteConfigurable[] settings;
    public bool allowModification = true;

    public override string ToString()
    {
        return component.GetType().ToString();
    }

}

[Serializable]
public class RemoteSettings
{
    public RemoteComponents[] components;
    public string[][] properties;
}

[Serializable, ExecuteInEditMode]
public class remote : MonoBehaviour
{

    private GameObject self;
    public RemoteSettings settings = new RemoteSettings();
    public List<Component> components = new List<Component>();

    void Start()
    {
        getAttributes();
    }

    public void getAttributes()
    {

        self = gameObject;

        components.Clear();
        components.AddRange(GetComponents(typeof(Component)));

        List<RemoteComponents> compBuilder = new List<RemoteComponents>();
        List<string[]> propBuilder = new List<string[]>();

        foreach (Component comp in components)
        {
            var compType = comp.GetType();
            MethodInfo[] methods = compType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            List<RemoteConfigurable> configBuilder = new List<RemoteConfigurable>();
            RemoteComponents remoteComps = new RemoteComponents();
            remoteComps.component = comp;
            
            foreach (MethodInfo method in methods)
            {

                RemoteConfigurable config = new RemoteConfigurable();

                config.name = method.Name;
                config.returnType = method.ReturnType;

                ParameterInfo[] paramsInfo = method.GetParameters();
                List<string[]> paramBuilder = new List<string[]>();

                foreach (ParameterInfo pInfo in paramsInfo)
                {
                    paramBuilder.Add(new string[] { pInfo.Name, pInfo.ParameterType.ToString() });
                }

                config.parameters = paramBuilder.ToArray();

                configBuilder.Add(config);

            }

            PropertyInfo[] properties = compType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (PropertyInfo pInfo in properties)
            {
                propBuilder.Add(new string[] { pInfo.Name, pInfo.PropertyType.ToString() });
            }

            remoteComps.settings = configBuilder.ToArray();
            compBuilder.Add(remoteComps);
        }

        settings.components = compBuilder.ToArray();
        settings.properties = propBuilder.ToArray();

        RemoteCollection rc = new RemoteCollection();
        rc.obj = self;
        rc.rs = settings;

        Component collector = GameObject.FindGameObjectWithTag("_GAME").GetComponent("remote_collector");
        collector.SendMessage("add", rc);

    }

}
