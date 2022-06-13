using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataCollection.Models
{
    public class Session
    {
        public string Id { get; }
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; private set; }
        public Student Student { get; }
        public Dictionary<string, Movement> Movements { get; }
        public Dictionary<string, Decision> Decisions { get; }
        public Dictionary<string, Interaction> Interactions { get; }
        public Dictionary<string, Activity> Activities { get; }
        
        public Session(string id, Student student, DateTime? startTime = null, DateTime? endTime = null,
            IEnumerable<Movement> movements = null, IEnumerable<Decision> decisions = null,
            IEnumerable<Interaction> interactions = null, IEnumerable<Activity> activities = null)
        {
            Id = id;
            StartTime = (startTime is null) ? DateTime.UtcNow : startTime;
            EndTime = endTime;
            Student = student;
            Movements = new Dictionary<string, Movement>();
            if (!(movements is null))
            {
                foreach (var movement in movements)
                {
                    Movements[movement.Id] = movement;
                }
            }

            Decisions = new Dictionary<string, Decision>();
            if (!(decisions is null))
            {
                foreach (var decision in decisions)
                {
                    Decisions[decision.Id] = decision;
                }
            }

            Interactions = new Dictionary<string, Interaction>();
            if (!(interactions is null))
            {
                foreach (var interaction in interactions)
                {
                    Interactions[interaction.Id] = interaction;
                }
            }

            Activities = new Dictionary<string, Activity>();
            if (!(activities is null))
            {
                foreach (var activity in activities)
                {
                    Activities[activity.Id] = activity;
                }
            }
        }

        [JsonConstructor]
        public Session(string id, DateTime? startTime, DateTime? endTime, Student student,
            Dictionary<string, Movement> movements, Dictionary<string, Decision> decisions,
            Dictionary<string, Interaction> interactions, Dictionary<string, Activity> activities)
        {
            Id = id;
            Student = student;
            StartTime = startTime;
            EndTime = endTime;
            Movements = movements;
            Decisions = decisions;
            Interactions = interactions;
            Activities = activities;
        }

        public void End(DateTime time)
        {
            EndTime = time;
        }

        public void End()
        {
            EndTime = DateTime.UtcNow;
        }
    }
}