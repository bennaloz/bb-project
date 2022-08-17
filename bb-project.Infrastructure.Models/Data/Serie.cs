
using bb_project.Infrastructure.Models.Enums;
using System;

namespace bb_project.Infrastructure.Models.Data
{
    public class Serie
    {
        public long ID { get; }

        public ExerciseDefinition ExerciseDefinition { get; }

        public int Reps { get; set; }

        public TimeSpan Rest { get; set; }

        public Serie(ExerciseDefinition exerciseDefinition)
            : this(0, exerciseDefinition)
        {

        }

        public Serie(long id, ExerciseDefinition exerciseDefinition)
        {
            this.ID = id;
            this.ExerciseDefinition = exerciseDefinition;
        }
    }
}