using bb_project.Infrastructure.DAL.Models;
using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.Infrastructure.BLL
{
    public interface IWorkoutsDataStore
    {
        Task<IEnumerable<ExerciseDefinition>> GetExerciseDefinitionsAsync(string userId);

        Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, ulong? workoutPlanId = null, ulong? workoutId = null, DateTime from = default, DateTime to = default);
        Task<ulong> InsertWorkoutHistoryAsync(DateTime startDate, DateTime endDate, ulong workoutId, ulong workoutPlanId, string userId);
        Task InsertWorkoutDataAsync(ulong workoutHistoryId, ulong serieId, ulong exerciseId, DateTime startTime, DateTime endTime, double? usedKgs);

        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(ulong? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null);

        Task<IEnumerable<ExerciseGroup>> GetWorkoutExercisesAsync(ulong workoutId, string userId, ulong? seriesGroupId = null);

        Task<ulong> InsertWorkoutPlanAsync(string userId, string workoutPlanName, bool isActive = false);

        Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, string workoutName, int order);

        Task InsertSeriesGroupsAsync(ulong workoutId, IEnumerable<ExerciseGroup> seriesGroups);

        Task<IEnumerable<Workout>> GetActiveWorkoutsAsync();

        Task<Workout> GetNextWorkoutAsync(string userId, ulong activeWorkoutPlanId);

        Task<ulong> InsertExerciseDefinitionAsync(string userId, ExerciseDefinition exercise);

        Task<bool?> HasActiveWorkoutPlanAsync();
    }
}