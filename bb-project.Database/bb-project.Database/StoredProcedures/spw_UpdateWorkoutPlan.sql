CREATE PROCEDURE [dbo].[spw_UpdateWorkoutPlan]
    @workoutPlanId BIGINT,
    @userId UNIQUEIDENTIFIER,
    @workoutPlanName VARCHAR(100),
    @isActive BIT,
    @isArchived BIT
AS
    UPDATE tbl_WorkoutPlan
    SET [Name] = @workoutPlanName, [IsActive] = @isActive, [IsArchived] = @isArchived
    WHERE [Id] = @workoutPlanId AND [fk_UserId] = @userId

RETURN @@ROWCOUNT