/*

    Daniel Vaughn
    UNCW VR_Research_Team

    remote_collector.cs

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

[DataContract]
public class RemoteCollection
{

    [DataMember]
    public GameObject obj;

    [DataMember]
    public RemoteSettings rs;

    public override string ToString()
    {
        return obj.name;
    }

}

[ExecuteInEditMode]
public class remote_collector : MonoBehaviour
{

    public static Dictionary<int, RemoteCollection> remotes = new Dictionary<int, RemoteCollection>();

    public void add(RemoteCollection rc)
    {
        int obj_id = rc.obj.GetInstanceID();
        Debug.Log("Remote_Collector.CS: RC Obj ID: " + obj_id);
        try
        {
            remotes[obj_id] = rc;
        }
        catch (Exception e)
        {
            Debug.Log("Remote_Collector.CS: " + e.ToString());
        }
        
    }

    public static string getObjects()
    {
        StringBuilder sb = new StringBuilder();
        foreach (KeyValuePair<int, RemoteCollection> rc in remotes)
        {
            sb.Append("<br><br>Object Name: " + rc.Value.ToString() + "<br>Object ID: " + rc.Key);
        }
        return sb.ToString();
    }

    public static string getJSON()
    {

        StringBuilder JSON = new StringBuilder();
        JSON.Append("JSON|");
        JSON.Append("{\"Objects\": [");

        foreach (KeyValuePair<int, RemoteCollection> rc in remotes)
        {
            JSON.Append("{\"ID\":\"" + rc.Key + 
                "\" ,\"Name\": \"" + rc.Value.ToString() + 
                "\" ,\"Settings\": [");
            foreach (RemoteComponents rcs in rc.Value.rs.components)
            {
                JSON.Append("{\"Component\": \"" + rcs.ToString() +
                    "\" ,\"Methods\": [");
                foreach (RemoteConfigurable rcfg in rcs.settings)
                {
                    JSON.Append("{\"Name\": \"" + rcfg.name +
                        "\" ,\"ReturnType\": \"" + rcfg.returnType + 
                        "\", \"Paramaters\": [");

                    if (rcfg.parameters.Length > 0)
                    {
                        foreach (string[] ps in rcfg.parameters)
                        {
                            JSON.Append("{\"Name\": \"" + ps[0] + "\" ,\"Type\": \"" + ps[1] + "\"},");
                        }
                    } else
                    {
                        JSON.Append(",");
                    }
                    JSON.Remove(JSON.Length - 1, 1);
                    JSON.Append("]},");
                }
                JSON.Remove(JSON.Length - 1, 1);
                JSON.Append("]},");
            }
            JSON.Remove(JSON.Length - 1, 1);
            JSON.Append("]},");
        }
        JSON.Remove(JSON.Length - 1, 1);
        JSON.Append("]}");
        return JSON.ToString();
    }

    public static new string ToString()
    {

        StringBuilder sb = new StringBuilder();
        foreach (KeyValuePair<int, RemoteCollection> rc in remotes)
        {
            sb.Append("<br>" + rc.ToString());
            foreach (RemoteComponents rcs in rc.Value.rs.components)
            {
                sb.Append("<br>&nbsp;COMP:&nbsp;" + rcs.ToString());        
                foreach (RemoteConfigurable rcfg in rcs.settings)
                {
                    sb.Append("<br>&nbsp;&nbsp;METHOD:&nbsp;&nbsp;" + rcfg.ToString());
                }
            }
        }

        return sb.ToString();

    }

}
