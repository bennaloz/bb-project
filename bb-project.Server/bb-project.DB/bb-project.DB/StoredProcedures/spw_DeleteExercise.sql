CREATE PROCEDURE [dbo].[spw_DeleteExercise]
    @exerciseId BIGINT,
    @userId UNIQUEIDENTIFIER
AS
    -- 09/10/22 Sospendiamo la gestione della cancellazione perché vorrebbe dire cancellare tutti i workout e wokrkoutplan associati, oltre che i dati relativi all'history degli allenamenti
    -- delete all the related workout plans
    -- consider calling the delete workout plan specific stored procedure
RETURN 0
