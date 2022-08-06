CREATE PROCEDURE [dbo].[spw_InsertExercise]
    @name VARCHAR,
    @type SMALLINT,
    @exerciseId BIGINT OUTPUT
AS
    INSERT INTO tbl_Exercise ([Name], [Type])
    VALUES (@name, @type)

SET @exerciseId = SCOPE_IDENTITY();
