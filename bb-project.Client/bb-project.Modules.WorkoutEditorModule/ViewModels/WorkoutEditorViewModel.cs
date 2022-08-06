
using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutEditorViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore workoutDataStore;

        public IEnumerable<WorkoutPlan> WorkoutPlans { get; }

        public Dictionary<long,IEnumerable<Workout>> WorkoutPlansChildren { get; }

        public WorkoutEditorViewModel(IWorkoutsDataStore workoutDataStore)
        {
            this.workoutDataStore = workoutDataStore;
            this.WorkoutPlans = this.workoutDataStore.GetWorkoutPlansAsync().GetAwaiter().GetResult();
            this.WorkoutPlansChildren = new Dictionary<long, IEnumerable<Workout>>();
            foreach (var item in this.WorkoutPlans)
            {
                var workout = this.workoutDataStore.GetWorkoutsAsync(item.ID).GetAwaiter().GetResult();
                if(workout != default)
                {
                    this.WorkoutPlansChildren.Add(item.ID,workout);
                }
            }
        }



    }
}
