/*

    Daniel Vaughn
    UNCW VR_Research_Team

    sessionManager.cs

*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session
{

    public string id;
    public int permissionLevel;
    public UserRecord user;

    //TODO: Add session timer

    public Session(UserRecord user)
    {
        id = Guid.NewGuid().ToString();
        this.user = user;
        permissionLevel = user.Permission;
    }

}

public class sessionManager : MonoBehaviour
{

    public static Dictionary<string, Session> sessions = new Dictionary<string, Session>();

    public static string createSession(UserRecord user)
    {
        Session s = new Session(user);
        sessions.Add(s.id, s);
        return s.id;
    }

    private static void addSession(Session session)
    {
        sessions.Add(session.id, session);
    }

    public static bool removeSession(Session session)
    {
        try { 
            sessions.Remove(session.id);
            return true;
        } catch
        {
            return false;
        }
    }

    public void Update()
    {

        //TODO: Prune expired sessions

    }

}
