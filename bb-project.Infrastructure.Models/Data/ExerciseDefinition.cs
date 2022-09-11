
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Infrastructure.Models.Data
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
            this.Id = id;
        }

    }
}