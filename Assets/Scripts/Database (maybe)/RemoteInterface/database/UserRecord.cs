using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

[Serializable]
public class UserRecord
{

    public string UserName;
    public string Pass = "";
    public string FirstName = "";
    public string LastName = "";
    public string MiddleInitial = "";
    public string DOB = "";
    public string ReadLevel = "";
    public int Age = 1;
    public int Permission = 0;
    public int Module_1 = 0;
    public int Module_2 = 0;
    public int Module_3 = 0;
    public int VR = 1;

    public UserRecord(string userName)
    {
        this.UserName = userName;
    }

    public string toJSON()
    {
        StringBuilder sb = new StringBuilder("");
        sb.Append(JSONHelper.getKeyValuePair("UserName",UserName) + ",");
        sb.Append(JSONHelper.getKeyValuePair("FirstName", FirstName) + ",");
        sb.Append(JSONHelper.getKeyValuePair("LastName", LastName) + ",");
        sb.Append(JSONHelper.getKeyValuePair("MiddleInitial", MiddleInitial) + ",");
        sb.Append(JSONHelper.getKeyValuePair("DOB", DOB) + ",");
        sb.Append(JSONHelper.getKeyValuePair("ReadLevel", ReadLevel) + ",");
        sb.Append(JSONHelper.getKeyValuePair("Permission", Permission.ToString()) + ",");
        sb.Append(JSONHelper.getKeyValuePair("Module_1", Module_1.ToString()) + ",");
        sb.Append(JSONHelper.getKeyValuePair("Module_2", Module_2.ToString()) + ",");
        sb.Append(JSONHelper.getKeyValuePair("Module_2", Module_3.ToString()) + ",");
        sb.Append(JSONHelper.getKeyValuePair("VR", VR.ToString()));
        return sb.ToString();
    }

    public string toString()
    {

        string output = string.Format("username:{0}, " +
            "firstname:{1}, lastname:{2}, module_1:{3}, " +
            "module_2:{4}, module_3:{5}, readlevel:{6}, permission:{7}, " +
            "vr:{8:D}",
            UserName,
            FirstName,  
            LastName,
            Module_1.ToString(),
            Module_2.ToString(),
            Module_3.ToString(),
            ReadLevel,
            Permission,
            VR
        );

        return output;

    }

}
