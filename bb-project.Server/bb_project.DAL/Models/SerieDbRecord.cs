using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;
using System;

namespace bb_project.Infrastructure.DAL.Models
{
    public class SerieDbRecord
    {
        public long Id { get; internal set; }

        public int Reps { get; set; }

        public TimeSpan Rest { get; set; }

        public long SeriesGroupId { get; set; }

        public long WorkoutId { get; set; }

        public long DefinitionExerciseId { get; set; }

        public string DefinitionExerciseName { get; set; }

        public ExerciseType DefinitionExerciseType { get; set; }

        public static implicit operator Serie(SerieDbRecord serieDbRecord)
        {
            var exercise = new ExerciseDefinition(serieDbRecord.DefinitionExerciseId);
            exercise.Name = serieDbRecord.DefinitionExerciseName;
            exercise.Type = serieDbRecord.DefinitionExerciseType;

            Serie result = new Serie(serieDbRecord.Id, exercise);
            result.Rest = serieDbRecord.Rest;
            result.Reps = serieDbRecord.Reps;
            return result;
        }

        public static implicit operator SerieDbRecord(Serie serie)
        {
            var result = new SerieDbRecord();
            result.DefinitionExerciseId = serie.ExerciseDefinition.Id;
            result.Rest = serie.Rest;
            result.Reps = serie.Reps;
            return result;
        }
    }
}