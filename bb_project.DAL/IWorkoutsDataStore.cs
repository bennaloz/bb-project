using bb_project.Models;
using System.Collections.Generic;
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