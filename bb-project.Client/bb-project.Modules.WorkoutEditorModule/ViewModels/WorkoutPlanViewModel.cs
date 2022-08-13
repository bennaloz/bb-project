using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutPlanViewModel : BindableBase
    {
        private readonly IWorkoutsManagementService workoutsDataStore;
        private WorkoutPlan workoutPlan;

        public string Name => this.workoutPlan?.Name;
        public bool IsActive => this.workoutPlan?.IsActive ?? false;
        public ObservableCollection<WorkoutViewModel> WorkoutsViewModels { get; } = new ObservableCollection<WorkoutViewModel>();
        public WorkoutPlan WorkoutPlan { get => workoutPlan; internal set => workoutPlan = value; }

        public ICommand GetWorkoutsCommand { get; set; }

        public WorkoutPlanViewModel(IWorkoutsManagementService workoutsDataStore)
        {
            this.workoutsDataStore = workoutsDataStore;

            this.GetWorkoutsCommand = new DelegateCommand(this.loadWorkouts);
        }

        private async void loadWorkouts()
        {
            var workouts = await this.workoutsDataStore.GetWorkoutsAsync(workoutPlan.ID);
            foreach (var wo in workouts)
            {
                var woViewModel = new WorkoutViewModel(this.workoutsDataStore);
                woViewModel.Workout = wo;
                this.WorkoutsViewModels.Add(woViewModel);
            }
        }
    }
}
