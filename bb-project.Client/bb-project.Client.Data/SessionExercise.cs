using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Data
{
    public class SessionExercise
    {
        public int Id { get; set; }
        
        public ulong ExerciseId { get; set; }

        public double? UsedKgs { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ExerciseType Type { get; set; }
    }
}
