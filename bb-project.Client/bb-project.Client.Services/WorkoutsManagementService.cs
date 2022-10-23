using bb_project.Infrastructure.Models.Data;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Client.Services
{
    public class WorkoutsManagementService : IWorkoutsManagementService
    {
        private readonly RestClient serviceClient;

        public WorkoutsManagementService(string serviceURL)
        {
            this.serviceClient = new RestClient(serviceURL);
        }

        public async Task<IEnumerable<Workout>> GetActiveWorkoutsAsync()
        {
            try
            {
                RestRequest request = new RestRequest("getActiveWO");
                var response = await this.serviceClient.GetAsync(request);
                if (response.IsSuccessful)
                    return JsonConvert.DeserializeObject<Workout[]>(response.Content);

                return Enumerable.Empty<Workout>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<Workout> GetNextWorkoutAsync(string userId, ulong activeWorkoutPlanId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, ulong? workoutPlanId = null, ulong? workoutId = null, DateTime from = default, DateTime to = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(ulong? id = null)
        {
            try
            {
                RestRequest request = new RestRequest("getWOPlans");
                if (id.HasValue)
                    request.AddQueryParameter("workoutPlanId", id.ToString());
                var response = await this.serviceClient.GetAsync(request);
                if (response.IsSuccessful)
                    return JsonConvert.DeserializeObject<WorkoutPlan[]>(response.Content);

                return Enumerable.Empty<WorkoutPlan>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<IEnumerable<Workout>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SeriesGroup>> GetWorkoutSeriesGroupsAsync(ulong workoutId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> HasActiveWorkoutPlanAsync()
        {
            try
            {
                RestRequest request = new RestRequest("hasActiveWO");
                var response = await this.serviceClient.GetAsync(request);
                if (response.IsSuccessful)
                    return bool.Parse(response.Content);

                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<ulong> InsertExerciseDefinitionAsync(ExerciseDefinition exercise)
        {
            throw new NotImplementedException();
        }

        public Task InsertExercisesGroupsAsync(ulong workoutId, IEnumerable<SeriesGroup> seriesGroups)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, string workoutName)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> InsertWorkoutPlanAsync(string workoutPlanName, ulong id, bool isActive = false)
        {
            throw new NotImplementedException();
        }
    }
}
