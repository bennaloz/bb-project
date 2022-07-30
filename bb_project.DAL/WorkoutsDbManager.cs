using bb_project.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using bb_project.Infrastructure.DAL.Models;

namespace bb_project.DAL
{
    public class WorkoutsDbManager
    {
        private string connectionString = "";

        public WorkoutsDbManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<WorkoutHistoryDbRecord>> GetWorkoutHistoryAsync(string userId, long? workoutId = null, DateTime from = default, DateTime to = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("workoutId", workoutId);
            parameters.Add("from", from == default ? DateTime.MinValue : from);
            parameters.Add("to", to == default ? DateTime.MaxValue : to);
            return await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync<WorkoutHistoryDbRecord>("spr_GetWorkoutHistory", parameters));
        }

        public async Task<bool> HasActiveWorkoutPlanAsync()
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
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", id ?? 0);
                var workoutPlans = await conn.QueryAsync<IEnumerable<WorkoutPlanDbRecord>>("spr_GetWorkoutPlans", parameters);
                return workoutPlans.Cast<WorkoutPlanDbRecord>();
            }
        }

        public async Task<IEnumerable<WorkoutDbRecord>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", workoutPlanId);
                parameters.Add("workoutId", workoutId ?? 0);
                var workouts = await conn.QueryAsync<IEnumerable<WorkoutDbRecord>>("spr_GetWorkouts", parameters);
                return workouts.Cast<WorkoutDbRecord>();
            }
        }

        public async Task<IEnumerable<SerieDbRecord>> GetWorkoutSeriesAsync(long workoutId, string userId)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("userId", userId);
                parameters.Add("workoutId", workoutId);
                var workouts = await conn.QueryAsync<IEnumerable<WorkoutDbRecord>>("spr_GetWorkoutSerie", parameters);
                return workouts.Cast<SerieDbRecord>();
            }
            
        }

        public async Task<long> InsertWorkoutPlanAsync(WorkoutPlanDbRecord workoutPlan)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanName", workoutPlan.Name);
                parameters.Add("isActive", workoutPlan.IsActive);
                parameters.Add("workoutPlanId", ParameterDirection.Output);
                var workouts = await conn.QueryAsync<IEnumerable<WorkoutDbRecord>>("spw_InsertWorkoutPlan", parameters);
                return parameters.Get<long>("workoutId");
            }
        }

        public async Task<long> InsertWorkoutAsync(long workoutPlanId, WorkoutDbRecord workout)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", workoutPlanId);
                parameters.Add("workoutName", workout.Name);
                parameters.Add("workoutId", ParameterDirection.Output);
                var workouts = await conn.QueryAsync<IEnumerable<WorkoutDbRecord>>("spw_InsertWorkout", parameters);
                return parameters.Get<long>("workoutId");
            }
        }

        public async Task InsertWorkoutSeriesAsync(long workoutId, IEnumerable<SerieDbRecord> series)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DataTable tbl = new DataTable();
                tbl.Columns.Add(new DataColumn("Reps", typeof(int)));
                tbl.Columns.Add(new DataColumn("Rest", typeof(int)));
                tbl.Columns.Add(new DataColumn("fk_WorkoutId", typeof(long)));
                tbl.Columns.Add(new DataColumn("fk_ExerciseId", typeof(long)));

                var objbulk = new SqlBulkCopy(conn);
                objbulk.DestinationTableName = "tbl_Serie";
                objbulk.ColumnMappings.Add("Reps", "Reps");
                objbulk.ColumnMappings.Add("Rest", "Rest");
                objbulk.ColumnMappings.Add("fk_WorkoutId", "fk_WorkoutId");
                objbulk.ColumnMappings.Add("fk_ExerciseId", "fk_ExerciseId");

                foreach (var serie in series)
                {
                    DataRow dr = tbl.NewRow();
                    dr["Reps"] = serie.Reps;
                    dr["Rest"] = serie.Rest;
                    dr["fk_WorkoutId"] = serie.WorkoutId;
                    dr["fk_ExerciseId"] = serie.OwnerExerciseId;

                    tbl.Rows.Add(dr);
                }

                objbulk.WriteToServer(tbl);
            }
        }

        public async Task<long> InsertExerciseAsync(ExerciseDbRecord exercise)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("name", exercise.Name);
                parameters.Add("type", exercise.Type);
                parameters.Add("exercsiseId", ParameterDirection.Output);
                var workouts = await conn.QueryAsync<IEnumerable<WorkoutDbRecord>>("spw_InsertExercise", parameters);
                return parameters.Get<long>("exerciseId");
            }
        }
    }
}
