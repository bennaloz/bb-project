
using bb_project.DAL.Models.Enums;

namespace bb_project.DAL.Models.DB
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