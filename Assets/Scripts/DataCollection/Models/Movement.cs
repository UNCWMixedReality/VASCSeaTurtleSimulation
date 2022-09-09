using System;
using System.Numerics;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Movement
    {
        private static int MovementCount;
        public string Id { get; }
        public Vector3 Root { get; }
        public DateTime Timestamp { get; }

        [JsonConstructor]
        public Movement(string id, Vector3 root, DateTime timestamp)
        {
            Id = id;
            Root = root;
            Timestamp = timestamp;
            MovementCount++;
        }
        public Movement(Vector3 root, DateTime timestamp)
        {
            Id = (MovementCount++).ToString();
            Root = root;
            Timestamp = timestamp;
        }
    }
}