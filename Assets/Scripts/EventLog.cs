/*
 *  VASC
 *  EventLog:
 *      Writes log file in .CSV format
 *      
 *  Daniel vaughn
 *  10/24/2019
 */

using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;
using antilunchbox;
using UnityEngine;
//using Debug = UnityEngine.Debug;

public class EventLog : Singleton<EventLog>
{

    // output file
    private static string DOCPATH = null;
    private static string LOGNAME = "VASC_Log_";
    private static int SUFFIX = 0;
    private static short SUFFIXLEN = 4;
    private static string extension = "txt";

    // output writer
    private static StreamWriter outputLog = null;

    // document header
    private const string HEADER = "Virtual Access to STEM Careers [VASC] Log\nTIME,LEVEL,ENTITY,LOG";

    // stopwatch
    private Stopwatch timer = null;

    // log event types
    private static string[] EventTypes = { "Info","Debug","Warning","Error" };
    public enum EventType
    {
        Info,
        Debug,
        Warning,
        Error
    }

    // verbosity level
    public static bool enableDebug = true;
    public static bool enableInfo = true;

    // log name
    private const string logName = "EventLog";

    // auto destruct
    /*
    ~EventLog() {

        Log(logName, "EventLog closed.", EventType.Warning);
        outputLog.Dispose();
        outputLog.Close();

    }
    */

    // manual destruct
    public static void Stop()
    {
        Log(logName, "EventLog closed.", EventType.Warning);
        outputLog.Dispose();
        outputLog.Close();
    }

    public static bool logReady()
    {

        return (outputLog != null);
 
    }

    void Awake()
    {

        DontDestroyOnLoad(this);

        // setup program timer
        timer = new Stopwatch();
        timer.Start();

        // find output file
        DOCPATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string[] files = Directory.GetFiles(DOCPATH);

        foreach (string file in files) {

            try
            {
                if (file.Substring(DOCPATH.Length + 1, LOGNAME.Length).Equals(LOGNAME))
                {

                    string suffix = file.Substring(DOCPATH.Length + LOGNAME.Length + 1, SUFFIXLEN);
                    if (int.TryParse(suffix, out SUFFIX))
                    {
                        SUFFIX++;
                    }

                }

            } catch { continue; }

        }

        // logname with suffix
        LOGNAME += string.Format("{0:D4}.{1}", SUFFIX, extension);

        outputLog = new StreamWriter(Path.Combine(DOCPATH, LOGNAME), true);
        outputLog.WriteLine(HEADER);

        Log(logName, "EventLog started.", EventType.Warning);

    }

    public static void SetVerboseLevel(bool debug, bool info) {

        enableDebug = debug;
        enableInfo = info;

    }

    public static void Log(string name, string log, EventType ev = EventType.Debug)
    {

        if (!enableDebug && (ev == EventType.Debug))
        {
            return;
        }

        if (!enableInfo && (ev == EventType.Info))
        {
            return;
        }

        if (ev != EventType.Info)
        {
            switch (ev)
            {
                case (EventType.Debug):
                    UnityEngine.Debug.Log(name + ": " + log);
                    break;
                case (EventType.Warning):
                    UnityEngine.Debug.LogWarning(name + ": " + log);
                    break;
                case (EventType.Error):
                default:
                    UnityEngine.Debug.LogError(name + ": " + log);
                    break;
            }
            
        }

        // timestamp
        TimeSpan ts = EventLog.Instance.timer.Elapsed;

        // format and write to log
        string logLine = string.Format("{0:00}:{1:00}:{2:00}.{3:00},{4},{5},[{6}]",
            ts.Hours, ts.Minutes, ts.Seconds, (ts.Milliseconds / 10),
            EventTypes[(int)ev],
            name,
            log);
        outputLog.WriteLine(logLine);

    }

}
