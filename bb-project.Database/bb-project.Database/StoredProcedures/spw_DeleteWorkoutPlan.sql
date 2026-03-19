CREATE PROCEDURE [dbo].[spw_DeleteWorkoutPlan]
    @workoutPlanId BIGINT
AS
    -- 09/10/22 Sospendiamo la gestione della cancellazione perché vorrebbe dire cancellare tutti i workout e wokrkoutplan associati, oltre che i dati relativi all'history degli allenamenti
    -- delete whole workout history and workout data
    -- consider calling the delete workout specific stored procedure to delete every single workout
    -- delete all the related workouts
    -- delete all the series that belong to the workouts
RETURN 0
