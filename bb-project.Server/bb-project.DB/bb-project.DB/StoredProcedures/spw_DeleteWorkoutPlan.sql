CREATE PROCEDURE [dbo].[spw_DeleteWorkoutPlan]
    @workoutPlanId BIGINT
AS
    -- delete whole workout history and workout data
    -- consider calling the delete workout specific stored procedure to delete every single workout
    -- delete all the related workouts
    -- delete all the series that belong to the workouts
RETURN 0
