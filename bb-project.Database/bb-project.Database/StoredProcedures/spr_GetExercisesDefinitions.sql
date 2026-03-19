CREATE PROCEDURE [dbo].[spr_GetExercisesDefinitions]
    @userId UNIQUEIDENTIFIER
AS
    SELECT *
    FROM tbl_ExerciseDefinition
    WHERE tbl_ExerciseDefinition.fk_UserId = @userId
RETURN @@ROWCOUNT
