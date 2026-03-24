-- V002: Update spr_GetWorkoutPlans to support optional userId filter
-- Allows the API to return only plans belonging to a specific user.

CREATE OR ALTER PROCEDURE [dbo].[spr_GetWorkoutPlans]
    @workoutPlanId BIGINT = 0,
    @userId        UNIQUEIDENTIFIER = NULL,
    @getArchived   BIT = 0
AS
BEGIN
    IF @getArchived = 0
        SELECT *
        FROM tbl_WorkoutPlan
        WHERE tbl_WorkoutPlan.IsArchived = 0
          AND (@workoutPlanId = 0 OR tbl_WorkoutPlan.Id = @workoutPlanId)
          AND (@userId IS NULL OR tbl_WorkoutPlan.fk_UserId = @userId)
    ELSE
        SELECT *
        FROM tbl_WorkoutPlan
        WHERE (@workoutPlanId = 0 OR tbl_WorkoutPlan.Id = @workoutPlanId)
          AND (@userId IS NULL OR tbl_WorkoutPlan.fk_UserId = @userId)
END
RETURN @@ROWCOUNT;
