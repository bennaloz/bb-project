CREATE TABLE [dbo].[tbl_WorkoutData]
(
    fk_SerieId BIGINT FOREIGN KEY REFERENCES tbl_Serie(Id),
    StartTime DATETIME2 NOT NULL, -- Data ed ora di inizio della serie
    EndTime DATETIME2 NOT NULL, -- Data ed ora di fine della serie
    UsedKgs FLOAT,
    fk_WorkoutHistoryId BIGINT FOREIGN KEY REFERENCES tbl_WorkoutHistory(Id),
    fk_ExerciseId BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_ExerciseDefinition(Id)
)
