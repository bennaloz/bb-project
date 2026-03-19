using bb_project.app.Contracts.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.app.Contracts.Models.Data
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
            Id = id;
            ExerciseMethod = exerciseMethod;
        }
    }
}
