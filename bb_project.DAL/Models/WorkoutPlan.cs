using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Models
{
    public class WorkoutPlan
    {
        public long ID { get; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
