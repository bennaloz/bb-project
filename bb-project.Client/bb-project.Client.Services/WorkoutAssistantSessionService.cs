using bb_project.Client.Data;
using bb_project.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Client.Services
{
    public class WorkoutAssistantSessionService : Interfaces.IWorkoutAssistantSessionService
    {
        private readonly IWorkoutAssistantSessionDataManagerService sessionDataManagerService;

        public WorkoutAssistantSessionService(IWorkoutAssistantSessionDataManagerService sessionDataManagerService)
        {
            //TODO: verificare esistenza database
            this.sessionDataManagerService = sessionDataManagerService;
        }

        public async Task<WorkoutSession> StartWorkoutAsync(ulong workoutId, ulong workoutPlanId, string userId)
        {
            var workoutSession = new WorkoutSession
            {
                UserId = userId,
                StartDate = DateTime.Now,
                WorkoutId = workoutId,
                WorkoutPlanId = workoutPlanId
            };
            await this.sessionDataManagerService.Database.SaveWorkoutSessionAsync(workoutSession);
            return workoutSession;
        }

        public async Task<WorkoutSession> EndWorkoutAsync()
        {
            var activeWorkoutSession = await this.sessionDataManagerService.Database.GetWorkoutSessionAsync();
            activeWorkoutSession.EndDate = DateTime.Now;
            await this.sessionDataManagerService.Database.SaveWorkoutSessionAsync(activeWorkoutSession);
            return activeWorkoutSession;
        }

        public async Task StartCardioAsync(ulong exerciseId)
        {
            var sessionExercise = new SessionExercise()
            {
                ExerciseId = exerciseId,
                Type = Infrastructure.Models.Enums.ExerciseType.Cardio,
                StartDate = DateTime.Now
            };
            await this.sessionDataManagerService.Database.SaveSessionExerciseAsync(sessionExercise);
        }

        public async Task StopCardioAsync(ulong exerciseId)
        {
            var activeCardioExercise = await this.sessionDataManagerService.Database.GetSessionExerciseAsync(exerciseId);
            if (activeCardioExercise == default)
                throw new ArgumentException($"There's not active cardio exercise with id '{exerciseId}'");
            activeCardioExercise.EndDate = DateTime.Now;
            await this.sessionDataManagerService.Database.SaveSessionExerciseAsync(activeCardioExercise);
        }

        public async Task StartWeightSerieAsync(ulong exerciseId, double? usedKgs)
        {
            var sessionExercise = new SessionExercise()
            {
                ExerciseId = exerciseId,
                Type = Infrastructure.Models.Enums.ExerciseType.Weights,
                UsedKgs = usedKgs,
                StartDate = DateTime.Now
            };
            await this.sessionDataManagerService.Database.SaveSessionExerciseAsync(sessionExercise);
        }

        public async Task StopWeightSerieAsync(ulong exerciseId)
        {
            var activeWeightsExercise = await this.sessionDataManagerService.Database.GetSessionExerciseAsync(exerciseId);
            if (activeWeightsExercise == default)
                throw new ArgumentException($"There's not active cardio exercise with id '{exerciseId}'");
            activeWeightsExercise.EndDate = DateTime.Now;
            await this.sessionDataManagerService.Database.SaveSessionExerciseAsync(activeWeightsExercise);
        }

        public async Task<WorkoutSession> GetActiveWorkoutSessionAsync()
            => await sessionDataManagerService.Database.GetWorkoutSessionAsync();
    }
}
