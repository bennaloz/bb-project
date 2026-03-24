CREATE PROCEDURE [dbo].[spr_GetExercisesDefinitions]
    @userId NVARCHAR(128)
AS
    SELECT *
    FROM tbl_ExerciseDefinition
    WHERE tbl_ExerciseDefinition.fk_UserId = @userId
RETURN @@ROWCOUNT
