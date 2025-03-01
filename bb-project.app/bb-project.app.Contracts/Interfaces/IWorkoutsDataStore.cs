using bb_project.app.Contracts.Models.Data;
using bb_project.app.Contracts.Models.Enums;
using bb_project.Infrastructure.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.app.Contracts.Interfaces
{
    public interface IWorkoutsDataStore
    {
        Task<IEnumerable<ExerciseDefinition>> GetExerciseDefinitionsAsync(string userId);

        Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, ulong? workoutPlanId = null, ulong? workoutId = null, DateTime from = default, DateTime to = default);
        Task<ulong> InsertWorkoutHistoryAsync(DateTime startDate, DateTime endDate, ulong workoutId, ulong workoutPlanId, string userId);
        Task InsertWorkoutDataAsync(ulong workoutHistoryId, ulong serieId, ulong exerciseId, DateTime startTime, DateTime endTime, double? usedKgs);

        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(ulong? id = null, bool getArchived = false);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null);

        Task<IEnumerable<ExerciseGroup>> GetWorkoutExercisesAsync(ulong workoutId, string userId, ulong? seriesGroupId = null);

        Task<ulong> InsertWorkoutPlanAsync(string userId, string workoutPlanName, bool isActive = false);

        Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, string workoutName, int order);

        Task InsertSeriesGroupsAsync(ulong workoutId, IEnumerable<ExerciseGroup> seriesGroups);

        Task<IEnumerable<Workout>> GetActiveWorkoutsAsync();

        Task<Workout> GetNextWorkoutAsync(string userId, ulong activeWorkoutPlanId);

        Task<ulong> InsertExerciseDefinitionAsync(string userId, ExerciseDefinition exercise);

        Task<bool?> HasActiveWorkoutPlanAsync();

        Task<int> UpdateWorkoutPlanAsync(ulong workoutPlanId, string userId, string workoutPlanName, bool isActive, bool isArchived);

        Task<int> UpdateWorkoutAsync(ulong workoutPlanId, ulong workoutId, string workoutName, int order);

        Task<int> UpdateExerciseDefinitionAsync(ulong exerciseId, string name, ExerciseType exerciseType, InvolvedMuscles involvedMuscles);

        Task<int> DeleteWorkoutSeriesAsync(ulong workoutPlanId, ulong workoutId, params ulong[] seriesIds);
    }
}