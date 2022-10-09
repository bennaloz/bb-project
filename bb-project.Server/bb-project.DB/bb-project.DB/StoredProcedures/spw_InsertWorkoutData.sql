CREATE PROCEDURE [dbo].[spw_InsertWorkoutData]
    @serieId BIGINT,
    @startTime TIME,
    @endTime TIME,
    @usedKgs FLOAT,
    @exerciseId BIGINT,
    @workoutHistoryId BIGINT
AS
    INSERT INTO tbl_WorkoutData (fk_SerieId, StartTime, EndTime, UsedKgs, fk_WorkoutHistoryId, fk_ExerciseId)
    VALUES (@serieId, @startTime, @endTime, @usedKgs, @workoutHistoryId, @exerciseId)

RETURN 1
