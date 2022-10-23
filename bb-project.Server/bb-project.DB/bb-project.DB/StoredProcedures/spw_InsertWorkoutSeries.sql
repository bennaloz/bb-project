CREATE PROCEDURE [dbo].[spw_InsertWorkoutSeries]
    @reps INT,
    @rest TIME,
    @workoutId BIGINT,
    @definitionExerciseId BIGINT,
    @seriesGroupId BIGINT,
    @serieId BIGINT OUTPUT
AS

    INSERT INTO tbl_Serie (Reps, Rest, fk_WorkoutId, fk_ExerciseId)
    VALUES (@reps, @rest, @workoutId, @definitionExerciseId)

SET @serieId = SCOPE_IDENTITY();

    INSERT INTO tbl_SeriesGroup_Serie(fk_SeriesGroup, fk_Serie)
    VALUES(@seriesGroupId, @serieId)