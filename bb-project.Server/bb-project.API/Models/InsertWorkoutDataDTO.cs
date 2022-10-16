namespace bb_project.API.Models
{
    public class InsertWorkoutDataDTO
    {
        public ulong WorkoutHistoryId { get; set; }

        public SeriesDataDTO[] SeriesData { get; set; }

        public class SeriesDataDTO
        {
            public ulong ExerciseId { get; set; }

            public ulong SerieId { get; set; }

            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }


            public double? UsedKgs { get; set; }
        }
    }
}
