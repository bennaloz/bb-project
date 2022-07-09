using bb_project.DAL.Helpers;
using bb_project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace bb_project.DAL
{
    internal class WorkoutsDbManager
    {
        private string connectionString = "";

        public WorkoutsDbManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<bool> HasActiveWorkoutAsync()
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<bool>("spr_HasActiveWorkoutPlan");
            }
        }

        public async Task<IEnumerable<WorkoutDbRecord>> GetActiveWorkoutsAsync()
        {
            using(var conn = new SqlConnection(this.connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<WorkoutDbRecord>("spr_GetActiveWorkouts");
            }
        }

        public async Task<IEnumerable<WorkoutPlanDbRecord>> GetWorkoutPlansAsync(long? id = null)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                var workoutPlans = await conn.QueryAsync<IEnumerable<WorkoutPlanDbRecord>>("spr_GetWorkoutPlans");
                return workoutPlans.Cast<WorkoutPlanDbRecord>();
            }
        }

        public async Task<IEnumerable<WorkoutDbRecord>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            return default;
        }

        public async Task<IEnumerable<SerieDbRecord>> GetWorkoutSeriesAsync(long workoutId)
        {
            return default;
        }

        public async Task<long> InsertWorkoutPlanAsync(WorkoutPlanDbRecord workoutPlan)
        {
            return default;
        }

        public async Task<long> InsertWorkoutAsync(long workoutPlanId, WorkoutDbRecord workout)
        {
            return default;
        }

        public async Task InsertWorkoutSeriesAsync(IEnumerable<SerieDbRecord> series)
        {
        }

        public async Task<long> InsertExerciseAsync(ExerciseDbRecord exercise)
        {
            return default;
        }
    }
}
