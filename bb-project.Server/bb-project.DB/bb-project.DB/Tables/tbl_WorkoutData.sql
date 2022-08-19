CREATE TABLE [dbo].[tbl_WorkoutData]
(
    fk_SerieId BIGINT NOT NULL,
    StartTime TIME NOT NULL, -- Ora di inizio della serie
    EndTime TIME NOT NULL, -- Ora di fine della serie
    UsedKgs FLOAT,
    fk_WorkoutHistoryId BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_WorkoutHistory(Id)
)
