using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Student
    {
        private static int StudentCount;
        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Dictionary<string, Session> Sessions { get; }
        public Student(string id, string firstName, string lastName, IEnumerable<Session> sessions = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Sessions = new Dictionary<string, Session>();
            if (!(sessions is null))
            {
                foreach (Session session in sessions)
                {
                    Sessions[session.Id] = session;
                }
            }
            StudentCount++;
        }

        public Student(string firstName, string lastName, IEnumerable<Session> sessions = null)
        {
            Id = (StudentCount++).ToString();
            FirstName = firstName;
            LastName = lastName;
            Sessions = new Dictionary<string, Session>();
            if (!(sessions is null))
            {
                foreach (Session session in sessions)
                {
                    Sessions[session.Id] = session;
                }
            }
        }
        [JsonConstructor]
        public Student(string id, string firstName, string lastName, Dictionary<string, Session> sessions)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Sessions = sessions;
            StudentCount++;
        }

    }
}