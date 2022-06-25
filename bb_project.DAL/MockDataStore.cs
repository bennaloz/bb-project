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
        public Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(int? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Workout>> GetWorkoutsAsync(int workoutPlanId, int? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(int workoutId)
        {
            throw new NotImplementedException();
        }
    }
}