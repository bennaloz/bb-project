CREATE PROCEDURE [dbo].[spw_InsertWorkoutSeries]
    @reps INT,
    @rest INT,
    @workoutId BIGINT,
    @ownerExerciseId BIGINT,
    @serieId BIGINT OUTPUT
AS
    INSERT INTO tbl_Serie (Reps, Rest, fk_WorkoutId, fk_ExerciseId)
    VALUES (@reps, @rest, @workoutId, @ownerExerciseId)
SET @serieId = SCOPE_IDENTITY();
