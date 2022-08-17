using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.Infrastructure.BLL
{
    public interface IWorkoutsDataStore
    {
        Task<IEnumerable<ExerciseDefinition>> GetExerciseDefinitionsAsync(string userId);

        Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, long? workoutPlanId = null, long? workoutId = null, DateTime from = default, DateTime to = default);

        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null);

        Task<IEnumerable<SeriesGroup>> GetWorkoutSeriesGroupsAsync(long workoutId, string userId, long? seriesGroupId = null);

        Task<long> InsertWorkoutPlanAsync(string userId, string workoutPlanName, bool isActive = false);

        Task<long> InsertWorkoutAsync(long workoutPlanId, string workoutName, int order);

        Task InsertSeriesGroupsAsync(long workoutId, IEnumerable<SeriesGroup> seriesGroups);

        Task<IEnumerable<Workout>> GetActiveWorkoutsAsync();

        Task<Workout> GetNextWorkoutAsync(string userId, long activeWorkoutPlanId);

        Task<long> InsertExerciseDefinitionAsync(string userId, ExerciseDefinition exercise);

        Task<bool?> HasActiveWorkoutPlanAsync();
    }
}