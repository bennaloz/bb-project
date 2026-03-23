using bb_project.app.Contracts.Interfaces;
using bb_project.app.Contracts.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bb_project.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutsEditorController : ControllerBase
    {
        private readonly ILogger<WorkoutsEditorController> _logger;
        private readonly IWorkoutsDataStore workoutsDataStore;

        public WorkoutsEditorController(ILogger<WorkoutsEditorController> logger,
            IWorkoutsDataStore workoutsDataStore)
        {
            _logger = logger;
            this.workoutsDataStore = workoutsDataStore;
        }

        [HttpGet("plans", Name = "getWOPlans")]
        public async Task<ActionResult> GetWorkoutsPlansAsync([FromQuery] ulong? workoutPlanId = null)
        {
            try
            {
                var workoutPlans = await this.workoutsDataStore.GetWorkoutPlansAsync(id:workoutPlanId);
                return new OkObjectResult(workoutPlans);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("workouts", Name = "getWorkouts")]
        public async Task<ActionResult> GetWorkoutsAsync([FromQuery] ulong workoutPlanId, [FromQuery] ulong? workoutId = null)
        {
            try
            {
                var workouts = await this.workoutsDataStore.GetWorkoutsAsync(workoutPlanId, workoutId);
                return new OkObjectResult(workouts);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("series-groups", Name = "getWOSeriesGroups")]
        public async Task<ActionResult> GetWorkoutSeriesGroupsAsync([FromQuery] ulong workoutId, [FromQuery] string userId)
        {
            try
            {
                var workoutSeriesGroups = await this.workoutsDataStore.GetWorkoutExercisesAsync(workoutId, userId);
                return new OkObjectResult(workoutSeriesGroups);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("active", Name = "getActiveWO")]
        public async Task<ActionResult> GetActiveWorkoutsAsync()
        {
            try
            {
                var activeWorkouts = await this.workoutsDataStore.GetActiveWorkoutsAsync();
                return new OkObjectResult(activeWorkouts);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("next", Name = "getNextWO")]
        public async Task<ActionResult> GetNextWorkoutAsync([FromQuery]string userId, [FromQuery] ulong activeWorkoutPlanId)
        {
            try
            {
                var activeWorkouts = await this.workoutsDataStore.GetNextWorkoutAsync(userId, activeWorkoutPlanId);
                return new OkObjectResult(activeWorkouts);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("has-active", Name = "hasActiveWO")]
        public async Task<ActionResult> HasActiveWorkoutPlanAsync()
        {
            try
            {
                var hasActiveWorkoutPlan = await this.workoutsDataStore.HasActiveWorkoutPlanAsync();
                return new OkObjectResult(hasActiveWorkoutPlan);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("plans", Name = "insertWOPlan")]
        public async Task<ActionResult> InsertWorkoutPlanAsync([FromQuery] string userId, [FromBody] WorkoutPlan workoutPlan)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (string.IsNullOrWhiteSpace(userId))
                    modelStateDictionary.AddModelError(nameof(userId), $"Invalid null or empty user id");
                if (workoutPlan == default)
                    modelStateDictionary.AddModelError(nameof(workoutPlan), $"Invalid workout plan data.");
                if (string.IsNullOrWhiteSpace(workoutPlan.Name))
                    modelStateDictionary.AddModelError(nameof(workoutPlan.Name), $"Invalid empty workout plan name.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var newWopId = await this.workoutsDataStore.InsertWorkoutPlanAsync(userId, workoutPlan.Name, workoutPlan.IsActive);

                return Ok(newWopId);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("workouts", Name = "insertWO")]
        public async Task<ActionResult> InsertWorkoutAsync([FromQuery] ulong workoutPlanId, [FromBody] Workout workout)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (workoutPlanId == 0)
                    modelStateDictionary.AddModelError(nameof(workoutPlanId), $"Invalid value for workout plan id");
                if (workout == default)
                    modelStateDictionary.AddModelError(nameof(workout), $"Invalid workout data.");
                if (string.IsNullOrWhiteSpace(workout.Name))
                    modelStateDictionary.AddModelError(nameof(workout.Name), $"Invalid empty workout name.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var newWoId = await this.workoutsDataStore.InsertWorkoutAsync(workoutPlanId, workout.Name, workout.Order);

                return Ok(newWoId);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("exercises", Name = "insertExercise")]
        public async Task<ActionResult> InsertExerciseAsync([FromQuery] string userId, [FromBody] ExerciseDefinition exerciseDefinition)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (string.IsNullOrWhiteSpace(userId))
                    modelStateDictionary.AddModelError(nameof(userId), $"Invalid null or empty user id");
                if (exerciseDefinition == default)
                    modelStateDictionary.AddModelError(nameof(exerciseDefinition), $"Invalid exercise definition data.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var newExId = await this.workoutsDataStore.InsertExerciseDefinitionAsync(userId, exerciseDefinition);

                return Ok(newExId);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("series-groups", Name = "insertSeriesGroups")]
        public async Task<ActionResult> InsertSeriesGroupsAsync([FromQuery] ulong workoutId, [FromBody] ExerciseGroup[] seriesGroups)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (workoutId == 0)
                    modelStateDictionary.AddModelError(nameof(workoutId), $"Invalid value for workout id");
                if (seriesGroups == default)
                    modelStateDictionary.AddModelError(nameof(seriesGroups), $"Invalid series group data.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                await this.workoutsDataStore.InsertSeriesGroupsAsync(workoutId, seriesGroups);

                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("data", Name = "insertWOData")]
        public async Task<ActionResult> InsertWorkoutDataAsync([FromBody] Models.InsertWorkoutDataDTO workoutData)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (workoutData == default)
                    modelStateDictionary.AddModelError(nameof(workoutData), $"Invalid workout data.");
                if (workoutData.WorkoutHistoryId == 0)
                    modelStateDictionary.AddModelError(nameof(workoutData.WorkoutHistoryId), $"Invalid workout history id value.");
                if (workoutData.SeriesData == default)
                    modelStateDictionary.AddModelError(nameof(workoutData.SeriesData), $"Invalid workout data content.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                if (workoutData.SeriesData.Length == 0)
                    return Ok();

                foreach (var seriesData in workoutData.SeriesData)
                {
                    await this.workoutsDataStore.InsertWorkoutDataAsync(workoutData.WorkoutHistoryId, seriesData.SerieId, seriesData.ExerciseId, seriesData.StartTime, seriesData.EndTime, seriesData.UsedKgs);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("history", Name = "insertWOHistory")]
        public async Task<ActionResult> InsertWorkoutHistoryAsync([FromBody] Models.InsertWorkoutHistoryDTO workoutHistoryDTO)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (workoutHistoryDTO == default)
                    modelStateDictionary.AddModelError(nameof(workoutHistoryDTO), $"Invalid workout history data.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                 var newWOHistoryId = await this.workoutsDataStore.InsertWorkoutHistoryAsync(workoutHistoryDTO.StartDate, workoutHistoryDTO.EndDate, workoutHistoryDTO.WorkoutId, workoutHistoryDTO.WorkoutPlanId, workoutHistoryDTO.UserId);
                return Ok(newWOHistoryId);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}