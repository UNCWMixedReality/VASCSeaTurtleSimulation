#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple Logging script, utilizing Debug.Log, to conditionally print messages
/// to the games log.
/// 
/// Also appends [DEVELOPER]: to the front of the log message for better visbility
/// in the headsets Log
/// </summary>
public class LogManager : MonoBehaviour
{

#if DEBUG
    static bool LoggingEnabled = true;
#else
    static LoggineEnabled = false;
#endif

    public static void LogMessage(string ValueToPrint, bool Error = false)
    {
        if ((LoggingEnabled) && (!Error))
        {
            Debug.Log($"[DEVELOPER]: {ValueToPrint}");
        }
        else if ((LoggingEnabled) && (Error))
        {
            Debug.LogError($"[DEVELOPER]: {ValueToPrint}");
        }
    }
}
