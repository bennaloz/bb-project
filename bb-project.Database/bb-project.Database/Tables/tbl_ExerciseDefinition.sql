CREATE TABLE [dbo].[tbl_ExerciseDefinition]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(100) NOT NULL, 
    [Type] SMALLINT NOT NULL,
    [InvolvedMuscles] SMALLINT NOT NULL,
    [fk_UserId] NVARCHAR(128) NOT NULL FOREIGN KEY REFERENCES tbl_User(Id) 

)
