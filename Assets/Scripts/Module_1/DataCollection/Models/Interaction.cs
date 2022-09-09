using System;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Interaction
    {
        public static int InteractionCount = 0;
        public string Id { get; }
        public DateTime Timestamp { get; }
        public bool WasPickedUp { get; }
        public string Name { get; }
        public string Scene { get; }
        [JsonConstructor]
        public Interaction(string id, DateTime timestamp, bool wasPickedUp, string name, string scene)
        {
            this.Id = id;
            this.Timestamp = timestamp;
            this.WasPickedUp = wasPickedUp;
            this.Name = name;
            this.Scene = scene;
            InteractionCount++;
        }
        public Interaction(DateTime timestamp, bool wasPickedUp, string name, string scene)
        {
            this.Id = (InteractionCount++).ToString();
            this.Timestamp = timestamp;
            this.WasPickedUp = wasPickedUp;
            this.Name = name;
            this.Scene = scene;
            InteractionCount++;
        }
        
    }
}