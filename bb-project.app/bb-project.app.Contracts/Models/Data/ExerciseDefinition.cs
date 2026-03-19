using bb_project.app.Contracts.Models.Enums;

namespace bb_project.app.Contracts.Models.Data
{
    public class ExerciseDefinition
    {
        public ulong Id { get; }

        public string Name { get; set; }

        public ExerciseType Type { get; set; }

        public InvolvedMuscles InvolvedMuscles { get; set; }

        public ExerciseDefinition()
        {

        }

        public ExerciseDefinition(ulong id)
        {
            Id = id;
        }

    }
}