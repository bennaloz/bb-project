
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Infrastructure.Models.Data
{
    public class Serie
    {
        public long ID { get; }

        public ExerciseDefinition ExerciseDefinition { get; set; }

        public int Reps { get; set; }

        public int Rest { get; set; }

        public Serie(long id, ExerciseDefinition exerciseDefinition)
        {
            this.ID = id;
            this.ExerciseDefinition = exerciseDefinition;
        }
    }
}