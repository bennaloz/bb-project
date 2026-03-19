using bb_project.app.Contracts.Models.Data;
using bb_project.app.Contracts.Models.Enums;

namespace bb_project.app.DataAccess.Models
{
    public class ExerciseDbRecord
    {
        public ulong Id { get; internal set; }

        public string Name { get; set; } = "";

        public ExerciseType Type { get; set; }

        public InvolvedMuscles InvolvedMuscles { get; set; }

        public static implicit operator ExerciseDbRecord(ExerciseDefinition exercise)
        {
            var result = new ExerciseDbRecord();
            result.Type = exercise.Type;
            result.Name = exercise.Name;
            result.InvolvedMuscles = exercise.InvolvedMuscles;
            return result;

        }

        public static implicit operator ExerciseDefinition(ExerciseDbRecord exercise)
        {
            var result = new ExerciseDefinition(exercise.Id);
            result.Type = exercise.Type;
            result.Name = exercise.Name;
            result.InvolvedMuscles = exercise.InvolvedMuscles;
            return result;
        }

    }
}