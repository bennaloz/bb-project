CREATE TABLE [dbo].[tbl_WorkoutData]
(
    fk_SerieId BIGINT NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    fk_WorkoutHistoryId BIGINT NOT NULL,

)
