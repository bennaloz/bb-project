
using bb_project.DAL.Models.Enums;

namespace bb_project.DAL.Models
{
    public class Serie
    {
        public long ID { get; }

        public Exercise OwnerExercise { get; set; }

        public int Reps { get; set; }

        public int Rest { get; set; }

        public ExerciseMethodology ExerciseMethod { get; set; }
    }
}