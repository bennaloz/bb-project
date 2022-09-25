CREATE PROCEDURE [dbo].[spw_UpdateWorkout]
    @workoutPlanId BIGINT,
    @workoutId BIGINT,
    @workoutName VARCHAR(100),
    @order SMALLINT
AS
    UPDATE tbl_Workout 
    SET [Name] = @workoutName, [Order] = @order
    WHERE [fk_WorkoutPlanId] = @workoutPlanId AND [Id] = @workoutId

RETURN @@ROWCOUNT
