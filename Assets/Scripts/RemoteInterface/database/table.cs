using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

[Serializable]
public class table
{

    // table name
    public string name;

    // records in table accessable by Username
    private Dictionary<string, UserRecord> records;

    public table(string name)
    {
        records = new Dictionary<string, UserRecord>();
        this.name = name;
    }

    public string toJSON()
    {
        List<string> users = new List<string>();
        foreach(KeyValuePair<string, UserRecord> kvp in records)
        {
            users.Add(kvp.Value.toJSON());
        }

        string output = JSONHelper.getKeyValuePair(name, JSONHelper.getArray(users.ToArray()));
        Debug.Log("Output: " + output);
        return output;
    }

    public string toString()
    {
        StringBuilder users = new StringBuilder();
        foreach (KeyValuePair<string, UserRecord> kvp in records)
        {
            users.Append(kvp.Value.toString() + "\n");
        }

        return users.ToString();
    }

    public void addRecord(UserRecord user)
    {
        records.Add(user.UserName, user);
    }

    public UserRecord getRecord(string username)
    {
        try
        {
            return records[username];
        }
        catch
        {
            return null;
        }
    }

    public static table csvToTable(string name, string data)
    {

        table t = new table(name);

        string[] rows = data.Split('\n');
        for (int i = 0; i < (rows.Length - 1); i++)
        {

            string[] fields = rows[i].Split(',');
            if (fields.Length < 6) continue;
            UserRecord user = new UserRecord(fields[0] + fields[1].Substring(0,3));
            user.FirstName = fields[0];
            user.LastName = fields[1];
            user.MiddleInitial = fields[2];
            user.Age = int.Parse(fields[3]);
            user.DOB = fields[4];
            user.ReadLevel = fields[5];
            t.addRecord(user);

        }

        return t;

    }

}

