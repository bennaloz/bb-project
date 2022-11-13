using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using static bb_project.Infrastructure.Models.Events.StartWorkoutEvent;

namespace bb_project.Infrastructure.Models.Events
{
    public class StartWorkoutEvent : PubSubEvent<StartWorkoutEventData>
    {
        public class StartWorkoutEventData
        {
            public string UserId { get; set; }

            public ulong WorkoutId { get; set; }
        }
    }
}
