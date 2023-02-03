using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class database
{

    public static Dictionary<string, table> tables;
    private static string tableExtension = ".table";
    private static string tableFilePath = Directory.GetCurrentDirectory() + @"\Assets\Scripts\RemoteInterface\database\tables";

    public static void init()
    {

        tables = new Dictionary<string, table>();
        //fetchTables();

        /*
        table t = new table("default");
        UserRecord daniel = new UserRecord("daniel");
        daniel.Pass = "5d41402abc4b2a76b9719d911017c592";
        daniel.Permission = 2;
        daniel.FirstName = "Daniel";
        daniel.LastName = "Vawn";
        daniel.ReadLevel = "3A";
        daniel.Module_1 = 3;
        daniel.Module_2 = 2;
        t.addRecord(daniel);
        writeTable(t);
        */

        fetchTables();

    }

    public static bool addTable(table t)
    {

        if (tables.ContainsKey(t.name)) return false;

        bool result = writeTable(t);
        if (result) fetchTables();

        return result;

    }

    // check if user exists
    public static bool containsUser(string username)
    {
        foreach (KeyValuePair<string, table> kvp in tables)
        {
            if (kvp.Value.getRecord(username) != null) return true;
        }

        return false;

    }

    // get specific user record
    public static UserRecord getUser(string username)
    {
        foreach (KeyValuePair<string, table> kvp in tables)
        {
            UserRecord record = kvp.Value.getRecord(username);
            if (record != null) return record;

        }

        return null;

    }

    public static string printUserData(string username)
    {

        foreach (KeyValuePair<string, table> kvp in tables)
        {
            UserRecord record = kvp.Value.getRecord(username);
            if (record != null)
            {
                StringBuilder output = new StringBuilder();
                output.Append("Username:" + record.UserName);
                output.Append(",FirstName:" + record.FirstName);
                output.Append(",LastName:" + record.LastName);
                output.Append(",ReadLevel:" + record.ReadLevel);
                output.Append(",Permission:" + record.Permission);
                return output.ToString();
            }
        }

        return "INVALID|User Not Found";

    }

    public static string getTable(string name)
    {
        try
        {
            return tables[name].toString();

        } catch(Exception e)
        {
            return name + " + INVALID TABLE: " + e.ToString();
        }
    }

    // List all tables in database
    public static string[] listTables()
    {
        List<string> keys = new List<string>();
        foreach(KeyValuePair<string, table> k in tables)
        {
            keys.Add(k.Key);
        }

        return keys.ToArray();
    }

    private static void fetchTables()
    {

        Debug.Log("Database.cs: fetchTables()");

        
        List<string> files = new List<string>();
        files.AddRange(Directory.GetFiles(tableFilePath));
        
        foreach (string f in files)
        {
            if (f.EndsWith(tableExtension))
            {

                string name = f.Substring(tableFilePath.Length + 1);
                name = name.Remove(name.LastIndexOf('.'));
                
                readTable(name);

            }
        }

    }

    private static bool writeTable(table t)
    {

        try
        {

            string path = tableFilePath + @"\" + t.name + tableExtension;

            using (Stream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, t);
            }

            return true;

        }
        catch
        {

            if (EventLog.logReady())
            {
                EventLog.Log("database", "Could not write table to file", EventLog.EventType.Error);
            } else
            {
                Debug.LogError("database: Could not write table to file");
            }
            return false;
        }

    }

    private static bool readTable(string name)
    {

        try
        {

            string path = tableFilePath + @"\" + name + tableExtension;

            using (Stream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                tables.Add(name, (table)bf.Deserialize(stream));
            }

            return true;

        }
        catch
        {
            if (EventLog.logReady())
            {
                EventLog.Log("database", "Could not read file to table", EventLog.EventType.Error);
            }
            else
            {
                Debug.LogError("database: Could not read file to table");
            }
            
            return false;
        }

    }

}
