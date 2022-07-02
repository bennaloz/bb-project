CREATE PROCEDURE [dbo].[spr_GetWorkoutPlans]
    @workoutPlanId int = 0
AS
    BEGIN
        SELECT *
        FROM tbl_WorkoutPlan
        WHERE @workoutPlanId = 0 OR tbl_WorkoutPlan.Id = @workoutPlanId
    END
RETURN 0
