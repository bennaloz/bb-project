
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.Infrastructure.Models.Data
{
    public class Exercise
    {
        public Exercise(long id)
        {
            this.ID = id;
        }
        public long ID { get;}

        public string Name { get; set; }

        public ExerciseType Type { get; set; }

    }
}