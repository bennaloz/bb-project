using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.Client.Services
{
    public interface IWorkoutsManagementService
    {
        Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, ulong? workoutPlanId = null, ulong? workoutId = null, DateTime from = default, DateTime to = default);

        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(ulong? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null);

        Task<IEnumerable<SeriesGroup>> GetWorkoutSeriesGroupsAsync(ulong workoutId, string userId);

        Task<ulong> InsertWorkoutPlanAsync(string workoutPlanName,ulong id , bool isActive = false);

        Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, string workoutName);

        Task InsertSeriesGroupsAsync(ulong workoutId, IEnumerable<SeriesGroup> seriesGroups);

        Task<IEnumerable<Workout>> GetActiveWorkoutsAsync();

        Task<Workout> GetNextWorkoutAsync(string userId, ulong activeWorkoutPlanId);

        Task<ulong> InsertExerciseDefinitionAsync(ExerciseDefinition exercise);
        
        Task<bool?> HasActiveWorkoutPlanAsync();
    }
}