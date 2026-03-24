-- V003: Change userId columns from UNIQUEIDENTIFIER to NVARCHAR(128)
-- Root cause fix: stored procedures expected UNIQUEIDENTIFIER, but the app sends simple string user IDs (e.g. 'user1').
-- Also seeds dummy test users.

-- Step 1: Drop all FK constraints referencing tbl_User.Id (dynamically, since names are auto-generated)
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql = @sql + N'ALTER TABLE [dbo].[' + OBJECT_NAME(parent_object_id) + N'] DROP CONSTRAINT [' + name + N'];' + CHAR(13)
FROM sys.foreign_keys
WHERE referenced_object_id = OBJECT_ID(N'dbo.tbl_User');
IF LEN(@sql) > 0
    EXEC sp_executesql @sql;
GO

-- Step 2: Drop PK on tbl_User (required before altering PK column type)
DECLARE @pkName NVARCHAR(256);
SELECT @pkName = name FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID(N'dbo.tbl_User');
IF @pkName IS NOT NULL
    EXEC(N'ALTER TABLE [dbo].[tbl_User] DROP CONSTRAINT [' + @pkName + N']');
GO

-- Step 3: Alter tbl_User.Id column type to NVARCHAR(128)
ALTER TABLE [dbo].[tbl_User] ALTER COLUMN [Id] NVARCHAR(128) NOT NULL;
GO

-- Step 4: Re-add PK on tbl_User
ALTER TABLE [dbo].[tbl_User] ADD CONSTRAINT [PK_tbl_User] PRIMARY KEY CLUSTERED ([Id]);
GO

-- Step 5: Alter FK columns in child tables
ALTER TABLE [dbo].[tbl_WorkoutPlan] ALTER COLUMN [fk_UserId] NVARCHAR(128) NOT NULL;
GO

ALTER TABLE [dbo].[tbl_ExerciseDefinition] ALTER COLUMN [fk_UserId] NVARCHAR(128) NOT NULL;
GO

ALTER TABLE [dbo].[tbl_WorkoutHistory] ALTER COLUMN [fk_UserId] NVARCHAR(128) NOT NULL;
GO

-- Step 6: Re-add FK constraints with explicit names
ALTER TABLE [dbo].[tbl_WorkoutPlan]
    ADD CONSTRAINT [FK_tbl_WorkoutPlan_fk_UserId] FOREIGN KEY ([fk_UserId]) REFERENCES [dbo].[tbl_User]([Id]);
GO

ALTER TABLE [dbo].[tbl_ExerciseDefinition]
    ADD CONSTRAINT [FK_tbl_ExerciseDefinition_fk_UserId] FOREIGN KEY ([fk_UserId]) REFERENCES [dbo].[tbl_User]([Id]);
GO

ALTER TABLE [dbo].[tbl_WorkoutHistory]
    ADD CONSTRAINT [FK_tbl_WorkoutHistory_fk_UserId] FOREIGN KEY ([fk_UserId]) REFERENCES [dbo].[tbl_User]([Id]);
GO

-- Step 7: Seed dummy test users
IF NOT EXISTS (SELECT 1 FROM [dbo].[tbl_User] WHERE [Id] = 'user1')
    INSERT INTO [dbo].[tbl_User] ([Id], [UserName]) VALUES ('user1', 'User One');
IF NOT EXISTS (SELECT 1 FROM [dbo].[tbl_User] WHERE [Id] = 'user2')
    INSERT INTO [dbo].[tbl_User] ([Id], [UserName]) VALUES ('user2', 'User Two');
IF NOT EXISTS (SELECT 1 FROM [dbo].[tbl_User] WHERE [Id] = 'user3')
    INSERT INTO [dbo].[tbl_User] ([Id], [UserName]) VALUES ('user3', 'User Three');
GO

-- Step 8: Update spr_GetWorkoutPlans to use NVARCHAR(128) for userId
CREATE OR ALTER PROCEDURE [dbo].[spr_GetWorkoutPlans]
    @workoutPlanId BIGINT = 0,
    @userId        NVARCHAR(128) = NULL,
    @getArchived   BIT = 0
AS
BEGIN
    IF @getArchived = 0
        SELECT *
        FROM tbl_WorkoutPlan
        WHERE tbl_WorkoutPlan.IsArchived = 0
          AND (@workoutPlanId = 0 OR tbl_WorkoutPlan.Id = @workoutPlanId)
          AND (@userId IS NULL OR tbl_WorkoutPlan.fk_UserId = @userId)
    ELSE
        SELECT *
        FROM tbl_WorkoutPlan
        WHERE (@workoutPlanId = 0 OR tbl_WorkoutPlan.Id = @workoutPlanId)
          AND (@userId IS NULL OR tbl_WorkoutPlan.fk_UserId = @userId)
END;
GO

-- Step 9: Update spw_InsertWorkoutPlan to use NVARCHAR(128) for userId
CREATE OR ALTER PROCEDURE [dbo].[spw_InsertWorkoutPlan]
    @workoutPlanName VARCHAR(100),
    @isActive        BIT = 0,
    @isArchived      BIT = 0,
    @userId          NVARCHAR(128),
    @workoutPlanId   BIGINT OUTPUT
AS
    INSERT INTO tbl_WorkoutPlan ([Name], IsActive, IsArchived, fk_UserId)
    VALUES (@workoutPlanName, @isActive, @isArchived, @userId);
    SET @workoutPlanId = SCOPE_IDENTITY();
RETURN @@ROWCOUNT;
GO

-- Step 10: Update spr_GetExercisesDefinitions to use NVARCHAR(128) for userId
CREATE OR ALTER PROCEDURE [dbo].[spr_GetExercisesDefinitions]
    @userId NVARCHAR(128)
AS
    SELECT *
    FROM tbl_ExerciseDefinition
    WHERE tbl_ExerciseDefinition.fk_UserId = @userId;
RETURN @@ROWCOUNT;
GO

-- Step 11: Update spr_GetWorkoutHistory to use NVARCHAR(128) for userId
CREATE OR ALTER PROCEDURE [dbo].[spr_GetWorkoutHistory]
    @userId        NVARCHAR(128),
    @workoutId     BIGINT,
    @workoutPlanId BIGINT,
    @from          DATE,
    @to            DATE
AS
    SELECT *
    FROM tbl_WorkoutHistory AS woh
    WHERE woh.fk_UserId = @userId
      AND (@workoutPlanId IS NULL OR woh.fk_WorkoutPlanId = @workoutPlanId)
      AND (@workoutId IS NULL OR woh.fk_WorkoutId = @workoutId)
      AND CAST(woh.StartDate AS DATE) >= @from
      AND CAST(woh.EndDate AS DATE) <= @to;
RETURN @@ROWCOUNT;
GO

-- Step 12: Update spw_InsertWorkoutHistory to use NVARCHAR(128) for userId
CREATE OR ALTER PROCEDURE [dbo].[spw_InsertWorkoutHistory]
    @startDate        DATETIME2,
    @endDate          DATETIME2,
    @fkWorkoutId      BIGINT,
    @fkWorkoutPlanId  BIGINT,
    @fkUserId         NVARCHAR(128),
    @workoutHistoryId BIGINT OUTPUT
AS
    INSERT INTO tbl_WorkoutHistory(StartDate, EndDate, fk_WorkoutId, fk_WorkoutPlanId, fk_UserId)
    VALUES (@startDate, @endDate, @fkWorkoutId, @fkWorkoutPlanId, @fkUserId);
    SET @workoutHistoryId = SCOPE_IDENTITY();
RETURN @@ROWCOUNT;
GO

-- Step 13: Update spw_InsertExerciseDefinition to use NVARCHAR(128) for userId
CREATE OR ALTER PROCEDURE [dbo].[spw_InsertExerciseDefinition]
    @name            VARCHAR(100),
    @type            SMALLINT,
    @involvedMuscles SMALLINT,
    @userId          NVARCHAR(128),
    @exerciseId      BIGINT OUTPUT
AS
    INSERT INTO tbl_ExerciseDefinition ([Name], [Type], [InvolvedMuscles], [fk_UserId])
    VALUES (@name, @type, @involvedMuscles, @userId);
    SET @exerciseId = SCOPE_IDENTITY();
RETURN @@ROWCOUNT;
GO
