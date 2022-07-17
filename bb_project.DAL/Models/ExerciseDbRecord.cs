using bb_project.Infrastructure.Models.Enums;

namespace bb_project.DAL.Models
{
    internal class ExerciseDbRecord
    {
        public long ID { get; internal set; }

        public string Name { get; set; }

        public ExerciseType Type { get; set; }

    }
}