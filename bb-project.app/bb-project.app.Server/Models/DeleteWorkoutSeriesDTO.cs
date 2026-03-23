namespace bb_project.API.Models
{
    public class DeleteWorkoutSeriesDTO
    {
        public ulong WorkoutPlanId { get; set; }

        public ulong WorkoutId { get; set; }

        public ulong[] SeriesIds { get; set; } = Array.Empty<ulong>();
    }
}
