using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.DAL.Models
{
    public class WorkoutDataDbRecord
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double UsedKgs { get; set; }
    }
}
