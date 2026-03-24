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
        private readonly IWebHostEnvironment _env;

        public WorkoutsEditorController(ILogger<WorkoutsEditorController> logger,
            IWorkoutsDataStore workoutsDataStore,
            IWebHostEnvironment env)
        {
            _logger = logger;
            this.workoutsDataStore = workoutsDataStore;
            _env = env;
        }

        private ActionResult HandleException(Exception ex, string operation)
        {
            _logger.LogError(ex, "Error in {Operation}: {Message}", operation, ex.Message);
            if (!_env.IsProduction())
                return Problem(
                    detail: $"{ex.GetType().Name}: {ex.Message}",
                    title: $"Error in {operation}",
                    statusCode: 500);
            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Gets workout plans, optionally filtered by workoutPlanId or userId.
        /// </summary>
        /// <param name="workoutPlanId">Optional: filter to a specific plan by its ID.</param>
        /// <param name="userId">Required for user-scoped results: returns only plans belonging to this user.</param>
        /// <returns>List of workout plans.</returns>
        [HttpGet("plans", Name = "getWOPlans")]
        [ProducesResponseType(typeof(IEnumerable<WorkoutPlan>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetWorkoutsPlansAsync([FromQuery] ulong? workoutPlanId = null, [FromQuery] string? userId = null)
        {
            try
            {
                var workoutPlans = await this.workoutsDataStore.GetWorkoutPlansAsync(id: workoutPlanId, userId: userId);
                return new OkObjectResult(workoutPlans);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(GetWorkoutsPlansAsync));
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
                return HandleException(ex, nameof(GetWorkoutsAsync));
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
                return HandleException(ex, nameof(GetWorkoutSeriesGroupsAsync));
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
                return HandleException(ex, nameof(GetActiveWorkoutsAsync));
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
                return HandleException(ex, nameof(GetNextWorkoutAsync));
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
                return HandleException(ex, nameof(HasActiveWorkoutPlanAsync));
            }
        }

        /// <summary>
        /// Creates a new workout plan for the specified user.
        /// </summary>
        /// <param name="userId">Required: the ID of the user who owns the plan.</param>
        /// <param name="workoutPlan">Required: the plan body. Must include a non-empty Name.</param>
        /// <returns>The ID of the newly created workout plan.</returns>
        [HttpPost("plans", Name = "insertWOPlan")]
        [ProducesResponseType(typeof(ulong), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> InsertWorkoutPlanAsync([FromQuery] string userId, [FromBody] WorkoutPlan workoutPlan)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (string.IsNullOrWhiteSpace(userId))
                    modelStateDictionary.AddModelError(nameof(userId), $"Invalid null or empty user id");
                if (workoutPlan == default)
                    modelStateDictionary.AddModelError(nameof(workoutPlan), $"Invalid workout plan data.");
                if (workoutPlan != default && string.IsNullOrWhiteSpace(workoutPlan.Name))
                    modelStateDictionary.AddModelError(nameof(workoutPlan.Name), $"Invalid empty workout plan name.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var newWopId = await this.workoutsDataStore.InsertWorkoutPlanAsync(userId, workoutPlan.Name, workoutPlan.IsActive);

                return Ok(newWopId);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(InsertWorkoutPlanAsync));
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
                if (workout != default && string.IsNullOrWhiteSpace(workout.Name))
                    modelStateDictionary.AddModelError(nameof(workout.Name), $"Invalid empty workout name.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var newWoId = await this.workoutsDataStore.InsertWorkoutAsync(workoutPlanId, workout.Name, workout.Order);

                return Ok(newWoId);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(InsertWorkoutAsync));
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
                return HandleException(ex, nameof(InsertExerciseAsync));
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
                return HandleException(ex, nameof(InsertSeriesGroupsAsync));
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
                return HandleException(ex, nameof(InsertWorkoutDataAsync));
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
                return HandleException(ex, nameof(InsertWorkoutHistoryAsync));
            }
        }

        /// <summary>
        /// Gets all exercise definitions for a given user.
        /// </summary>
        [HttpGet("exercise-definitions", Name = "getExerciseDefinitions")]
        public async Task<ActionResult> GetExerciseDefinitionsAsync([FromQuery] string userId)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (string.IsNullOrWhiteSpace(userId))
                    modelStateDictionary.AddModelError(nameof(userId), $"Invalid null or empty user id");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var exerciseDefinitions = await this.workoutsDataStore.GetExerciseDefinitionsAsync(userId);
                return new OkObjectResult(exerciseDefinitions);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(GetExerciseDefinitionsAsync));
            }
        }

        /// <summary>
        /// Gets workout/completed session history for a user, optionally filtered by plan, workout, and date range.
        /// </summary>
        [HttpGet("history", Name = "getWOHistory")]
        public async Task<ActionResult> GetWorkoutHistoryAsync([FromQuery] string userId, [FromQuery] ulong? workoutPlanId = null, [FromQuery] ulong? workoutId = null, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (string.IsNullOrWhiteSpace(userId))
                    modelStateDictionary.AddModelError(nameof(userId), $"Invalid null or empty user id");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var historyItems = await this.workoutsDataStore.GetWorkoutHistoryItems(userId, workoutPlanId, workoutId, from ?? default, to ?? default);
                return new OkObjectResult(historyItems);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(GetWorkoutHistoryAsync));
            }
        }

        /// <summary>
        /// Updates an existing workout plan (name, isActive, isArchived).
        /// </summary>
        [HttpPut("plans/{id}", Name = "updateWOPlan")]
        public async Task<ActionResult> UpdateWorkoutPlanAsync([FromRoute] ulong id, [FromBody] Models.UpdateWorkoutPlanDTO dto)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (id == 0)
                    modelStateDictionary.AddModelError(nameof(id), $"Invalid workout plan id.");
                if (dto == default)
                    modelStateDictionary.AddModelError(nameof(dto), $"Invalid workout plan data.");
                if (dto != default && string.IsNullOrWhiteSpace(dto.UserId))
                    modelStateDictionary.AddModelError(nameof(dto.UserId), $"Invalid null or empty user id.");
                if (dto != default && string.IsNullOrWhiteSpace(dto.Name))
                    modelStateDictionary.AddModelError(nameof(dto.Name), $"Invalid empty workout plan name.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var affected = await this.workoutsDataStore.UpdateWorkoutPlanAsync(id, dto.UserId, dto.Name, dto.IsActive, dto.IsArchived);
                return Ok(affected);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(UpdateWorkoutPlanAsync));
            }
        }

        /// <summary>
        /// Updates an existing workout (name, order).
        /// </summary>
        [HttpPut("workouts/{id}", Name = "updateWO")]
        public async Task<ActionResult> UpdateWorkoutAsync([FromRoute] ulong id, [FromBody] Models.UpdateWorkoutDTO dto)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (id == 0)
                    modelStateDictionary.AddModelError(nameof(id), $"Invalid workout id.");
                if (dto == default)
                    modelStateDictionary.AddModelError(nameof(dto), $"Invalid workout data.");
                if (dto != default && dto.WorkoutPlanId == 0)
                    modelStateDictionary.AddModelError(nameof(dto.WorkoutPlanId), $"Invalid workout plan id.");
                if (dto != default && string.IsNullOrWhiteSpace(dto.Name))
                    modelStateDictionary.AddModelError(nameof(dto.Name), $"Invalid empty workout name.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var affected = await this.workoutsDataStore.UpdateWorkoutAsync(dto.WorkoutPlanId, id, dto.Name, dto.Order);
                return Ok(affected);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(UpdateWorkoutAsync));
            }
        }

        /// <summary>
        /// Updates an existing exercise definition (name, type, involved muscles).
        /// </summary>
        [HttpPut("exercises/{id}", Name = "updateExercise")]
        public async Task<ActionResult> UpdateExerciseDefinitionAsync([FromRoute] ulong id, [FromBody] Models.UpdateExerciseDefinitionDTO dto)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (id == 0)
                    modelStateDictionary.AddModelError(nameof(id), $"Invalid exercise definition id.");
                if (dto == default)
                    modelStateDictionary.AddModelError(nameof(dto), $"Invalid exercise definition data.");
                if (dto != default && string.IsNullOrWhiteSpace(dto.Name))
                    modelStateDictionary.AddModelError(nameof(dto.Name), $"Invalid empty exercise definition name.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var affected = await this.workoutsDataStore.UpdateExerciseDefinitionAsync(id, dto.Name, dto.Type, dto.InvolvedMuscles);
                return Ok(affected);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(UpdateExerciseDefinitionAsync));
            }
        }

        /// <summary>
        /// Deletes workout series by their ids within a given workout plan and workout.
        /// </summary>
        [HttpDelete("series", Name = "deleteWOSeries")]
        public async Task<ActionResult> DeleteWorkoutSeriesAsync([FromBody] Models.DeleteWorkoutSeriesDTO dto)
        {
            try
            {
                var modelStateDictionary = new ModelStateDictionary();
                if (dto == default)
                    modelStateDictionary.AddModelError(nameof(dto), $"Invalid series data.");
                if (dto != default && dto.WorkoutPlanId == 0)
                    modelStateDictionary.AddModelError(nameof(dto.WorkoutPlanId), $"Invalid workout plan id.");
                if (dto != default && dto.WorkoutId == 0)
                    modelStateDictionary.AddModelError(nameof(dto.WorkoutId), $"Invalid workout id.");
                if (dto != default && (dto.SeriesIds == null || dto.SeriesIds.Length == 0))
                    modelStateDictionary.AddModelError(nameof(dto.SeriesIds), $"Series ids must not be empty.");
                if (modelStateDictionary.Count > 0)
                    return new BadRequestObjectResult(modelStateDictionary);

                var affected = await this.workoutsDataStore.DeleteWorkoutSeriesAsync(dto.WorkoutPlanId, dto.WorkoutId, dto.SeriesIds);
                return Ok(affected);
            }
            catch (Exception ex)
            {
                return HandleException(ex, nameof(DeleteWorkoutSeriesAsync));
            }
        }
    }
}