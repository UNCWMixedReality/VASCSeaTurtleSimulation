using System;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Activity
    {
        public static int ActivityCount;
        public string Id { get; }
        public string Scene { get; }
        public DateTime Timestamp { get; }
        public string ExtraData { get; }
        
        [JsonConstructor]
        public Activity(string id, string scene, DateTime timestamp, string extraData)
        {
            Id = id;
            Scene = scene;
            Timestamp = timestamp;
            ExtraData = extraData;
            ActivityCount++;
        }
        public Activity(string scene, DateTime timestamp, string extraData)
        {
            Id = (ActivityCount++).ToString();
            Scene = scene;
            Timestamp = timestamp;
            ExtraData = extraData;
        }

        public Activity(DateTime timestamp, string scene)
        {
            Id = (ActivityCount++).ToString();
            Scene = scene;
            Timestamp = timestamp;
        }
    }
}