using System;
using System.Numerics;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Movement
    {
        public static int MovementCount;
        public string Id { get; }
        public Vector3 Root { get; }
        public string Scene { get; }
        public DateTime Timestamp { get; }

        [JsonConstructor]
        public Movement(string id, Vector3 root, string scene, DateTime timestamp)
        {
            Id = id;
            Root = root;
            Scene = scene;
            Timestamp = timestamp;
            MovementCount++;
        }
        public Movement(Vector3 root, string scene, DateTime timestamp)
        {
            Id = (MovementCount++).ToString();
            Root = root;
            Scene = scene;
            Timestamp = timestamp;
        }
    }
}