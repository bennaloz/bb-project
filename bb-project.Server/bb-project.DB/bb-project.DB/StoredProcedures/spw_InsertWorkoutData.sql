CREATE PROCEDURE [dbo].[spw_InsertWorkoutData]
    @serieId BIGINT,
    @startTime TIME,
    @endTime TIME,
    @usedKgs FLOAT,
    @workoutHistoryId BIGINT
AS
    INSERT INTO tbl_WorkoutData (fk_SerieId, StartTime, EndTime, UsedKgs, fk_WorkoutHistoryId)
    VALUES (@serieId, @startTime, @endTime, @usedKgs, @workoutHistoryId)

RETURN 1
