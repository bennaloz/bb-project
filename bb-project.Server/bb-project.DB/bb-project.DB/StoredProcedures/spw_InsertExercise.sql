CREATE PROCEDURE [dbo].[spw_InsertExercise]
    @name VARCHAR(100),
    @type SMALLINT, --Cardio or weights
    @involvedMuscles SMALLINT,
    @userId UNIQUEIDENTIFIER,
    @exerciseId BIGINT OUTPUT
AS
    INSERT INTO tbl_Exercise ([Name], [Type], [InvolvedMuscles], [fk_UserId])
    VALUES (@name, @type, @involvedMuscles, @userId)

SET @exerciseId = SCOPE_IDENTITY();
