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
            return new BadRequestResult();
        }
    }
}