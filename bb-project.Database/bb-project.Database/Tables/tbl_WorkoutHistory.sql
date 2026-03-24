CREATE TABLE [dbo].[tbl_WorkoutHistory]
(
    [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    StartDate DATETIME2 NOT NULL, --Data ed ora di inizio
    EndDate DATETIME2, --Data ed ora di fine
    fk_WorkoutId BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_Workout(Id),
    fk_WorkoutPlanId BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_WorkoutPlan(Id),
    fk_UserId NVARCHAR(128) NOT NULL FOREIGN KEY REFERENCES tbl_User(Id)
)
