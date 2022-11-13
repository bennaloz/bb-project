using bb_project.Client.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Client.Services.Interfaces
{
    public interface IWorkoutAssistantSessionService
    {
        Task<WorkoutSession> GetActiveWorkoutSessionAsync();

        Task<WorkoutSession> StartWorkoutAsync(ulong workoutId, ulong workoutPlanId, string userId);

        Task<WorkoutSession> EndWorkoutAsync();

        Task StartCardioAsync(ulong exerciseId);

        Task StopCardioAsync(ulong exerciseId);

        Task StartWeightSerieAsync(ulong exerciseId, double? usedKgs);

        Task StopWeightSerieAsync(ulong exerciseId);

    }
}
