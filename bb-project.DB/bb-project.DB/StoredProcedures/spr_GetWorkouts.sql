CREATE PROCEDURE [dbo].[spr_GetWorkouts]
    @workoutPlanId int,
    @workoutId int = 0
AS
    SELECT *
    FROM tbl_Workout
    WHERE tbl_Workout.fk_WorkoutPlanId = @workoutPlanId AND (@workoutId = 0 OR tbl_Workout.Id = @workoutId)
RETURN 0
