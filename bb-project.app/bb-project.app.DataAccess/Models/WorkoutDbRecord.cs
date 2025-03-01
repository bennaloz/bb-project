using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.DAL.Models
{
    public class WorkoutDbRecord
    {
        public ulong Id { get; }

        public string Name { get; set; }

        public int Order { get; set; }


        public static implicit operator Workout(WorkoutDbRecord workoutDbRecord)
        {
            return new Workout(workoutDbRecord.Id)
            {
                Name = workoutDbRecord.Name,
                Order = workoutDbRecord.Order
            };
        }
    }
}
