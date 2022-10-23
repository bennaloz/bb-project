DELETE FROM tbl_SeriesGroup_Serie
DELETE FROM tbl_SeriesGroup
UPDATE tbl_WorkoutData SET fk_SerieId = NULL, fk_WorkoutHistoryId = NULL
DELETE FROM tbl_Serie
DELETE FROM tbl_WorkoutData
DELETE FROM tbl_ExerciseDefinition
DELETE FROM tbl_WorkoutHistory
DELETE FROM tbl_Workout
DELETE FROM tbl_WorkoutPlan