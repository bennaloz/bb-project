CREATE PROCEDURE [dbo].[spr_GetWorkoutHistory]
    @userId NVARCHAR(128),
    @workoutId BIGINT,
    @workoutPlanId BIGINT,
    @from DATE,
    @to DATE
AS
    SELECT *
    FROM tbl_WorkoutHistory AS woh
    WHERE woh.fk_UserId = @userId AND (@workoutPlanId IS NULL OR woh.fk_WorkoutPlanId = @workoutPlanId) AND (@workoutId IS NULL OR woh.fk_WorkoutId = @workoutId) AND
          CAST(woh.StartDate AS DATE) >= @from AND CAST(woh.EndDate AS DATE) <= @to
RETURN @@ROWCOUNT
