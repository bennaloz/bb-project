CREATE PROCEDURE [dbo].[spr_GetWorkoutSerie]
    @workoutId int,
    @userId uniqueidentifier
AS
    SELECT tbl_Serie.*,
           tbl_Exercise.Id AS OwnerExerciseId,
           tbl_Exercise.Name AS OwnerExerciseName,
           tbl_Exercise.Type AS OwnerExerciseType
    FROM tbl_Serie
    INNER JOIN tbl_Exercise ON tbl_Exercise.Id = tbl_Serie.fk_ExerciseId 
    WHERE tbl_Serie.fk_WorkoutId = @workoutId AND tbl_Exercise.fk_UserId = @userId
RETURN @@ROWCOUNT
