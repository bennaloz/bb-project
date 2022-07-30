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

        public async Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, long? workoutId = null, DateTime from = default, DateTime to = default)
        {
            return (await this.dbManager.GetWorkoutHistoryAsync(userId, workoutId, from, to)).Cast<WorkoutHistoryItem>();
        }

        public async Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null)
        {
            var workoutPlansDb = await this.dbManager.GetWorkoutPlansAsync(id);

            return workoutPlansDb.Cast<WorkoutPlan>();
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            var workouts = await this.dbManager.GetWorkoutsAsync(workoutPlanId);

            return workouts.Cast<Workout>();
        }

        public async Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId, string userId)
        {
            var workouts = await this.dbManager.GetWorkoutSeriesAsync(workoutId, userId);

            return workouts.Cast<Serie>();
        }

        public async Task<bool?> HasActiveWorkoutPlanAsync()
            => await this.dbManager.HasActiveWorkoutPlanAsync();

        public async Task<long> InsertExerciseAsync(Exercise exercise)
        {
            return await this.dbManager.InsertExerciseAsync(new ExerciseDbRecord
            {
                Name = exercise.Name,
                Type = exercise.Type
            });
        }

        public async Task<long> InsertWorkoutAsync(long workoutPlanId, string workoutName)
        {
            return await this.dbManager.InsertWorkoutAsync(workoutPlanId, new WorkoutDbRecord
            {
                Name = workoutName
            });
        }

        public async Task<long> InsertWorkoutPlanAsync(string workoutPlanName, bool isActive = false)
        {
            return await this.dbManager.InsertWorkoutPlanAsync(new WorkoutPlanDbRecord
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
    }
}
