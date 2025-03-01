CREATE PROCEDURE [dbo].[spw_InsertWorkout]
    @workoutPlanId BIGINT,
    @workoutName VARCHAR(100),
    @order SMALLINT,
    @workoutId bigint OUTPUT
AS
    INSERT INTO tbl_Workout(fk_WorkoutPlanId, [Order], [Name])
    VALUES (@workoutPlanId, @order, @workoutName)

    SET @workoutId   = SCOPE_IDENTITY();
RETURN @@ROWCOUNT
