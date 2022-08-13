
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Infrastructure.Models.Data
{
    public class ExerciseDefinition
    {
        public long ID { get; }

        public string Name { get; set; }

        public ExerciseType Type { get; set; }

        public ExerciseDefinition(long id)
        {
            this.ID = id;
        }

    }
}