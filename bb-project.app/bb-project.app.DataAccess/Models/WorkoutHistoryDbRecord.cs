using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.DAL.Models
{
    public class WorkoutHistoryDbRecord
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ulong WorkoutId { get; set; }

        public string UserId { get; set; }

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
