using System;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Decision
    {
        public static int DecisionCount;
        public string Id { get; }
        public string Scene { get; }
        public DateTime Timestamp { get; }
        public string DecisionValue { get; }
        public string CorrectValue { get; }
        public string TaskId { get; }
        [JsonConstructor]
        public Decision(string id, string scene, DateTime timestamp, string decisionValue, string correctValue, string taskId)
        {
            Id = id;
            Scene = scene;
            Timestamp = timestamp;
            DecisionValue = decisionValue;
            CorrectValue = correctValue;
            TaskId = taskId;
            DecisionCount++;
        }
        public Decision(string scene, DateTime timestamp, string decisionValue, string correctValue, string taskId)
        {
            Id = (DecisionCount++).ToString();
            Scene = scene;
            Timestamp = timestamp;
            DecisionValue = decisionValue;
            CorrectValue = correctValue;
            TaskId = taskId;
        }
    }
}