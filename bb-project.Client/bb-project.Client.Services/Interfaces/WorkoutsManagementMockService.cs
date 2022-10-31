using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace bb_project.Client.Services
{

    public class WorkoutsManagementMockService : IWorkoutsManagementService
    {
        IEnumerable<ExerciseGroup> exerciseGroups;
        IEnumerable<Workout> workouts;
        IEnumerable<WorkoutPlan> plans;
        Dictionary<ulong, IEnumerable<Workout>> workoutPlanChildren;
        public WorkoutsManagementMockService()
        {
            this.plans = new ObservableCollection<WorkoutPlan>()
            {
               new WorkoutPlan(1) { Name ="MASSA", IsActive=true},
                new WorkoutPlan(2) { Name ="DEFINIZIONE", IsActive=false},
                new WorkoutPlan(3) { Name ="FORZA",IsActive =false}
            };

            this.workouts = new ObservableCollection<Workout>()
            {
                new Workout(1) { Name ="Scheda A"},
                new Workout(2) { Name ="Scheda B"},
                new Workout(3) { Name ="Scheda C"}
            };


            ExerciseGroup benchGroup = BenchGroupFiller();
            ExerciseGroup rowSquatGroup = RowSquatGroupFiller();

            this.exerciseGroups = new List<ExerciseGroup>() { benchGroup, rowSquatGroup };


            this.workoutPlanChildren = new Dictionary<ulong, IEnumerable<Workout>>();
            foreach (var item in this.plans)
            {
                List<Workout> workouts = new List<Workout>()
                {
                    new Workout(1){Name = "Scheda A"},
                    new Workout(1){Name = "Scheda B"},
                    new Workout(1){Name = "Scheda C"}
                };
                this.workoutPlanChildren.Add(item.Id, workouts);
            }

        }

        private static ExerciseGroup RowSquatGroupFiller()
        {
            ExerciseGroup rowSquatGroup = new ExerciseGroup(2, ExerciseMethodology.SuperSet);
            List<Serie> squatSerie = new List<Serie>()
            {
                new Serie(1) { Reps = 8, Rest = new TimeSpan(0,1,30) },
                new Serie(1) { Reps = 8, Rest = new TimeSpan(0,1,30) },
                new Serie(1) { Reps = 8, Rest = new TimeSpan(0,1,30) },
            };

            List<Serie> rowSerie = new List<Serie>()
            {
                new Serie(1) { Reps = 10, Rest = new TimeSpan(0,1,30) },
                new Serie(1) { Reps = 10, Rest = new TimeSpan(0,1,30) },
                new Serie(1) { Reps = 10, Rest = new TimeSpan(0,1,30) },
            };

            Exercise squatExercise = new Exercise(new ExerciseDefinition(2)
            {
                InvolvedMuscles = InvolvedMuscles.Quadriceps,
                Name = "Squat",
                Type = ExerciseType.Weights
            });
            squatExercise.Series.AddRange(squatSerie);

            Exercise rowExercise = new Exercise(new ExerciseDefinition(2)
            {
                InvolvedMuscles = InvolvedMuscles.Quadriceps,
                Name = "Row",
                Type = ExerciseType.Weights
            });
            rowExercise.Series.AddRange(rowSerie);
            
            rowSquatGroup.Exercises.Add(3, squatExercise);
            rowSquatGroup.Exercises.Add(4, rowExercise);
            
            return rowSquatGroup;
        }

        private static ExerciseGroup BenchGroupFiller()
        {
            ExerciseGroup benchGroup;
            List<Serie> benchGroupSeries = new List<Serie>()
            {
                new Serie(1) { Reps = 10, Rest = new TimeSpan(0,1,30) },
                new Serie(1) { Reps = 8, Rest = new TimeSpan(0,1,30) },
                new Serie(1) { Reps = 6, Rest = new TimeSpan(0,1,30) },
            };

            Exercise benchGroupExercise = new Exercise(new ExerciseDefinition()
            { Name = "Bench Press", Type = ExerciseType.Weights, InvolvedMuscles = InvolvedMuscles.Pectorals });
            benchGroupExercise.Series.AddRange(benchGroupSeries);

            benchGroup = new ExerciseGroup(1, ExerciseMethodology.Single);
            benchGroup.Exercises.Add(1, benchGroupExercise);
            return benchGroup;
        }

        public async Task<IEnumerable<Workout>> GetActiveWorkoutsAsync()
        {
            return await Task.FromResult(workouts);
        }

        public Task<Workout> GetNextWorkoutAsync(string userId, ulong activeWorkoutPlanId)
        {
            return Task.FromResult(workouts.First());
        }

        public async Task<IEnumerable<ExerciseGroup>> GetWorkoutExercisesGroupsAsync(ulong workoutId, string userId)
        {
            return await Task.FromResult(this.exerciseGroups);
        }

        public Task<IEnumerable<WorkoutHistoryItem>> GetWorkoutHistoryItems(string userId, ulong? workoutPlanId = null, ulong? workoutId = null, DateTime from = default, DateTime to = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkoutPlan>> GetWorkoutPlansAsync(ulong? id = null)
        {
            return await Task.FromResult(plans);
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync(ulong workoutPlanId, ulong? workoutId = null)
        {
            IEnumerable<Workout> result = new List<Workout>();
            foreach (var item in workoutPlanChildren)
            {
                if (item.Key == workoutPlanId)
                {
                    result = item.Value;
                }
            }
            return await Task.FromResult(result);
        }

        

        public async Task<bool?> HasActiveWorkoutPlanAsync()
        {
            return await Task.FromResult(this.plans.ToList().First(o => o.IsActive) != default);
        }

        public Task<ulong> InsertExerciseDefinitionAsync(ExerciseDefinition exercise)
        {
            throw new NotImplementedException();
        }


        public Task InsertExercisesGroupsAsync(ulong workoutId, IEnumerable<ExerciseGroup> exercisesGroups)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, string workoutName)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> InsertWorkoutPlanAsync(string workoutPlanName, ulong id, bool isActive = false)
        {
            var plan = plans.First(o => o.Id == id);

            if (plan == default)
            {
                plans.ToList().Add(new WorkoutPlan(id) { IsActive = isActive, Name = workoutPlanName });
            }
            else
            {
                plan.Name = workoutPlanName;
            }

            return Task.FromResult((ulong)0);

        }
    }
}