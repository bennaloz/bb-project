CREATE PROCEDURE [dbo].[spw_InsertWorkoutSeriesGroup]
    @exerciseMethod INT,
    @seriesGroupId BIGINT OUTPUT
AS
    INSERT INTO tbl_SeriesGroup(ExerciseMethod)
    VALUES (@exerciseMethod)
SET @seriesGroupId = SCOPE_IDENTITY();
