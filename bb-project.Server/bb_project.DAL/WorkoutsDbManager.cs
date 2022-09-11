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
using bb_project.Infrastructure.Models.Enums;

namespace bb_project.DAL
{
    public class WorkoutsDbManager
    {
        private readonly string connectionString = "";

        public WorkoutsDbManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<WorkoutHistoryDbRecord>> GetWorkoutHistoryAsync(string userId, ulong? workoutId = null, ulong? workoutPlanId = null, DateTime from = default, DateTime to = default)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("workoutId", workoutId);
            parameters.Add("workoutPlanId", workoutPlanId);
            parameters.Add("from", from == default ? DateTime.MinValue : from);
            parameters.Add("to", to == default ? DateTime.MaxValue : to);
            return await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync<WorkoutHistoryDbRecord>("spr_GetWorkoutHistory", parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<ulong> InsertWorkoutHistoryAsync(DateTime startDate, DateTime endDate, ulong workoutId, ulong workoutPlanId, string userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);
            parameters.Add("fkWorkoutId", workoutId);
            parameters.Add("fkWorkoutPlanId", workoutPlanId);
            parameters.Add("fkUserId", userId);
            parameters.Add("workoutHistoryId", dbType: DbType.Int64, direction: ParameterDirection.Output);
            await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutHistory", parameters, commandType: CommandType.StoredProcedure));
            return parameters.Get<ulong>("workoutHistoryId");
        }

        public async Task InsertWorkoutDataAsync(ulong workoutHistoryId, ulong serieId, DateTime startTime, DateTime endTime, double? usedKgs)
        {
                var parameters = new DynamicParameters();
                parameters.Add("workoutHistoryId", workoutHistoryId);
                parameters.Add("serieId", serieId);
                parameters.Add("startTime", startTime);
                parameters.Add("endTime", endTime);
                parameters.Add("usedKgs", usedKgs);
                await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutData", parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<IEnumerable<ExerciseDbRecord>> GetExercisesAsync(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            return await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync<ExerciseDbRecord>("spr_GetExercises", param: parameters, commandType: CommandType.StoredProcedure));
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
            using (var conn = new SqlConnection(this.connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<WorkoutDbRecord>("spr_GetActiveWorkouts", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<WorkoutPlanDbRecord>> GetWorkoutPlansAsync(ulong? id = null)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", id ?? 0);
                var workoutPlans = await conn.QueryAsync<WorkoutPlanDbRecord>("spr_GetWorkoutPlans", parameters, commandType: CommandType.StoredProcedure);
                return workoutPlans;
            }
        }

        public async Task<IEnumerable<WorkoutDbRecord>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", workoutPlanId);
                parameters.Add("workoutId", workoutId ?? 0);
                var workouts = await conn.QueryAsync<WorkoutDbRecord>("spr_GetWorkouts", parameters, commandType: CommandType.StoredProcedure);
                return workouts.Cast<WorkoutDbRecord>();
            }
        }

        public async Task<IEnumerable<SerieDbRecord>> GetWorkoutSeriesGroupsAsync(ulong workoutId, string userId, ulong? seriesGroupId = null)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("userId", userId);
                parameters.Add("workoutId", workoutId);
                parameters.Add("seriesGroupId", seriesGroupId);
                var workouts = await conn.QueryAsync<SerieDbRecord>("spr_GetWorkoutSeriesGroup", parameters, commandType: CommandType.StoredProcedure);
                return workouts.Cast<SerieDbRecord>();
            }

        }

        public async Task<ulong> InsertWorkoutPlanAsync(string userId, WorkoutPlanDbRecord workoutPlan)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanName", workoutPlan.Name);
                parameters.Add("isActive", workoutPlan.IsActive);
                parameters.Add("userId", userId);
                parameters.Add("workoutPlanId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                await conn.QueryAsync("spw_InsertWorkoutPlan", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<ulong>("workoutPlanId");
            }
        }

        public async Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, WorkoutDbRecord workout)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", workoutPlanId);
                parameters.Add("workoutName", workout.Name);
                parameters.Add("order", workout.Order);
                parameters.Add("workoutId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                await conn.QueryAsync("spw_InsertWorkout", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<ulong>("workoutId");
            }
        }

        public async Task<ulong> InsertWorkoutSeriesGroupAsync(ExerciseMethodology exerciseMethod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("exerciseMethod", (int)exerciseMethod, DbType.Int32);
            parameters.Add("seriesGroupId", dbType: DbType.Int64, direction: ParameterDirection.Output);
            await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutSeriesGroup", parameters, commandType: CommandType.StoredProcedure));
            return parameters.Get<ulong>("seriesGroupId");
        }

        public async Task InsertWorkoutSeriesAsync(ulong workoutId, IEnumerable<SerieDbRecord> series)
        {
            foreach (var serie in series)
            {
                var parameters = new DynamicParameters();
                parameters.Add("reps", serie.Reps);
                parameters.Add("rest", serie.Rest);
                parameters.Add("workoutId", workoutId);
                parameters.Add("definitionExerciseId", serie.DefinitionExerciseId);
                parameters.Add("seriesGroupId", serie.SeriesGroupId);
                parameters.Add("serieId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutSeries", parameters, commandType: CommandType.StoredProcedure));
                serie.Id = parameters.Get<ulong>("serieId");
            }
            //using (var conn = new SqlConnection(this.connectionString))
            //{
            //    DataTable tbl = new DataTable();
            //    tbl.Columns.Add(new DataColumn("Reps", typeof(int)));
            //    tbl.Columns.Add(new DataColumn("Rest", typeof(int)));
            //    tbl.Columns.Add(new DataColumn("fk_WorkoutId", typeof(ulong)));
            //    tbl.Columns.Add(new DataColumn("fk_ExerciseId", typeof(ulong)));

            //    var objbulk = new SqlBulkCopy(conn);
            //    objbulk.DestinationTableName = "tbl_Serie";
            //    objbulk.ColumnMappings.Add("Reps", "Reps");
            //    objbulk.ColumnMappings.Add("Rest", "Rest");
            //    objbulk.ColumnMappings.Add("fk_WorkoutId", "fk_WorkoutId");
            //    objbulk.ColumnMappings.Add("fk_ExerciseId", "fk_ExerciseId");

            //    foreach (var serie in series)
            //    {
            //        DataRow dr = tbl.NewRow();
            //        dr["Reps"] = serie.Reps;
            //        dr["Rest"] = serie.Rest;
            //        dr["fk_WorkoutId"] = serie.WorkoutId;
            //        dr["fk_ExerciseId"] = serie.OwnerExerciseId;

            //        tbl.Rows.Add(dr);
            //    }

            //    objbulk.WriteToServer(tbl);
            //}
        }

        public async Task<ulong> InsertExerciseAsync(string userId, ExerciseDbRecord exercise)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("name", exercise.Name, dbType: DbType.String);
                parameters.Add("type", exercise.Type, dbType: DbType.Int16);
                parameters.Add("involvedMuscles", exercise.InvolvedMuscles, dbType: DbType.Int16);
                parameters.Add("userId", userId);
                parameters.Add("exerciseId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                await conn.QueryAsync("spw_InsertExercise", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<ulong>("@exerciseId");
            }
        }
    }
}
