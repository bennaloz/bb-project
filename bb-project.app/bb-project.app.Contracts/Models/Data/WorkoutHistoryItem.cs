using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.app.Contracts.Models.Data
{
    public class WorkoutHistoryItem
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ulong WorkoutId { get; set; }

        public string UserId { get; set; }
    }
}
