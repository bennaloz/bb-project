using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.DAL.Models
{
    public class WorkoutPlanDbRecord
    {
        public ulong ID { get; }

        public string Name { get; set; }

        public bool IsActive { get; set; }


        public static implicit operator WorkoutPlan(WorkoutPlanDbRecord dbRecord)
        {
            var result = new WorkoutPlan(dbRecord.ID);

            result.Name = dbRecord.Name;
            result.IsActive = dbRecord.IsActive;

            return result;
        }
    }
}
