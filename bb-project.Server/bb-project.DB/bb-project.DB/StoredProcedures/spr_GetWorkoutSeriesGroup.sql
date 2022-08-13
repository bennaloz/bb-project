CREATE PROCEDURE [dbo].[spr_GetWorkoutSeriesGroup]
    @workoutId BIGINT,
    @userId UNIQUEIDENTIFIER,
    @seriesGroupId BIGINT
AS
    SELECT tbl_Serie.*,
           tbl_Exercise.Id AS OwnerExerciseId,
           tbl_Exercise.Name AS OwnerExerciseName,
           tbl_Exercise.Type AS OwnerExerciseType,
           tbl_SeriesGroup.ExerciseMethod AS ExerciseMethod
    FROM tbl_Serie
    INNER JOIN tbl_Exercise ON tbl_Exercise.Id = tbl_Serie.fk_ExerciseId
    INNER JOIN tbl_SeriesGroup_Serie ON tbl_SeriesGroup_Serie.fk_Serie = tbl_Serie.Id
    INNER JOIN tbl_SeriesGroup ON tbl_SeriesGroup.Id = tbl_SeriesGroup_Serie.fk_SeriesGroup
    WHERE (@seriesGroupId IS NULL OR @seriesGroupId = 0 OR @seriesGroupId = tbl_SeriesGroup.Id) AND tbl_Serie.fk_WorkoutId = @workoutId AND tbl_Exercise.fk_UserId = @userId
RETURN @@ROWCOUNT
