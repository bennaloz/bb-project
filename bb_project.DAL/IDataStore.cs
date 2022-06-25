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
        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(int? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(int workoutPlanId, int? workoutId = null);

        Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(int workoutId);


    }
}