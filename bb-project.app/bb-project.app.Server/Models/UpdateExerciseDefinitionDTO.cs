using bb_project.app.Contracts.Models.Enums;

namespace bb_project.API.Models
{
    public class UpdateExerciseDefinitionDTO
    {
        public string Name { get; set; } = string.Empty;

        public ExerciseType Type { get; set; }

        public InvolvedMuscles InvolvedMuscles { get; set; }
    }
}
