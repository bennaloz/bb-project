using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class ExerciseGroup
    {
        public ulong Id { get; set; }

        public ExerciseMethodology ExerciseMethod { get; }

        public Dictionary<ulong, Exercise> Exercises { get; } = new Dictionary<ulong, Exercise>();

        public ExerciseGroup(ExerciseMethodology exerciseMethod)
            : this(0, exerciseMethod)
        {

        }

        public ExerciseGroup(ulong id, ExerciseMethodology exerciseMethod)
        {
            this.Id = id;
            ExerciseMethod = exerciseMethod;
        }
    }
}
