CREATE PROCEDURE [dbo].[spw_DeleteWorkout]
    @workoutId BIGINT,
    @workoutPlanId BIGINT
AS
        -- 09/10/22 Sospendiamo la gestione della cancellazione perché vorrebbe dire cancellare tutti i workout e wokrkoutplan associati, oltre che i dati relativi all'history degli allenamenti
RETURN 0
