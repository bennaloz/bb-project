using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutPlanViewModel: BindableBase
    {
        private readonly IWorkoutsDataStore workoutsDataStore;

        public string Name { get; }
        public bool IsActive { get; }
        public List<Workout> Workouts { get; private set; }
        public WorkoutsViewModel WorkoutsViewModel { get; }

        public WorkoutPlanViewModel(IWorkoutsDataStore workoutsDataStore, WorkoutPlan workoutPlan = null)
        {
            this.workoutsDataStore = workoutsDataStore;
            this.WorkoutPlan = workoutPlan;
            this.Name = workoutPlan.Name;  
            this.IsActive = this.WorkoutPlan.IsActive;

            this.Workouts = workoutsDataStore.GetWorkoutsAsync(workoutPlan.ID).GetAwaiter().GetResult().ToList();

            this.WorkoutsViewModel = new WorkoutsViewModel(this.WorkoutPlan.ID, this.workoutsDataStore);
        }

        public WorkoutPlan WorkoutPlan { get; }
    }
}
