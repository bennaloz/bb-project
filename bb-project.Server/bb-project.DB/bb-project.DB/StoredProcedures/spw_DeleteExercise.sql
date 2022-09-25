CREATE PROCEDURE [dbo].[spw_DeleteExercise]
    @exerciseId BIGINT,
    @userId UNIQUEIDENTIFIER
AS
    -- delete all the related workout plans
    -- consider calling the delete workout plan specific stored procedure
RETURN 0
