using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class JSONHelper
{

    public static string getKeyValuePair(string key, string value)
    {
        string output = string.Format(" \"{0}\" : \"{1}\" ", key, value);

        return output;
    }

    public static string getArray(string[] value)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for(int i = 0; i < (value.Length - 1); i++)
        {
            sb.Append("\"" + value[i] + "\",");
        }
        sb.Append("\"" + value[value.Length - 1] + "\"]");
        return sb.ToString();
    }

    public static string getArray(int[] value)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < (value.Length - 1); i++)
        {
            sb.Append("\"" + value[i].ToString() + "\",");
        }
        sb.Append("\"" + value[value.Length - 1].ToString() + "\"]");
        return sb.ToString();
    }

}
