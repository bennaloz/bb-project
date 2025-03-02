using bb_project.app.Contracts.Models.Data;

namespace bb_project.app.DataAccess.Models
{
    public class WorkoutDbRecord
    {
        public ulong Id { get; }

        public string Name { get; set; } = "";

        public int Order { get; set; }


        public static implicit operator Workout(WorkoutDbRecord workoutDbRecord)
        {
            return new Workout(workoutDbRecord.Id)
            {
                Name = workoutDbRecord.Name,
                Order = workoutDbRecord.Order
            };
        }
    }
}
