using bb_project.DAL.Models;
using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.DAL
{
    public class WorkoutsDataStore : IWorkoutsDataStore
    {
        private readonly WorkoutsDbManager dbManager = new WorkoutsDbManager("Server=bb-project.database.windows.net;Database=bb-project-test;User Id=bb-project-admin-test;Password=vHETYV9W4Z5sDGGBGn;");

        public async Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null)
        {
            var workoutPlansDb = await this.dbManager.GetWorkoutPlansAsync(id);

            return workoutPlansDb.Cast<WorkoutPlan>();
        }

        public Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId)
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
