CREATE PROCEDURE [dbo].[spr_GetExercises]
    @userId UNIQUEIDENTIFIER
AS
    SELECT *
    FROM tbl_Exercise
    WHERE tbl_Exercise.fk_UserId = @userId
RETURN @@ROWCOUNT
