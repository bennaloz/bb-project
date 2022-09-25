CREATE PROCEDURE [dbo].[spr_GetWorkoutPlans]
    @workoutPlanId int = 0,
    @getArchived BIT = 0
AS
    BEGIN

    IF @getArchived = 0
        SELECT *
        FROM tbl_WorkoutPlan
        WHERE tbl_WorkoutPlan.IsArchived = 0 AND (@workoutPlanId = 0 OR tbl_WorkoutPlan.Id = @workoutPlanId)
    ELSE
        SELECT *
        FROM tbl_WorkoutPlan
        WHERE @workoutPlanId = 0 OR tbl_WorkoutPlan.Id = @workoutPlanId
    END
RETURN @@ROWCOUNT;
