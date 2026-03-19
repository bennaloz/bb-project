using bb_project.app.Contracts.Models.Data;
using bb_project.app.Contracts.Models.Enums;

namespace bb_project.app.DataAccess.Models
{
    public class SerieDbRecord
    {
        public ulong Id { get; internal set; }

        public int Reps { get; set; }

        public TimeSpan Rest { get; set; }

        public ulong SeriesGroupId { get; set; }

        public ulong WorkoutId { get; set; }

        public ulong DefinitionExerciseId { get; set; }

        public string DefinitionExerciseName { get; set; } = "";

        public ExerciseMethodology ExerciseMethod { get; set; }

        public ExerciseType DefinitionExerciseType { get; set; }


        public static implicit operator Serie(SerieDbRecord serieDbRecord)
        {
            Serie result = new Serie(serieDbRecord.Id);
            result.Rest = serieDbRecord.Rest;
            result.Reps = serieDbRecord.Reps;
            return result;
        }

        public static implicit operator SerieDbRecord(Serie serie)
        {
            var result = new SerieDbRecord();
            result.Rest = serie.Rest;
            result.Reps = serie.Reps;
            return result;
        }
    }
}