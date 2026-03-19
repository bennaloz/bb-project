using bb_project.app.Contracts.Models.Data;

namespace bb_project.app.DataAccess.Models
{
    public class WorkoutHistoryDbRecord
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ulong WorkoutId { get; set; }

        public string UserId { get; set; } = "";

        public static implicit operator WorkoutHistoryItem(WorkoutHistoryDbRecord historyDbRecord)
        {
            return new WorkoutHistoryItem
            {
                UserId = historyDbRecord.UserId,
                WorkoutId = historyDbRecord.WorkoutId,
                StartDate = historyDbRecord.StartDate,
                EndDate = historyDbRecord.EndDate
            };
        }
    }
}
