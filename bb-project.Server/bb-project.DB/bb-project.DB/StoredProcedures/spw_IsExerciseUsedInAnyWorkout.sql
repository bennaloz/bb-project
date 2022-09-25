CREATE PROCEDURE [dbo].[spw_IsExerciseUsedInAnyWorkout]
    @exerciseId BIGINT,
    @userId UNIQUEIDENTIFIER,
    @isExerciseUsed BIT OUTPUT
AS
    SELECT @isExerciseUsed = count(*) 
    FROM tbl_Exercise AS exercise
    INNER JOIN tbl_User AS usr ON usr.Id = exercise.fk_UserId
    INNER JOIN tbl_Serie AS serie ON serie.fk_ExerciseId = exercise.Id
    WHERE exercise.Id = @exerciseId AND usr.Id = @userId

RETURN 0
