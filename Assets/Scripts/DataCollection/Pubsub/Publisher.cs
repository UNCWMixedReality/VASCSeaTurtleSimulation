using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataCollection
{
    public static class Publisher
    {
        /*
         * The publisher class maintains a dict of all events and which subscribers are listening to those events
         * It includes functionality to add/remove subscribers and functions to publish events of each type
         */

        public static Dictionary<string, List<ISubscriber>> SubList = new()
        {
            {"Decision", new List<ISubscriber>(){ } },
            {"Interaction", new List<ISubscriber>(){ } },
            { "Movement", new List<ISubscriber>(){ } },
            {"Activity", new List<ISubscriber>(){ } },
            {"General", new List<ISubscriber>(){ } }
        };

        public static void AddSubscriber(ISubscriber NewSub, params string[] Types)
        {
            /*
             * adds a subscriber to listen on each specified event type
             */
            foreach (string event_type in Types)
            {
                SubList[event_type].Add(NewSub);
            }
            //add subscriber to the master list
            SubList["General"].Add(NewSub);
        }

        public static void RemoveSubscriber(ISubscriber OldSub, params string[] Types)
        {
            //remove subscriber from each event type you want it removed from
            foreach (string event_type in Types)
            {
                SubList[event_type].Remove(OldSub);
            }
            //remove subscriber from the master list
            SubList["General"].Remove(OldSub);
        }

        #region Publishers
        //invokes the callback method for the appropriate subscribers
        public static void PublishEvent(string event_type="General")
        {
            foreach (ISubscriber Sub in SubList[event_type])
            {
                Sub.Callback();
            }
        }

        public static void PublishDecision()
        {
            PublishEvent("Decision");
        }

        public static void PublishInteraction()
        {
            PublishEvent("Interaction");
        }

        public static void PublishMovement()
        {
            PublishEvent("Movement");
        }

        public static void PublishActivity()
        {
            PublishEvent("Activity");
        }

        #endregion
    }
    
}
