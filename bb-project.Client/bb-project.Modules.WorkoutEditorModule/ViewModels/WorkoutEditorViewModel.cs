
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

        public ObservableCollection<WorkoutPlanViewModel> WorkoutPlansViewModels { get; private set; }
        public WorkoutEditorViewModel(IWorkoutsDataStore workoutDataStore)
        {
            this.workoutDataStore = workoutDataStore;
            this.WorkoutPlansViewModels = new ObservableCollection<WorkoutPlanViewModel>();
            foreach (var item in this.workoutDataStore.GetWorkoutPlansAsync().GetAwaiter().GetResult())
            {
                var workoutPlanViewModel = new WorkoutPlanViewModel(this.workoutDataStore);
                workoutPlanViewModel.WorkoutPlan = item;
                this.WorkoutPlansViewModels.Add(workoutPlanViewModel);
            }
        }

    }
}
