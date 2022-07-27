
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
<<<<<<< HEAD:Assets/Scripts/Module_1/DataCollection/DcDataLogging.cs
            // TODO read in largest sessionID from json file
            // _sessionID = this.LoadLatestSessionId();
=======
            LoadLatestSessionId();
>>>>>>> 580834f2351c37d24f6385e177437e02946edac1:Assets/Scripts/DataCollection/DcDataLogging.cs
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
<<<<<<< HEAD:Assets/Scripts/Module_1/DataCollection/DcDataLogging.cs
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
=======
>>>>>>> 580834f2351c37d24f6385e177437e02946edac1:Assets/Scripts/DataCollection/DcDataLogging.cs

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
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory($"{Application.persistentDataPath}/Log");
                    files = Array.Empty<string>();
                }
                Models.Session.sessionCount = files.Length;
            }
            
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
            UnityEngine.Debug.Log("Beginning session");
            if (DcDataLogging.Student != null)
            {
                LoadLatestSessionId();
                Session = new Models.Session(DcDataLogging.Student);
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

        }

        public static void ExportData()
        {
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