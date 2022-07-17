using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.DAL.Models
{
    internal class SerieDbRecord
    {
        public long ID { get; }

        public int Reps { get; set; }

        public int Rest { get; set; }

        public ExerciseMethodology ExerciseMethod { get; set; }

        public long WorkoutId { get; set; }

        public long OwnerExerciseId { get; set; }

        public string OwnerExerciseName { get; set; }

        public ExerciseType OwnerExerciseType { get; set; }

        public static implicit operator Serie(SerieDbRecord serieDbRecord)
        {
            var exercise = new Exercise(serieDbRecord.OwnerExerciseId);
            exercise.Name = serieDbRecord.OwnerExerciseName;
            exercise.Type = serieDbRecord.OwnerExerciseType;

            Serie result = new Serie(serieDbRecord.ID, exercise);
            result.ExerciseMethod = serieDbRecord.ExerciseMethod;
            result.Rest = serieDbRecord.Rest;
            result.Reps = serieDbRecord.Reps;
            return result;
        }

        public static implicit operator SerieDbRecord(Serie serie)
        {
            var result = new SerieDbRecord();
            result.ExerciseMethod = serie.ExerciseMethod;
            result.OwnerExerciseId = serie.OwnerExercise.ID;
            result.Rest = serie.Rest;
            result.Reps = serie.Reps;
            return result;
        }
    }
}