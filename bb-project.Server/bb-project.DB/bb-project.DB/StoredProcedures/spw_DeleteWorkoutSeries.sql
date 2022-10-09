CREATE PROCEDURE [dbo].[spw_DeleteWorkoutSeries]
    @workoutPlanId BIGINT,
    @workoutId BIGINT,
    @serieIds IdsTableType READONLY
AS
    
    DECLARE @serieGroups IdsTableType;

    INSERT INTO @serieGroups 
    SELECT fk_SeriesGroup FROM tbl_SeriesGroup_Serie WHERE tbl_SeriesGroup_Serie.fk_Serie IN (SELECT Id FROM @serieIds);

    DELETE FROM tbl_SeriesGroup_Serie
    WHERE tbl_SeriesGroup_Serie.fk_Serie IN (SELECT Id FROM @serieIds)

    DELETE FROM tbl_SeriesGroup
    WHERE tbl_SeriesGroup.Id IN (SELECT Id FROM @serieGroups)
    
    DELETE wh 
    FROM tbl_WorkoutHistory AS wh
    INNER JOIN tbl_WorkoutData AS wd ON wd.fk_WorkoutHistoryId = wh.Id
    WHERE wh.fk_WorkoutId = @workoutId AND wh.fk_WorkoutPlanId = @workoutPlanId AND wd.fk_SerieId IN (SELECT Id FROM @serieIds)

    UPDATE tbl_WorkoutData
    SET [fk_SerieId] = NULL, [fk_WorkoutHistoryId] = NULL
    WHERE fk_SerieId IN (SELECT Id FROM @serieIds)
    
    DELETE FROM tbl_Serie
    WHERE tbl_Serie.fk_WorkoutId = @workoutId AND tbl_Serie.Id IN (SELECT Id From @serieIds)


    -- cancellare le serie indicate del workout e workoutplan
    -- nella tabella workoutdata, mettere a null le foreign key per i singoli workouthistory legati ai workout
    -- rimuovere i workout history per il workout e workoutplan
RETURN 0
