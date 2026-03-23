namespace bb_project.API.Models
{
    public class UpdateWorkoutDTO
    {
        public ulong WorkoutPlanId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Order { get; set; }
    }
}
