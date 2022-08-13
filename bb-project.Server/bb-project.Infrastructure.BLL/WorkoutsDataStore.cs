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

        public async Task<Workout> GetNextWorkoutAsync(string userId, long activeWorkoutPlanId)
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

        public async Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, long? workoutId = null, long? workoutPlanId = default, DateTime from = default, DateTime to = default)
        {
            return (await this.dbManager.GetWorkoutHistoryAsync(userId, workoutId, workoutPlanId, from, to)).Cast<WorkoutHistoryItem>();
        }

        public async Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null)
        {
            var workoutPlansDb = await this.dbManager.GetWorkoutPlansAsync(id);

            return workoutPlansDb.Select(wpdb=> (WorkoutPlan) wpdb);
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            var workouts = await this.dbManager.GetWorkoutsAsync(workoutPlanId);

            return workouts.Select(w=>(Workout)w);
        }

        public async Task<IEnumerable<Serie>> GetWorkoutSeriesGroupsAsync(long workoutId, string userId, long? seriesGroupId = null)
        {
            var workouts = await this.dbManager.GetWorkoutSeriesGroupsAsync(workoutId, userId, seriesGroupId);

            return workouts.Cast<Serie>();
        }

        public async Task<bool?> HasActiveWorkoutPlanAsync()
            => await this.dbManager.HasActiveWorkoutPlanAsync();

        public async Task<long> InsertExerciseDefinitionAsync(string userId, ExerciseDefinition exerciseDefinition)
        {
            return await this.dbManager.InsertExerciseAsync(userId, new ExerciseDbRecord
            {
                Name = exerciseDefinition.Name,
                Type = exerciseDefinition.Type
            });
        }

        public async Task InsertSeriesGroupsAsync(long workoutId, IEnumerable<SeriesGroup> seriesGroups)
        {
            foreach (var seriesGroup in seriesGroups)
            {
                var seriesGroupId = await this.dbManager.InsertWorkoutSeriesGroupAsync(seriesGroup.ExerciseMethod);
                await this.dbManager.InsertWorkoutSeriesAsync(workoutId, seriesGroup.Series.Select(s =>
                {
                    var dbRecord = (SerieDbRecord)s;
                    dbRecord.SeriesGroupId = seriesGroupId;
                    dbRecord.WorkoutId = workoutId;
                    return dbRecord;
                }));
            }
        }

        public async Task<long> InsertWorkoutAsync(long workoutPlanId, string workoutName, int order)
        {
            return await this.dbManager.InsertWorkoutAsync(workoutPlanId, new WorkoutDbRecord
            {
                Name = workoutName,
                Order = order
            });
        }

        public async Task<long> InsertWorkoutPlanAsync(string userId, string workoutPlanName, bool isActive = false)
        {
            return await this.dbManager.InsertWorkoutPlanAsync(userId, new WorkoutPlanDbRecord
            {
                Name = workoutPlanName,
                IsActive = isActive
            });
        }

        public async Task InsertWorkoutSeriesAsync(long workoutId, IEnumerable<Serie> series)
        {
            await this.dbManager.InsertWorkoutSeriesAsync(workoutId, series.Select(s =>
            {
                var sDb = (SerieDbRecord)s;
                sDb.WorkoutId = workoutId;
                return sDb;
            }));
        }

        Task<IEnumerable<SeriesGroup>> IWorkoutsDataStore.GetWorkoutSeriesGroupsAsync(long workoutId, string userId, long? seriesGroupId)
        {
            throw new NotImplementedException();
        }
    }
}
