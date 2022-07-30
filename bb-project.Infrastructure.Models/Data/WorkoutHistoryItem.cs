using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class WorkoutHistoryItem
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long WorkoutId { get; set; }

        public long UserId { get; set; }
    }
}
