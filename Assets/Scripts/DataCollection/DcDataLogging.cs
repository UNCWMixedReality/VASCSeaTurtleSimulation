
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataCollection.Models;
using System.IO;
using DataCollection.Converters;
using Newtonsoft.Json;
using UnityEngine;
using Debug = UnityEngine.Debug;
using DataCollection.PubSub;

namespace DataCollection
{
    public static class DcDataLogging
    {
        public static string SessionId;
        public static Models.Session Session;
        public static Student Student;
        public static Dictionary<string, string> CorrectAnswers = new Dictionary<string, string>();

        static DcDataLogging()
        {
            LoadLatestSessionId();
        }
        
        public static void SetCorrectAnswer(string taskId, string correctAnswer)
        {
            if (CorrectAnswers.ContainsKey(taskId))
            {
                CorrectAnswers[taskId] = correctAnswer;
            }
            else
            {
                CorrectAnswers.Add(taskId, correctAnswer);
            }
        }

        private static void LoadLatestSessionId()
        {

            if (Models.Session.sessionCount == 0)
            {
                string[] files;
                try
                {
                    files = Directory.GetFiles($"{Application.persistentDataPath}/Log");
                    if (files.Length > 0)
                    {
                        LogManager.LogMessage($"Found Files! Here's a path: {files[0]}");
                    }
                    Models.Session.sessionCount = files.Length;
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory($"{Application.persistentDataPath}/Log");
                    Models.Session.sessionCount = 0;
                }
            }
            
            SessionId = Models.Session.sessionCount.ToString();

        }
        
        public static Models.Session BeginSession(Student student)
        {
            UnityEngine.Debug.Log("Beginning session");
            Student = student;
            LoadLatestSessionId();
            Session = new Models.Session(student);
            Debug.Log(Session.ToString());
            return Session;
        }
        
        public static Models.Session BeginSession()
        {
            Debug.Log("Beginning session");
            LoadLatestSessionId();
            if (DcDataLogging.Student != null)
            {
                DcDataLogging.Session = new Models.Session(DcDataLogging.Student);
                return Session;
            }
            Debug.Log("IMPORTANT DATA COLLECTION WARNING: Student was not set for this session");
            DcDataLogging.Session = new Models.Session(new Student("NO NAME ENTERED", ""));
            return Session;
        }

        public static void EndSession()
        {
            UnityEngine.Debug.Log("Ending session");
            Session.End();
            //SubmitDataToServer();

        }

        public static void ExportData()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new DateTimeNullableConverter());
            using (StreamWriter file = new StreamWriter($"{Application.persistentDataPath}/Log/Session-{Session.Id}.json"))
            {
                serializer.Serialize(file, DcDataLogging.Session);
            }

        }

        public static async void SubmitDataToServer()
        {
            string NewServerPayload = JsonConvert.SerializeObject(DcDataLogging.Session);
            LogManager.LogMessage($"Session Data: {NewServerPayload.Substring(0, 30)}");
            if (await APIManager.HeadsetIsConnectedToInternet())
            {
                (bool, string) results = await APIManager.SubmitNewSessionDataToServer(NewServerPayload);

                LogManager.LogMessage($"{results.Item1.ToString()} - {results.Item2}");
            }
        }
        
        #region LogMethods

        public static void LogInteraction(Interaction data)
        {
            // Add interaction to interactions log
            Session.Interactions[data.Id] = data;
            // Export data to json file on each interaction
            ExportData();
            // Publish data to any subscribers
            Publisher.PublishInteraction(data);
        }

        public static void LogDecision(Decision data)
        {
            // Add Decision to decisions log
            Session.Decisions[data.Id] = data;
            // Export data to json file on each interaction
            ExportData();
            // Publish data to any subscribers
            Publisher.PublishDecision(data);
        }

        public static void LogMovement(Movement data)
        {
            // Add movement to movements log
            Session.Movements[data.Id] = data;
            // Export data to json file on each interaction
            ExportData();
            // Publish data to any subscribers
            Publisher.PublishMovement(data);
        }

        public static void LogActivity(Activity data)
        {
            // Add activity to activities log
            Session.Activities[data.Id] = data;
            // Export data to json file on each interaction
            ExportData();
            // Publish data to any subscribers
            Publisher.PublishActivity(data);
        }
        #endregion
    }
}