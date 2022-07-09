using bb_project.DAL.Models;
using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.DAL
{
    public class WorkoutsMockDataStore : IWorkoutsDataStore
    {
        public Task<WorkoutPlan> GetActiveWorkoutPlanAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId)
        {
            throw new NotImplementedException();
        }

        public Task<bool?> HasActiveWorkoutPlanAsync()
        {
            throw new NotImplementedException();
        }

        public Task InsertExerciseAsync(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public Task InsertWorkoutAsync(long workoutPlanId, Workout workout)
        {
            throw new NotImplementedException();
        }

        public Task InsertWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            throw new NotImplementedException();
        }

        public Task InsertWorkoutSeriesAsync(IEnumerable<Serie> series)
        {
            throw new NotImplementedException();
        }
    }
}