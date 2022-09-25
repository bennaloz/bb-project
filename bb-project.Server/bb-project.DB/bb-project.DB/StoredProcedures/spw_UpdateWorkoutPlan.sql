CREATE PROCEDURE [dbo].[spw_UpdateWorkoutPlan]
    @workoutPlanId BIGINT,
    @workoutPlanName VARCHAR(100),
    @isActive BIT,
    @isArchived BIT,
    @userId UNIQUEIDENTIFIER
AS
    UPDATE tbl_WorkoutPlan
    SET [Name] = @workoutPlanName, [IsActive] = @isActive, [IsArchived] = @isArchived
    WHERE [Id] = @workoutPlanId AND [fk_UserId] = @userId

RETURN @@ROWCOUNT