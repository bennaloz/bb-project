using bb_project.DAL.Models;
using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.DAL
{
    public class WorkoutsMockDataStore : IWorkoutsDataStore
    {

        IEnumerable<Workout> workouts;
        IEnumerable<Serie> series;
        IEnumerable<WorkoutPlan> plans;
        public WorkoutsMockDataStore()
        {
            workouts = new List<Workout>()
            {
                new Workout(1){Name = "Scheda A"},
                new Workout(2){Name = "Scheda B"},
                new Workout(3){Name = "Scheda C"},
                new Workout(4){Name = "Scheda D"},
            };

            series = new List<Serie>()
            {
                new Serie() { ExerciseMethod = ExerciseMethodology.Single , OwnerExercise = new Exercise(1) { Name = "SQUAT", Type= ExerciseType.Weights}, Reps= 8, Rest=120 },
                new Serie() { ExerciseMethod = ExerciseMethodology.Single , OwnerExercise = new Exercise(1) { Name = "SQUAT", Type= ExerciseType.Weights}, Reps= 8, Rest=120 },
                new Serie() { ExerciseMethod = ExerciseMethodology.Single , OwnerExercise = new Exercise(1) { Name = "SQUAT", Type= ExerciseType.Weights}, Reps= 8, Rest=120 }
            };

            plans = new List<WorkoutPlan>()
            {
                new WorkoutPlan(){ Name = "MASSA", IsActive = false },
                new WorkoutPlan(){ Name = "DEFINIZIONE", IsActive = true},
                new WorkoutPlan(){ Name = "FORZA", IsActive = false}
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

        public async Task InsertWorkoutPlanAsync(WorkoutPlan workoutPlan)
        {
            throw new NotImplementedException();
        }

        public async Task InsertWorkoutSeriesAsync(IEnumerable<Serie> series)
        {
            throw new NotImplementedException();
        }
    }
}