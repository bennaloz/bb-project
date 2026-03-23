-- V001: Initial schema
-- Creates all base tables if they do not already exist.

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_User')
BEGIN
    CREATE TABLE [dbo].[tbl_User]
    (
        [Id]       UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        [UserName] VARCHAR(50)      NOT NULL
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_WorkoutPlan')
BEGIN
    CREATE TABLE [dbo].[tbl_WorkoutPlan]
    (
        [Id]         BIGINT           NOT NULL PRIMARY KEY IDENTITY,
        [Name]       VARCHAR(100)     NOT NULL,
        [IsActive]   BIT              NOT NULL DEFAULT 0,
        [fk_UserId]  UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES tbl_User(Id),
        [IsArchived] BIT              NOT NULL DEFAULT 0
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_Workout')
BEGIN
    CREATE TABLE [dbo].[tbl_Workout]
    (
        [Id]              BIGINT        NOT NULL PRIMARY KEY IDENTITY,
        [Name]            NVARCHAR(100) NOT NULL,
        [Order]           SMALLINT      NOT NULL,
        [fk_WorkoutPlanId] BIGINT       NOT NULL FOREIGN KEY REFERENCES tbl_WorkoutPlan(Id)
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_ExerciseDefinition')
BEGIN
    CREATE TABLE [dbo].[tbl_ExerciseDefinition]
    (
        [Id]              BIGINT           NOT NULL PRIMARY KEY IDENTITY,
        [Name]            VARCHAR(100)     NOT NULL,
        [Type]            SMALLINT         NOT NULL,
        [InvolvedMuscles] SMALLINT         NOT NULL,
        [fk_UserId]       UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES tbl_User(Id)
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_SeriesGroup')
BEGIN
    CREATE TABLE [dbo].[tbl_SeriesGroup]
    (
        [Id]             BIGINT   NOT NULL PRIMARY KEY IDENTITY,
        [ExerciseMethod] SMALLINT NOT NULL DEFAULT 1
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_Serie')
BEGIN
    CREATE TABLE [dbo].[tbl_Serie]
    (
        [Id]             BIGINT   NOT NULL PRIMARY KEY IDENTITY,
        [Reps]           SMALLINT NOT NULL,
        [Rest]           TIME(0)  NULL,
        [fk_ExerciseId]  BIGINT   NOT NULL FOREIGN KEY REFERENCES tbl_ExerciseDefinition(Id),
        [fk_WorkoutId]   BIGINT   NOT NULL FOREIGN KEY REFERENCES tbl_Workout(Id)
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_SeriesGroup_Serie')
BEGIN
    CREATE TABLE [dbo].[tbl_SeriesGroup_Serie]
    (
        [fk_SeriesGroup] BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_SeriesGroup(Id),
        [fk_Serie]       BIGINT NOT NULL FOREIGN KEY REFERENCES tbl_Serie(Id)
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_WorkoutHistory')
BEGIN
    CREATE TABLE [dbo].[tbl_WorkoutHistory]
    (
        [Id]              BIGINT           NOT NULL PRIMARY KEY IDENTITY,
        [StartDate]       DATETIME2        NOT NULL,
        [EndDate]         DATETIME2        NULL,
        [fk_WorkoutId]    BIGINT           NOT NULL FOREIGN KEY REFERENCES tbl_Workout(Id),
        [fk_WorkoutPlanId] BIGINT          NOT NULL FOREIGN KEY REFERENCES tbl_WorkoutPlan(Id),
        [fk_UserId]       UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES tbl_User(Id)
    );
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tbl_WorkoutData')
BEGIN
    CREATE TABLE [dbo].[tbl_WorkoutData]
    (
        [fk_SerieId]          BIGINT    NULL FOREIGN KEY REFERENCES tbl_Serie(Id),
        [StartTime]           DATETIME2 NOT NULL,
        [EndTime]             DATETIME2 NOT NULL,
        [UsedKgs]             FLOAT     NULL,
        [fk_WorkoutHistoryId] BIGINT    NULL FOREIGN KEY REFERENCES tbl_WorkoutHistory(Id),
        [fk_ExerciseId]       BIGINT    NOT NULL FOREIGN KEY REFERENCES tbl_ExerciseDefinition(Id)
    );
END
