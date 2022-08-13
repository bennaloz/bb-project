using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.Client.Services
{
    public interface IWorkoutsManagementService
    {
        Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, long? workoutPlanId = null, long? workoutId = null, DateTime from = default, DateTime to = default);

        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null);

        Task<IEnumerable<SeriesGroup>> GetWorkoutSeriesGroupsAsync(long workoutId, string userId);

        Task<long> InsertWorkoutPlanAsync(string workoutPlanName, bool isActive = false);

        Task<long> InsertWorkoutAsync(long workoutPlanId, string workoutName);

        Task InsertSeriesGroupsAsync(long workoutId, IEnumerable<SeriesGroup> seriesGroups);

        Task<IEnumerable<Workout>> GetActiveWorkoutsAsync();

        Task<Workout> GetNextWorkoutAsync(string userId, long activeWorkoutPlanId);

        Task<long> InsertExerciseDefinitionAsync(ExerciseDefinition exercise);
        
        Task<bool?> HasActiveWorkoutPlanAsync();
    }
}