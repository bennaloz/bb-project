CREATE PROCEDURE [dbo].[spw_InsertWorkout]
    @workoutPlanId BIGINT,
    @workoutName VARCHAR,
    @workoutId bigint OUTPUT
AS
    INSERT INTO tbl_Workout(fk_WorkoutPlanId, [Name])
    VALUES (@workoutPlanId, @workoutName)

    SET @workoutId   = SCOPE_IDENTITY();
RETURN @@ROWCOUNT
