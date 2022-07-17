
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Infrastructure.Models.Data
{
    public class Serie
    {
        public long ID { get; }

        public Exercise OwnerExercise { get; set; }

        public int Reps { get; set; }

        public int Rest { get; set; }

        public ExerciseMethodology ExerciseMethod { get; set; }

        public Serie(long id, Exercise ownerExercise)
        {
            this.ID = id;
            this.OwnerExercise = ownerExercise;
        }
    }
}