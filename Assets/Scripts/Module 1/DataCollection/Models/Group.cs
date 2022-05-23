
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Group
    {
        public string Id { get; }
        public Dictionary<string, Student> Members { get; }
        
        public Group(string id, IEnumerable<Student> members = null)
        {
            Id = id;
            Members = new Dictionary<string, Student>();
            if (!(members is null))
            {
                foreach (Student member in members)
                {
                    Members[member.Id] = member;
                }
            }
        }

        [JsonConstructor]
        public Group(string id, Dictionary<string, Student> members)
        {
            Id = id;
            Members = members;
        }
    }
}