
using bb_project.Infrastructure.Models.Enums;
using System;

namespace bb_project.Infrastructure.Models.Data
{
    public class Serie
    {
        public ulong Id { get; }

        public ExerciseDefinition ExerciseDefinition { get; }

        public int Reps { get; set; }

        public TimeSpan Rest { get; set; }

        public Serie(ExerciseDefinition exerciseDefinition)
            : this(0, exerciseDefinition)
        {

        }

        public Serie(ulong id, ExerciseDefinition exerciseDefinition)
        {
            this.Id = id;
            this.ExerciseDefinition = exerciseDefinition;
        }
    }
}