
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Infrastructure.Models.Data
{
    public class ExerciseDefinition
    {
        public long Id { get; }

        public string Name { get; set; }

        public ExerciseType Type { get; set; }

        public ExerciseDefinition()
        {

        }

        public ExerciseDefinition(long id)
        {
            this.Id = id;
        }

    }
}