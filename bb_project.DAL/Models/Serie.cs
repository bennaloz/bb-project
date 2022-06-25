using bb_project.Models.Enums;

namespace bb_project.Models
{
    public class Serie
    {
        public long ID { get; }

        public long OwnerExerciseId { get; set; }

        public int Reps { get; set; }

        public int Rest { get; set; }

        public ExerciseKind Kind { get; set; }
    }
}