
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
using System.Threading.Tasks;

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
            // TODO read in largest sessionID from json file
            // _sessionID = this.LoadLatestSessionId();
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

        private static int LoadLatestSessionId()
        {
            string[] files;
            // TODO count the amount of files in Log folder
            try
            {
                files = Directory.GetFiles("Log");
                LogManager.LogMessage($"Found Files! Here's a path: {files[0].ToString()}");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory($"{Application.persistentDataPath}/Log");
                files = Array.Empty<string>();
            }
            return files.Length;
        }

        public static Models.Session BeginSession(Student student)
        {
            UnityEngine.Debug.Log("Beginning session");
            Student = student;
            SessionId = LoadLatestSessionId().ToString();
            Session = new Models.Session(SessionId, student);
            Debug.Log(Session.ToString());
            return Session;
        }

        public static Models.Session BeginSession()
        {
            UnityEngine.Debug.Log("Beginning session");
            if (DcDataLogging.Student != null)
            {
                Session = new Models.Session(SessionId, DcDataLogging.Student);
                SessionId = LoadLatestSessionId().ToString();
                return Session;
            }

            throw new Exception(
                "DcDataLogging.Student is null. If calling BeginSession without a Student you" +
                " must set this attribute yourself first."
            );
        }

        public static void EndSession()
        {
            UnityEngine.Debug.Log("Ending session");
            Session.End();
            //SubmitDataToServer();
            ExportData();
            
        }

        public static void ExportData()
        {
            if (!Directory.Exists($"{Application.persistentDataPath}/Log"))
            {
                Directory.CreateDirectory($"{Application.persistentDataPath}/Log");
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new DateTimeNullableConverter());
            string New_GUID = Guid.NewGuid().ToString();
            using (StreamWriter file = new StreamWriter($"{Application.persistentDataPath}/Log/Session-{New_GUID}.json"))
            {
                
                serializer.Serialize(file, DcDataLogging.Session);
            }

        }

        public static async void SubmitDataToServer()
        {
            string NewServerPayload = JsonConvert.SerializeObject(DcDataLogging.Session);
            LogManager.LogMessage($"Session Data: {NewServerPayload.Substring(0, 30)}");
            var ConnectionCheck = APIManager.HeadsetIsConnectedToInternet();
            (bool, string) results = await APIManager.SubmitNewSessionDataToServer(NewServerPayload);

            LogManager.LogMessage($"{results.Item1.ToString()} - {results.Item2}");
            
        }

        public static void LogInteraction(Interaction data)
        {
            if (Session.Interactions.ContainsKey(data.Id))
            {
                Session.Interactions[data.Id] = data;
            }
            else
            {
                Session.Interactions.Add(data.Id, data);
            }
            
            ExportData();
        }

        public static void LogDecision(Decision data)
        {
            if (Session.Decisions.ContainsKey(data.Id))
            {
                Session.Decisions[data.Id] = data;
            }
            else
            {
                Session.Decisions.Add(data.Id, data);
            }
            ExportData();
        }

        public static void LogMovement(Movement data)
        {
            if (Session.Movements.ContainsKey(data.Id))
            {
                Session.Movements[data.Id] = data;
            }
            else
            {
                Session.Movements.Add(data.Id, data);
            }
            ExportData();
        }

        public static void LogActivity(Activity data)
        {
            if (Session.Activities.ContainsKey(data.Id))
            {
                Session.Activities[data.Id] = data;
            }
            else
            {
                Session.Activities.Add(data.Id, data);
            }
            ExportData();
        }

    }
}