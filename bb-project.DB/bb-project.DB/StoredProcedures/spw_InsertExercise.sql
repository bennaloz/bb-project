CREATE PROCEDURE [dbo].[spw_InsertExercise]
    @name VARCHAR,
    @exerciseId BIGINT OUTPUT
AS
    INSERT INTO tbl_Exercise ([Name])
    VALUES (@name)

SET @exerciseId = SCOPE_IDENTITY();
