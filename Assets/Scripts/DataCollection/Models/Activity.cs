using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Activity
    {
        public static int ActivityCount;
        public string Id { get; }
        public string Scene { get; }
        public DateTime Timestamp { get; }
        public string ActivityDetail { get; }
        
        [JsonConstructor]
        public Activity(string id, string scene, DateTime timestamp, string activityDetail)
        {
            Id = id;
            Scene = scene;
            Timestamp = timestamp;
            ActivityDetail = activityDetail;
            ActivityCount++;
        }
        public Activity(DateTime timestamp, string scene, string activityDetail)  // First Attempt at what might be needed for activity log
        {
            Id = (ActivityCount++).ToString();
            Scene = scene;
            Timestamp = timestamp;
            ActivityDetail = activityDetail;
        }
    }
}