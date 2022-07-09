using bb_project.DAL.Models;
using bb_project.Infrastructure.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bb_project.DAL
{
    public interface IWorkoutsDataStore
    {
        Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(long? id = null);

        Task<IEnumerable<Workout>> GetWorkoutsAsync(long workoutPlanId, long? workoutId = null);

        Task<IEnumerable<Serie>> GetWorkoutSeriesAsync(long workoutId);

        Task InsertWorkoutPlanAsync(WorkoutPlan workoutPlan);

        Task InsertWorkoutAsync(long workoutPlanId, Workout workout);

        Task InsertWorkoutSeriesAsync(IEnumerable<Serie> series);
        Task< IEnumerable<Workout>> GetActiveWorkoutsAsync();

        Task InsertExerciseAsync(Exercise exercise);
        
        Task<bool?> HasActiveWorkoutPlanAsync();
    }
}