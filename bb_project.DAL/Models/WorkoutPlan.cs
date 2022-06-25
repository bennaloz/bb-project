using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Models
{
    public class WorkoutPlan
    {
        public int ID { get; }

        public string Name { get; set; }

        public IEnumerable<Workout> Workouts { get; set; }
    }
}
