CREATE PROCEDURE [dbo].[spr_GetWorkoutSeriesGroup]
    @workoutId BIGINT,
    @userId UNIQUEIDENTIFIER,
    @seriesGroupId BIGINT = 0
AS
    BEGIN
    IF @seriesGroupId = 0
        SELECT tbl_Serie.*,
               tbl_Exercise.Id AS DefinitionExerciseId,
               tbl_Exercise.Name AS DefinitionExerciseName,
               tbl_Exercise.Type AS DefinitionExerciseType,
               tbl_Exercise.InvolvedMuscles AS InvolvedMuscles
        FROM tbl_Serie
        INNER JOIN tbl_Exercise ON tbl_Exercise.Id = tbl_Serie.fk_ExerciseId
        WHERE tbl_Serie.fk_WorkoutId = @workoutId AND tbl_Exercise.fk_UserId = @userId
    ELSE
        SELECT tbl_Serie.*,
               tbl_Exercise.Id AS DefinitionExerciseId,
               tbl_Exercise.Name AS DefinitionExerciseName,
               tbl_Exercise.Type AS DefinitionExerciseType,
               tbl_Exercise.InvolvedMuscles AS InvolvedMuscles,
               tbl_SeriesGroup.Id AS SeriesGroupId,
               tbl_SeriesGroup.ExerciseMethod AS ExerciseMethod
        FROM tbl_Serie
        INNER JOIN tbl_Exercise ON tbl_Exercise.Id = tbl_Serie.fk_ExerciseId
        INNER JOIN tbl_SeriesGroup_Serie ON tbl_SeriesGroup_Serie.fk_Serie = tbl_Serie.Id
        INNER JOIN tbl_SeriesGroup ON tbl_SeriesGroup.Id = tbl_SeriesGroup_Serie.fk_SeriesGroup
        WHERE (@seriesGroupId IS NULL OR @seriesGroupId = 0 OR @seriesGroupId = tbl_SeriesGroup.Id) AND tbl_Serie.fk_WorkoutId = @workoutId AND tbl_Exercise.fk_UserId = @userId
    END
RETURN @@ROWCOUNT
