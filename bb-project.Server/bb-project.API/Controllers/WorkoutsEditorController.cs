using bb_project.Infrastructure.BLL;
using bb_project.Infrastructure.Models.Data;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet(Name = "getWorkoutsPlans")]
        public async Task<ActionResult> GetWorkoutsPlansAsync([FromQuery] long? workoutPlanId = null)
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

        [HttpGet]
        public async Task<ActionResult> GetWorkoutsAsync([FromQuery] long workoutPlanId, [FromQuery] long? workoutId = null)
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

        [HttpGet]
        public async Task<ActionResult> GetWorkoutSeriesGroupsAsync([FromQuery] long workoutId, [FromQuery] string userId)
        {
            try
            {
                var workoutSeriesGroups = await this.workoutsDataStore.GetWorkoutSeriesGroupsAsync(workoutId, userId);
                return new OkObjectResult(workoutSeriesGroups);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<ActionResult> GetNextWorkoutAsync([FromQuery]string userId, [FromQuery] long activeWorkoutPlanId)
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

        [HttpGet]
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
    }
}