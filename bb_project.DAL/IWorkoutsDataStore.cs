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

namespace bb_project
{
    public interface IWorkoutsDataStore
    {
        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null);

        Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId);

        Task<Exercise> GetOwnerExerciseAsync(long exerciseId);

        Task<long> InsertWorkoutPlanAsync(WorkoutPlan workoutPlan);

        Task<long> InsertWorkoutAsync(long workoutPlanId, Workout workout);

        Task InsertWorkoutSeriesAsync(IEnumerable<Serie> series);

        Task<long> InsertExerciseAsync(Exercise exercise);
    }
}