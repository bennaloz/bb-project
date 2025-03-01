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

namespace bb_project.app.DataAccess
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
            parameters.Add("fkWorkoutId", (long)workoutId);
            parameters.Add("fkWorkoutPlanId", (long)workoutPlanId);
            parameters.Add("fkUserId", userId);
            parameters.Add("workoutHistoryId", dbType: DbType.Int64, direction: ParameterDirection.Output);
            await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutHistory", parameters, commandType: CommandType.StoredProcedure));
            return (ulong)parameters.Get<long>("workoutHistoryId");
        }

        public async Task InsertWorkoutDataAsync(ulong workoutHistoryId, ulong serieId, ulong exerciseId, DateTime startTime, DateTime endTime, double? usedKgs)
        {
            var parameters = new DynamicParameters();
            parameters.Add("workoutHistoryId", (long)workoutHistoryId);
            parameters.Add("serieId", (long)serieId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("exerciseId", (long)exerciseId);
            parameters.Add("usedKgs", usedKgs);
            await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutData", parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<IEnumerable<ExerciseDbRecord>> GetExercisesAsync(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            return await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync<ExerciseDbRecord>("spr_GetExercisesDefinitions", param: parameters, commandType: CommandType.StoredProcedure));
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

        public async Task<IEnumerable<WorkoutPlanDbRecord>> GetWorkoutPlansAsync(ulong? id = null, bool getArchived = false)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", (long)(id ?? 0), dbType: DbType.Int64);
                parameters.Add("getArchived", getArchived);
                var workoutPlans = await conn.QueryAsync<WorkoutPlanDbRecord>("spr_GetWorkoutPlans", parameters, commandType: CommandType.StoredProcedure);
                return workoutPlans;
            }
        }

        public async Task<IEnumerable<WorkoutDbRecord>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", (long)workoutPlanId);
                parameters.Add("workoutId", (long)(workoutId ?? 0));
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
                parameters.Add("workoutId", (long)workoutId);
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
                return (ulong)parameters.Get<long>("workoutPlanId");
            }
        }

        public async Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, WorkoutDbRecord workout)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("workoutPlanId", (long)workoutPlanId);
                parameters.Add("workoutName", workout.Name);
                parameters.Add("order", workout.Order);
                parameters.Add("workoutId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                await conn.QueryAsync("spw_InsertWorkout", parameters, commandType: CommandType.StoredProcedure);
                return (ulong)parameters.Get<long>("workoutId");
            }
        }

        public async Task<ulong> InsertWorkoutSeriesGroupAsync(ExerciseMethodology exerciseMethod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("exerciseMethod", (int)exerciseMethod, DbType.Int32);
            parameters.Add("seriesGroupId", dbType: DbType.Int64, direction: ParameterDirection.Output);
            await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutSeriesGroup", parameters, commandType: CommandType.StoredProcedure));
            return (ulong)parameters.Get<long>("seriesGroupId");
        }

        public async Task InsertWorkoutSeriesAsync(ulong workoutId, IEnumerable<SerieDbRecord> series)
        {
            foreach (var serie in series)
            {
                var parameters = new DynamicParameters();
                parameters.Add("reps", serie.Reps);
                parameters.Add("rest", serie.Rest);
                parameters.Add("workoutId", (long)workoutId);
                parameters.Add("definitionExerciseId", (long)serie.DefinitionExerciseId);
                parameters.Add("seriesGroupId", (long)serie.SeriesGroupId);
                parameters.Add("serieId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                await ConnectionHelper.ConnectAsync(this.connectionString, c => c.QueryAsync("spw_InsertWorkoutSeries", parameters, commandType: CommandType.StoredProcedure));
                serie.Id = (ulong)parameters.Get<long>("serieId");
            }
        }

        public async Task<ulong> InsertExerciseDefinitionAsync(string userId, ExerciseDbRecord exercise)
        {
            using (var conn = new SqlConnection(this.connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("name", exercise.Name, dbType: DbType.String);
                parameters.Add("type", exercise.Type, dbType: DbType.Int16);
                parameters.Add("involvedMuscles", exercise.InvolvedMuscles, dbType: DbType.Int16);
                parameters.Add("userId", userId);
                parameters.Add("exerciseId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                await conn.QueryAsync("spw_InsertExerciseDefinition", parameters, commandType: CommandType.StoredProcedure);
                return (ulong)parameters.Get<long>("@exerciseId");
            }
        }

        public async Task<int> UpdateWorkoutPlanAsync(ulong workoutPlanId, string userId, string workoutPlanName, bool isActive, bool isArchived)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("workoutPlanId", (long)workoutPlanId);
            parameters.Add("userId", userId);
            parameters.Add("workoutPlanName", workoutPlanName);
            parameters.Add("isActive", isActive);
            parameters.Add("isArchived", isArchived);
            var updatedRecordsCount = await ConnectionHelper.ConnectAsync(this.connectionString, c => c.ExecuteAsync("spw_UpdateWorkoutPlan", param: parameters, commandType: CommandType.StoredProcedure));
            return updatedRecordsCount;
        }

        public async Task<int> UpdateWorkoutAsync(ulong workoutPlanId, ulong workoutId, string workoutName, int order)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("workoutPlanId", (long)workoutPlanId);
            parameters.Add("workoutId", (long)workoutId);
            parameters.Add("workoutName", workoutName);
            parameters.Add("order", order);
            var updatedRecordsCount = await ConnectionHelper.ConnectAsync(this.connectionString, c => c.ExecuteAsync("spw_UpdateWorkout", param: parameters, commandType: CommandType.StoredProcedure));
            return updatedRecordsCount;
        }

        public async Task<int> UpdateExerciseDefinitionAsync(ulong exerciseId, string name, int exerciseType, int involvedMuscles)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("exerciseId", (long)exerciseId);
            parameters.Add("name", name);
            parameters.Add("type", exerciseType);
            parameters.Add("involvedMuscles", involvedMuscles);
            var updatedRecordsCount = await ConnectionHelper.ConnectAsync(this.connectionString, c => c.ExecuteAsync("spw_UpdateExerciseDefinition", param: parameters, commandType: CommandType.StoredProcedure));
            return updatedRecordsCount;
        }

        public async Task<int> DeleteWorkoutSeriesAsync(ulong workoutPlanId, ulong workoutId, params ulong[] seriesIds)
        {
            var parameters = new DynamicParameters();
            parameters.Add("workoutPlanId", (long)workoutPlanId);
            parameters.Add("workoutId", (long)workoutId);
            parameters.Add("serieIds", this.createIdsTable(seriesIds));

            var updatedRecordsCount = await ConnectionHelper.ConnectAsync(this.connectionString, c => c.ExecuteAsync("spw_DeleteWorkoutSeries", param: parameters, commandType: CommandType.StoredProcedure));
            return updatedRecordsCount;
        }

        private DataTable createIdsTable(ulong[] ids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(ulong));

            foreach (ulong id in ids)
                dt.Rows.Add(id);

            return dt;
        }
    }
}
