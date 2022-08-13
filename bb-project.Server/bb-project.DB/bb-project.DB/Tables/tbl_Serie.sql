CREATE TABLE [dbo].[tbl_Serie]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Reps] SMALLINT NOT NULL, 
    [Rest] SMALLINT NULL, 
    [fk_ExerciseId] BIGINT NOT NULL, 
    [fk_WorkoutId] BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_Workout(Id), 
)
