using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.DAL.Models
{
    internal class WorkoutDbRecord
    {
        public long Id { get; }

        public string Name { get; set; }


        public static implicit operator Workout(WorkoutDbRecord workoutDbRecord)
        {
            return new Workout(workoutDbRecord.Id)
            {
                Name = workoutDbRecord.Name
            };
        }
    }
}
