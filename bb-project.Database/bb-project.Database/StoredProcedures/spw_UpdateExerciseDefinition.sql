CREATE PROCEDURE [dbo].[spw_UpdateExerciseDefinition]
    @exerciseId BIGINT,
    @name VARCHAR(100),
    @type SMALLINT, --Cardio or weights
    @involvedMuscles SMALLINT
AS
    UPDATE tbl_ExerciseDefinition
    SET [Name] = @name, [Type] = @type, [InvolvedMuscles] = @involvedMuscles
    WHERE Id = @exerciseId
    
RETURN @@ROWCOUNT