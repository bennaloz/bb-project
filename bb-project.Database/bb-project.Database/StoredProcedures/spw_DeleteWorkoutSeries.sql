CREATE PROCEDURE [dbo].[spw_DeleteWorkoutSeries]
    @workoutPlanId BIGINT,
    @workoutId BIGINT,
    @serieIds IdsTableType READONLY
AS
    IF (
        SELECT COUNT(*)
        FROM [dbo].[tbl_Serie] AS s
        INNER JOIN tbl_Workout ON tbl_Workout.Id = s.fk_WorkoutId
        WHERE tbl_Workout.Id = @workoutId AND tbl_Workout.fk_WorkoutPlanId = @workoutPlanId AND s.Id IN (SELECT * FROM @serieIds) 
    ) = 0
        RETURN -1

    DECLARE @serieGroups IdsTableType;

    INSERT INTO @serieGroups 
    SELECT fk_SeriesGroup FROM tbl_SeriesGroup_Serie WHERE tbl_SeriesGroup_Serie.fk_Serie IN (SELECT Id FROM @serieIds);

    DELETE FROM tbl_SeriesGroup_Serie
    WHERE tbl_SeriesGroup_Serie.fk_Serie IN (SELECT Id FROM @serieIds)

    --DELETE FROM tbl_SeriesGroup   
    --WHERE tbl_SeriesGroup.Id IN (SELECT Id FROM @serieGroups)
    
    DELETE wh 
    FROM tbl_WorkoutHistory AS wh
    INNER JOIN tbl_WorkoutData AS wd ON wd.fk_WorkoutHistoryId = wh.Id
    WHERE wh.fk_WorkoutId = @workoutId AND wh.fk_WorkoutPlanId = @workoutPlanId AND wd.fk_SerieId IN (SELECT Id FROM @serieIds)

    UPDATE tbl_WorkoutData
    SET [fk_SerieId] = NULL, [fk_WorkoutHistoryId] = NULL
    WHERE fk_SerieId IN (SELECT Id FROM @serieIds)
    
    DELETE FROM tbl_Serie
    WHERE tbl_Serie.fk_WorkoutId = @workoutId AND tbl_Serie.Id IN (SELECT Id From @serieIds)

    DECLARE @seriesCountPerGroup INT = 0
    DECLARE @groupId BIGINT = 0
    WHILE (SELECT COUNT(*) FROM @serieGroups) > 0
    BEGIN
        SET @groupId = (SELECT TOP 1 Id FROM @serieGroups)
        SET @seriesCountPerGroup = (
            SELECT COUNT(*) 
            FROM tbl_SeriesGroup_Serie
            WHERE tbl_SeriesGroup_Serie.fk_SeriesGroup = @groupId)
        IF @seriesCountPerGroup = 0
            DELETE FROM tbl_SeriesGroup WHERE Id = @groupId
        DELETE FROM @serieGroups WHERE Id = @groupId
    END
    -- cancellare le serie indicate del workout e workoutplan
    -- nella tabella workoutdata, mettere a null le foreign key per i singoli workouthistory legati ai workout
    -- rimuovere i workout history per il workout e workoutplan
RETURN 0
