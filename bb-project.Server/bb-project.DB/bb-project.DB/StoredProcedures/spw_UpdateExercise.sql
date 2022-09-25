CREATE PROCEDURE [dbo].[spw_UpdateExercise]
    @name VARCHAR(100),
    @type SMALLINT, --Cardio or weights
    @involvedMuscles SMALLINT,
    @exerciseId BIGINT OUTPUT
AS
    UPDATE tbl_Exercise
    SET [Name] = @name, [Type] = @type, [InvolvedMuscles] = @involvedMuscles
    WHERE Id = @exerciseId
    
RETURN @@ROWCOUNT