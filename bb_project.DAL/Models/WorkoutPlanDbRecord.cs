using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.DAL.Models
{
    public class WorkoutPlanDbRecord
    {
        public long ID { get; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public static implicit operator WorkoutPlan(WorkoutPlanDbRecord dbRecord)
        {
            var result = new WorkoutPlan();

            return result;
        }
    }
}
