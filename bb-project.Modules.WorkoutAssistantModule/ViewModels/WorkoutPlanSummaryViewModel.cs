using bb_project.DAL;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace bb_project.Modules.WorkoutAssistantModule.ViewModels
{
    internal class WorkoutPlanSummaryViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore workoutsDataStore;
        private readonly IRegionManager regionManager;

        public string WorkoutPlanName { get; private set; }

        public IEnumerable<Workout> Workouts { get; private set; }

        public ICommand ShowWorkoutExercisesCommand { get; set; } 

        public WorkoutPlanSummaryViewModel(IWorkoutsDataStore workoutsDataStore,
                                           IRegionManager regionManager)
        {
            this.workoutsDataStore = workoutsDataStore;
            this.regionManager = regionManager;
            this.Workouts = this.workoutsDataStore.GetActiveWorkoutsAsync().GetAwaiter().GetResult();

            this.ShowWorkoutExercisesCommand = new DelegateCommand<Workout>(this.goToWorkoutExercisesView);
        }

        private void goToWorkoutExercisesView(Workout workout)
        {
            //TODO: passare workout alla region che visualizza gli esercizi
            this.regionManager.RequestNavigate(WorkoutAssistantModule.WORKOUT_ASSISTANT_MODULE_MAIN_REGION_NAME, 
                                               nameof(Views.WorkoutExercisesView));
        }
    }
}
