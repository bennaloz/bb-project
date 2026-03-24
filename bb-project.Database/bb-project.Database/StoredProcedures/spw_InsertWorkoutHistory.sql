CREATE PROCEDURE [dbo].[spw_InsertWorkoutHistory]
    @startDate DATETIME2, 
    @endDate DATETIME2,
    @fkWorkoutId BIGINT,
    @fkWorkoutPlanId BIGINT,
    @fkUserId NVARCHAR(128),
    @workoutHistoryId BIGINT OUTPUT
AS
    
    INSERT INTO tbl_WorkoutHistory(StartDate, EndDate, fk_WorkoutId, fk_WorkoutPlanId, fk_UserId)
    VALUES (@startDate, @endDate, @fkWorkoutId, @fkWorkoutPlanId, @fkUserId)

    SET @workoutHistoryId   = SCOPE_IDENTITY();
