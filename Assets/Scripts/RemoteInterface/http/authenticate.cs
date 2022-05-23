/*

    Daniel Vaughn
    UNCW VR_Research_Team

    authenticate.cs

*/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using UnityEngine;

/*
public class User
{
    public string name;
    public string auth;
    public int permission = 0;

    public User(string name, string auth)
    {

        this.name = name;
        this.auth = auth;

    }

    public User(string name, string auth, int permission)
    {

        this.name = name;
        this.auth = auth;
        this.permission = permission;

    }

}
*/

public class authenticate
{

    //public static Dictionary<string, User> users;
    //private static string userFile = @"\Assets\Scripts\RemoteInterface\http\users.csv";
    //private static char delimit = ',';

    public static string login(string name, string auth) 
    {

        UserRecord user = database.getUser(name);
        if (user != null)
        {

            if (authenticateUser(user, hashPassword(auth)))
            {
                return sessionManager.createSession(user);
            }

        }

        /*
        if (users.ContainsKey(name))
        {

            if (authenticateUser(name, hashPassword(auth)))
            {

                return sessionManager.createSession(users[name]);

            }

        }
        */

        return "INVALID";

    }

    // Hash user password using MD5
    public static string hashPassword(string auth)
    {
        StringBuilder hex = new StringBuilder();

        using (MD5 hasher = MD5.Create())
        {

            byte[] rawHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(auth));
            foreach (byte d in rawHash) hex.Append(d.ToString("x2"));

        }

        return hex.ToString();

    }

    public static bool compareHash(string h1, string h2)
    {
        // hash may be in lowercase or uppercase
        StringComparer sc = StringComparer.OrdinalIgnoreCase;

        return (sc.Compare(h1, h2) == 0);

    }



    // using new database
    public static bool authenticateUser(UserRecord user, string pass)
    {
        return (compareHash(user.Pass, pass));
    }

    /*
    public static bool addUserWithPlaintextPassword(string name, string auth, int permssion = 0)
    {

       return addUser(name, hashPassword(auth), permssion);

    }

    public static bool authenticateUser(string name, string pass)
    {
        return (compareHash(users[name].auth, pass));
    }

    public static bool addUser(string name, string auth, int permssion = 0)
    {

        try
        {
            using (StreamWriter writer = File.AppendText(userFile))
            {
                writer.WriteLine(name + "," + auth.ToString() + "," + permssion.ToString());
            }
        }

        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        fetchUserData();
        return true;

    }

    public static void fetchUserData()
    {

        users = new Dictionary<string, User>();

        string topPath = Directory.GetCurrentDirectory();
        userFile = topPath + userFile;

        try
        {
            using (StreamReader dataFile = new StreamReader(userFile))
            {
                while (!dataFile.EndOfStream)
                {
                    string[] userData = dataFile.ReadLine().Split(delimit);

                    if (userData.Length > 2)
                    {
                        users.Add(userData[0], new User(userData[0], (userData[1]), Int32.Parse(userData[2])));
                    }

                    else
                    {
                        users.Add(userData[0], new User(userData[0], (userData[1])));
                    }

                }
            }

        }

        catch (Exception e)
        {
            Debug.Log("Could not open user file: " + e.ToString());
        }

    }
    */

}
