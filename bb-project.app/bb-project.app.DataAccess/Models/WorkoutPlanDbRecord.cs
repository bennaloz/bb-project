using bb_project.app.Contracts.Models.Data;

namespace bb_project.app.DataAccess.Models
{
    public class WorkoutPlanDbRecord
    {
        public ulong ID { get; }

        public string Name { get; set; } = "";

        public bool IsActive { get; set; }


        public static implicit operator WorkoutPlan(WorkoutPlanDbRecord dbRecord)
        {
            var result = new WorkoutPlan(dbRecord.ID);

            result.Name = dbRecord.Name;
            result.IsActive = dbRecord.IsActive;

            return result;
        }
    }
}
