using System;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Interaction
    {
        private static int InteractionCount;
        public string Id { get; }
        public DateTime Timestamp { get; }
        private bool WasPickedUp { get; }
        public string Name { get; }
        [JsonConstructor]
        public Interaction(string id, DateTime timestamp, bool wasPickedUp, string name)
        {
            this.Id = id;
            this.Timestamp = timestamp;
            this.WasPickedUp = wasPickedUp;
            this.Name = name;
            InteractionCount++;
        }
        public Interaction(DateTime timestamp, bool wasPickedUp, string name)
        {
            this.Id = (InteractionCount++).ToString();
            this.Timestamp = timestamp;
            this.WasPickedUp = wasPickedUp;
            this.Name = name;
            InteractionCount++;
        }
        
    }
}