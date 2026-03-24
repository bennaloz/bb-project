CREATE PROCEDURE [dbo].[spw_InsertExerciseDefinition]
    @name VARCHAR(100),
    @type SMALLINT, --Cardio or weights
    @involvedMuscles SMALLINT,
    @userId NVARCHAR(128),
    @exerciseId BIGINT OUTPUT
AS
    INSERT INTO tbl_ExerciseDefinition ([Name], [Type], [InvolvedMuscles], [fk_UserId])
    VALUES (@name, @type, @involvedMuscles, @userId)

SET @exerciseId = SCOPE_IDENTITY();
