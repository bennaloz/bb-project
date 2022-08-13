CREATE PROCEDURE [dbo].[spw_InsertWorkoutPlan]
    @workoutPlanName VARCHAR(100),
    @isActive bit = 0,
    @userId UNIQUEIDENTIFIER,
    @workoutPlanId bigint OUTPUT
AS
    INSERT INTO tbl_WorkoutPlan ([Name], IsActive, fk_UserId)
    VALUES (@workoutPlanName, @isActive, @userId)

    SET @workoutPlanId = SCOPE_IDENTITY();
RETURN @@ROWCOUNT
