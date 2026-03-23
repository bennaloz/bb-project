namespace bb_project.API.Models
{
    public class UpdateWorkoutPlanDTO
    {
        public string UserId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsArchived { get; set; }
    }
}
