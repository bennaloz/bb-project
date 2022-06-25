CREATE TABLE [dbo].[tbl_Serie]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Reps] SMALLINT NOT NULL, 
    [Rest] SMALLINT NULL, 
    [ExerciseKind] SMALLINT NOT NULL, 
    [fk_ExerciseId] BIGINT NOT NULL,

)
