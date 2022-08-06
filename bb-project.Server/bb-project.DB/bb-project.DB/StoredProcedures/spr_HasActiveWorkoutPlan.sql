CREATE PROCEDURE [dbo].[spr_HasActiveWorkoutPlan]
    @hasActiveWorkoutPlan BIT OUTPUT
AS
    SELECT @hasActiveWorkoutPlan = count(*) FROM tbl_WorkoutPlan AS wop WHERE wop.IsActive = 1
RETURN 0
