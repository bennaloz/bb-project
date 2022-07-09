
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Infrastructure.Models.Data
{
    public class Exercise
    {
        public long ID { get; internal set; }

        public string Name { get; set; }

        public ExerciseType Type { get; set; }

    }
}