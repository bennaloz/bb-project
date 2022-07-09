CREATE PROCEDURE [dbo].[spw_InsertWorkoutPlan]
    @workoutPlanName varchar,
    @isActive bit = 0,
    @workoutPlanId bigint OUTPUT
AS
    INSERT INTO tbl_WorkoutPlan ([Name], IsActive)
    VALUES (@workoutPlanName, @isActive)

    SET @workoutPlanId = SCOPE_IDENTITY();
RETURN @@ROWCOUNT
