using bb_project.DAL.Models;
using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace bb_project.Infrastructure.BLL
{
    public class WorkoutsMockDataStore : IWorkoutsDataStore
    {


        IEnumerable<Workout> workouts;
        IEnumerable<Serie> series;
        IEnumerable<WorkoutPlan> plans;
        public WorkoutsMockDataStore()
        {


            this.plans = new ObservableCollection<WorkoutPlan>()
            {
               new WorkoutPlan { Name ="MASSA", IsActive=true},
                new WorkoutPlan { Name ="DEFINIZIONE", IsActive=false},
                new WorkoutPlan { Name ="FORZA",IsActive =false}
            };

            this.workouts = new ObservableCollection<Workout>()
            {
                new Workout(1) { Name ="Scheda A"},
                new Workout(2) { Name ="Scheda B"},
                new Workout(3) { Name ="Scheda C"}
            };

            this.series = new ObservableCollection<Serie>()
            {
                new Serie(1,new Exercise(1){Name ="Panca Piana",Type= ExerciseType.Weights}),
                new Serie(2,new Exercise(1){Name ="Squat",Type= ExerciseType.Weights}),
                new Serie(1,new Exercise(1){Name ="Tapis Roulant",Type= ExerciseType.Cardio})
            };

        }


        public async Task<IEnumerable<Workout>> GetActiveWorkoutsAsync()
        {
            return await Task.FromResult(workouts);
        }

        public async Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null)
        {
            return await Task.FromResult(plans);
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId)
        {
            return await Task.FromResult(series);
        }

        public Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> HasActiveWorkoutPlanAsync()
        {
            return await Task.FromResult(true);
        }

        public async Task InsertExerciseAsync(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public async Task InsertWorkoutAsync(long workoutPlanId, Workout workout)
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertWorkoutAsync(long workoutPlanId, string workoutName)
        {
            throw new NotImplementedException();
        }

        public async Task InsertWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertWorkoutPlanAsync(string workoutPlanName, bool isActive = false)
        {
            throw new NotImplementedException();
        }

        public async Task InsertWorkoutSeriesAsync(IEnumerable<Serie> series)
        {
            throw new NotImplementedException();
        }

        public Task InsertWorkoutSeriesAsync(long workoutId, IEnumerable<Serie> series)
        {
            throw new NotImplementedException();
        }

        Task<long> IWorkoutsDataStore.InsertExerciseAsync(Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }
}