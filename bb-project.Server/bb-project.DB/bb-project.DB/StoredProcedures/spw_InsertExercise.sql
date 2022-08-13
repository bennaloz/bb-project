CREATE PROCEDURE [dbo].[spw_InsertExercise]
    @name VARCHAR(100),
    @type SMALLINT,
    @userId UNIQUEIDENTIFIER,
    @exerciseId BIGINT OUTPUT
AS
    INSERT INTO tbl_Exercise ([Name], [Type], [fk_UserId])
    VALUES (@name, @type, @userId)

SET @exerciseId = SCOPE_IDENTITY();
