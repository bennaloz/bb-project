Backend Operations Coverage
===========================

Scope: verify whether the backend handles all expected workout operations and list what is currently available.

Currently exposed via API (`WorkoutsEditorController`)
------------------------------------------------------
- `GET /workoutsplans?workoutPlanId` → list workout plans (optional id filter).
- `GET /workouts?workoutPlanId&workoutId` → list workouts inside a plan (optional workout id).
- `GET /workoutseriesgroups?workoutId&userId` → list exercise groups/series for a workout.
- `GET /activeworkouts` → list active workouts.
- `GET /nextworkout?userId&activeWorkoutPlanId` → get next workout for a user.
- `GET /hasactiveworkout` → check if any active plan exists.
- `POST /insertWOPlan?userId` (body: `WorkoutPlan`) → create workout plan.
- `POST /insertWO?workoutPlanId` (body: `Workout`) → create workout within a plan.
- `POST /insertExercise?userId` (body: `ExerciseDefinition`) → create exercise definition.
- `POST /insertSeriesGroups?workoutId` (body: `ExerciseGroup[]`) → create exercise groups/series for a workout.
- `POST /insertWOHistory` (body: `InsertWorkoutHistoryDTO`) → add workout history entry.
- `POST /insertWOData` (body: `InsertWorkoutDataDTO`) → add workout data rows for history items.

Supported in BLL/DAL but not exposed as API endpoints
-----------------------------------------------------
- Read: `GetExerciseDefinitionsAsync`, `GetWorkoutHistoryItems`.
- Update: `UpdateWorkoutPlanAsync`, `UpdateWorkoutAsync`, `UpdateExerciseDefinitionAsync`.
- Delete: `DeleteWorkoutSeriesAsync`.

Stored procedures present but unused in code
--------------------------------------------
- Deletes: `spw_DeleteWorkoutPlan`, `spw_DeleteWorkout`, `spw_DeleteExercise`.
- Checks: `spw_IsExerciseUsedInAnyWorkout`.

Gaps / incomplete coverage
--------------------------
- No API endpoints for: listing exercise definitions, listing workout history, updating plans/workouts/exercises, deleting series/workouts/plans/exercises, or checking if an exercise is used.
- DAL exposes delete/update operations, but they are not surfaced through the BLL interface or the API controller (except `DeleteWorkoutSeriesAsync` which is BLL-only). Implementing endpoints for these would be required for full CRUD coverage.
