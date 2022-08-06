
using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
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

        public List<WorkoutPlanViewModel> WorkoutPlans { get; private set; }


        public WorkoutEditorViewModel(IWorkoutsDataStore workoutDataStore)
        {
            this.workoutDataStore = workoutDataStore;
            this.WorkoutPlans = new List<WorkoutPlanViewModel>();
            foreach (var item in this.workoutDataStore.GetWorkoutPlansAsync().GetAwaiter().GetResult())
            {
                this.WorkoutPlans.Add(new WorkoutPlanViewModel(this.workoutDataStore, item));
            }
        }

    }
}
