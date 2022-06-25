using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using bb_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.DAL
{
    public class WorkoutsMockDataStore : IWorkoutsDataStore
    {
        public Task<Exercise> GetOwnerExerciseAsync(long exerciseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(int? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Workout>> GetWorkoutsAsync(int workoutPlanId, int? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(int workoutId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId)
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertExerciseAsync(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertWorkoutAsync(long workoutPlanId, Workout workout)
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            throw new NotImplementedException();
        }

        public Task InsertWorkoutSeriesAsync(IEnumerable<Serie> series)
        {
            throw new NotImplementedException();
        }
    }
}