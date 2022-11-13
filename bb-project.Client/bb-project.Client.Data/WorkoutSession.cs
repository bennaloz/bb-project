using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Data
{
    public class WorkoutSession
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ulong WorkoutId { get; set; }

        public ulong WorkoutPlanId { get; set; }

        public string UserId { get; set; }
    }
}
