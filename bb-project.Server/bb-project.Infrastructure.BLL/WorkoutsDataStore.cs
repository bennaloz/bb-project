using bb_project.DAL;
using bb_project.Infrastructure.DAL.Models;
using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Infrastructure.BLL
{
    public class WorkoutsDataStore : IWorkoutsDataStore
    {
        private readonly WorkoutsDbManager dbManager = new WorkoutsDbManager("Server=bb-project.database.windows.net;Database=bb-project-test;User Id=bb-project-admin-test;Password=vHETYV9W4Z5sDGGBGn;");

        public async Task<IEnumerable<Workout>> GetActiveWorkoutsAsync()
        {
            return (await this.dbManager.GetActiveWorkoutsAsync()).Cast<Workout>();
        }

        public async Task<IEnumerable<ExerciseDefinition>> GetExerciseDefinitionsAsync(string userId)
        {
            var exercises = await this.dbManager.GetExercisesAsync(userId);
            return exercises.Select(e => (ExerciseDefinition)e);
        }

        public async Task<Workout> GetNextWorkoutAsync(string userId, ulong activeWorkoutPlanId)
        {
            var activeWorkouts = await this.GetActiveWorkoutsAsync();
            var userWorkoutsHistory = await this.GetWorkoutHistoryItems(userId, workoutPlanId: activeWorkoutPlanId, from: DateTime.Now.AddDays(-14));
            var previousWorkoutId = (userWorkoutsHistory?.Count() ?? 0) > 0 ? userWorkoutsHistory.OrderBy(woh => woh.StartDate).Last().WorkoutId
                                                                        : 0;
            var nextWorkout = default(Workout);
            var previousWorkoutIndex = activeWorkouts.First(wo => wo.Id == previousWorkoutId).Order;
            var lastWorkoutIndex = activeWorkouts.OrderBy(wo => wo.Order).Last().Order;
            if (previousWorkoutIndex == lastWorkoutIndex)
                nextWorkout = activeWorkouts.OrderBy(wo => wo.Order).First();
            else
                nextWorkout = activeWorkouts.Where(wo => wo.Equals(previousWorkoutIndex + 1)).First();
            return nextWorkout;
        }

        public async Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, ulong? workoutId = null, ulong? workoutPlanId = default, DateTime from = default, DateTime to = default)
        {
            return (await this.dbManager.GetWorkoutHistoryAsync(userId, workoutId, workoutPlanId, from, to)).Cast<WorkoutHistoryItem>();
        }

        public async Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(ulong? id = null, bool getArchived = false)
        {
            var workoutPlansDb = await this.dbManager.GetWorkoutPlansAsync(id);

            return workoutPlansDb.Select(wpdb=> (WorkoutPlan) wpdb);
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null)
        {
            var workouts = await this.dbManager.GetWorkoutsAsync(workoutPlanId);

            return workouts.Select(w=>(Workout)w);
        }

        public async Task<IEnumerable<ExerciseGroup>> GetWorkoutExercisesAsync(ulong workoutId, string userId, ulong? seriesGroupId = null)
        {
            var seriesRecords = await this.dbManager.GetWorkoutSeriesGroupsAsync(workoutId, userId, seriesGroupId);

            var groups = new Dictionary<ulong, ExerciseGroup>();
            foreach (var record in seriesRecords)
            {
                if (!groups.ContainsKey(record.SeriesGroupId))
                    groups.Add(record.SeriesGroupId, new ExerciseGroup(record.SeriesGroupId, record.ExerciseMethod));

                if (!groups[record.SeriesGroupId].Exercises.ContainsKey(record.DefinitionExerciseId))
                {
                    groups[record.SeriesGroupId].Exercises.Add(record.DefinitionExerciseId, new Exercise(record.DefinitionExerciseId, record.DefinitionExerciseName));
                }
                groups[record.SeriesGroupId].Exercises[record.DefinitionExerciseId].Series.Add((Serie)record);
            }

            return groups.Values;
        }

        public async Task<bool?> HasActiveWorkoutPlanAsync()
            => await this.dbManager.HasActiveWorkoutPlanAsync();

        public async Task<ulong> InsertExerciseDefinitionAsync(string userId, ExerciseDefinition exerciseDefinition)
        {
            return await this.dbManager.InsertExerciseAsync(userId, (ExerciseDbRecord)exerciseDefinition);
        }

        public async Task InsertSeriesGroupsAsync(ulong workoutId, IEnumerable<ExerciseGroup> exercisesGroups)
        {
            foreach (var seriesGroup in exercisesGroups)
            {
                var seriesGroupId = await this.dbManager.InsertWorkoutSeriesGroupAsync(seriesGroup.ExerciseMethod);
                await this.dbManager.InsertWorkoutSeriesAsync(workoutId, seriesGroup.Exercises.Values.SelectMany(v=>v.Series).Select(s =>
                {
                    var dbRecord = (SerieDbRecord)s;
                    dbRecord.SeriesGroupId = seriesGroupId;
                    dbRecord.WorkoutId = workoutId;
                    return dbRecord;
                }));
            }
        }

        public async Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, string workoutName, int order)
        {
            return await this.dbManager.InsertWorkoutAsync(workoutPlanId, new WorkoutDbRecord
            {
                Name = workoutName,
                Order = order
            });
        }

        public async Task InsertWorkoutDataAsync(ulong workoutHistoryId, ulong serieId, ulong exerciseId, DateTime startTime, DateTime endTime, double? usedKgs)
        {
            await this.dbManager.InsertWorkoutDataAsync(workoutHistoryId, serieId, exerciseId, startTime, endTime, usedKgs);
        }

        public async Task<ulong> InsertWorkoutHistoryAsync(DateTime startDate, DateTime endDate, ulong workoutId, ulong workoutPlanId, string userId)
        {
            return await this.dbManager.InsertWorkoutHistoryAsync(startDate, endDate, workoutId, workoutPlanId, userId);
        }

        public async Task<ulong> InsertWorkoutPlanAsync(string userId, string workoutPlanName, bool isActive = false)
        {
            return await this.dbManager.InsertWorkoutPlanAsync(userId, new WorkoutPlanDbRecord
            {
                Name = workoutPlanName,
                IsActive = isActive
            });
        }
    }
}
