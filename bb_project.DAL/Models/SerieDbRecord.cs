using bb_project.Infrastructure.Models.Enums;

namespace bb_project.DAL.Models
{
    public class SerieDbRecord
    {
        public long ID { get; }

        public int Reps { get; set; }

        public int Rest { get; set; }

        public ExerciseMethodology ExerciseMethod { get; set; }

        public long WorkoutId { get; set; }

        public long OwnerExerciseId { get; set; }

        public string OwnerExerciseName { get; set; }
    }
}