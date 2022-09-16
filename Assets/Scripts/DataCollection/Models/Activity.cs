using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Activity: IDataModel
    {
        public static int ActivityCount;
        public string Id { get; }
        public DateTime Timestamp { get; }
        public string ActivityDetail { get; }
        
        [JsonConstructor]
        public Activity(string id, DateTime timestamp, string activityDetail)
        {
            Id = id;
            Timestamp = timestamp;
            ActivityDetail = activityDetail;
            ActivityCount++;
        }
        public Activity(DateTime timestamp, string activityDetail)  // First Attempt at what might be needed for activity log
        {
            Id = (ActivityCount++).ToString();
            Timestamp = timestamp;
            ActivityDetail = activityDetail;
        }
    }
}