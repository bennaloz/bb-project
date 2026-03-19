CREATE PROCEDURE [dbo].[spr_GetActiveWorkouts]
AS
    SELECT *
    FROM tbl_Workout
    INNER JOIN tbl_WorkoutPlan ON tbl_Workout.fk_WorkoutPlanId = tbl_WorkoutPlan.Id
    WHERE tbl_WorkoutPlan.IsActive = 1
RETURN @@ROWCOUNT
