using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.Client.Services
{
    public interface IWorkoutsDataStore
    {
        Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, long? workoutPlanId = null, long? workoutId = null, DateTime from = default, DateTime to = default);

        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null);

        Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId, string userId);

        Task<long> InsertWorkoutPlanAsync(string workoutPlanName, bool isActive = false);

        Task<long> InsertWorkoutAsync(long workoutPlanId, string workoutName);

        Task InsertWorkoutSeriesAsync(long workoutId, IEnumerable<Serie> series);

        Task<IEnumerable<Workout>> GetActiveWorkoutsAsync();

        Task<Workout> GetNextWorkoutAsync(string userId, long activeWorkoutPlanId);

        Task<long> InsertExerciseAsync(Exercise exercise);
        
        Task<bool?> HasActiveWorkoutPlanAsync();
    }
}