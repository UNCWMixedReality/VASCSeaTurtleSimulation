using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Log
    {
        public string Id { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public string Line { get; }
        public Dictionary<string, Session> Sessions;
        public Dictionary<string, Student> Students;

        public Log(string id, DateTime startTime, DateTime endTime, string line, IEnumerable<Session> sessions = null,
            IEnumerable<Student> students = null)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Line = line;
            Sessions = new Dictionary<string, Session>();
            if (!(sessions is null))
            {
                foreach (Session session in sessions)
                {
                    Sessions[session.Id] = session;
                }
            }
            Students = new Dictionary<string, Student>();
            if (!(students is null))
            {
                foreach (Student student in students)
                {
                    Students[student.Id] = student;
                }
            }
        }

        [JsonConstructor]
        public Log(string id, DateTime startTime, DateTime endTime, string line,
            Dictionary<string, Session> sessions, Dictionary<string, Student> students)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Line = line;
            Sessions = sessions;
            Students = students;
        }
    }
}