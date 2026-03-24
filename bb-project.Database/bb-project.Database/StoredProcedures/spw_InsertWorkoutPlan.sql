CREATE PROCEDURE [dbo].[spw_InsertWorkoutPlan]
    @workoutPlanName VARCHAR(100),
    @isActive bit = 0,
    @isArchived BIT = 0,
    @userId NVARCHAR(128),
    @workoutPlanId bigint OUTPUT
AS
    INSERT INTO tbl_WorkoutPlan ([Name], IsActive, IsArchived, fk_UserId)
    VALUES (@workoutPlanName, @isActive, @isArchived, @userId)

    SET @workoutPlanId = SCOPE_IDENTITY();
RETURN @@ROWCOUNT
