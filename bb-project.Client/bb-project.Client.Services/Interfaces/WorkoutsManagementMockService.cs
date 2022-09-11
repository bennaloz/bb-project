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


        IEnumerable<Workout> workouts;
        IEnumerable<SeriesGroup> seriesGroups;
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

            var seriesGroup1 = new SeriesGroup(1, ExerciseMethodology.Single);
            seriesGroup1.Series.Add(new Serie(1, new ExerciseDefinition(1) { Name = "Panca Piana", Type = ExerciseType.Weights }));
            seriesGroup1.Series.Add(new Serie(2, new ExerciseDefinition(1) { Name = "Panca Piana", Type = ExerciseType.Weights }));
            seriesGroup1.Series.Add(new Serie(3, new ExerciseDefinition(1) { Name = "Panca Piana", Type = ExerciseType.Weights }));
            seriesGroup1.Series.Add(new Serie(4, new ExerciseDefinition(1) { Name = "Panca Piana", Type = ExerciseType.Weights }));

            var seriesGroup2 = new SeriesGroup(2, ExerciseMethodology.JumpSet);
            seriesGroup2.Series.Add(new Serie(5, new ExerciseDefinition(1) { Name = "Squat", Type = ExerciseType.Weights }));
            seriesGroup2.Series.Add(new Serie(6, new ExerciseDefinition(1) { Name = "Tapis Roulant", Type = ExerciseType.Cardio }));
            seriesGroup2.Series.Add(new Serie(7, new ExerciseDefinition(1) { Name = "Squat", Type = ExerciseType.Weights }));
            seriesGroup2.Series.Add(new Serie(8, new ExerciseDefinition(1) { Name = "Tapis Roulant", Type = ExerciseType.Cardio })); 
            seriesGroup2.Series.Add(new Serie(9, new ExerciseDefinition(1) { Name = "Squat", Type = ExerciseType.Weights }));
            seriesGroup2.Series.Add(new Serie(10, new ExerciseDefinition(1) { Name = "Tapis Roulant", Type = ExerciseType.Cardio }));
            this.seriesGroups = new ObservableCollection<SeriesGroup>()
            {
                seriesGroup1,
                seriesGroup2
            };

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


        public async Task<IEnumerable<Workout>> GetActiveWorkoutsAsync()
        {
            return await Task.FromResult(workouts);
        }

        public Task<Workout> GetNextWorkoutAsync(string userId, ulong activeWorkoutPlanId)
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<SeriesGroup>> GetWorkoutSeriesGroupsAsync(ulong workoutId, string userId)
        {
            return await Task.FromResult(seriesGroups);
        }

        public async Task<bool?> HasActiveWorkoutPlanAsync()
        {
            return await Task.FromResult(this.plans.ToList().First(o => o.IsActive) != default); 
        }

        public Task<ulong> InsertExerciseDefinitionAsync(ExerciseDefinition exercise)
        {
            throw new NotImplementedException();
        }

        public Task InsertSeriesGroupsAsync(ulong workoutId, IEnumerable<SeriesGroup> seriesGroups)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> InsertWorkoutAsync(ulong workoutPlanId, string workoutName)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> InsertWorkoutPlanAsync(string workoutPlanName,ulong id, bool isActive = false)
        {
            var plan = plans.First(o => o.Id == id);
            
            if(plan == default)
            {
                plans.ToList().Add(new WorkoutPlan(id) { IsActive = isActive, Name= workoutPlanName});    
            }
            else
            {
                plan.Name = workoutPlanName;
            }

            return Task.FromResult((ulong)0);
        
        }
    }
}