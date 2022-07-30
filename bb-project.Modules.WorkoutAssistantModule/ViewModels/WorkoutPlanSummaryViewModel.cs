using bb_project.DAL;
using bb_project.Infrastructure.BLL;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace bb_project.Modules.WorkoutAssistantModule.ViewModels
{
    internal class WorkoutPlanSummaryViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore workoutsDataStore;
        private readonly IRegionManager regionManager;
        private WorkoutPlan selectedWorkoutPlan;

        public string WorkoutPlanName { get; private set; }

        public IEnumerable<Workout> Workouts { get; private set; }

        public ICommand ShowWorkoutExercisesCommand { get; set; }

        public IEnumerable<WorkoutPlan> WorkoutPlans { get; }


        public WorkoutPlan SelectedWorkoutPlan
        {
            get { return selectedWorkoutPlan; }
            set{SetProperty(ref selectedWorkoutPlan, value);}
        }

        public WorkoutPlanSummaryViewModel(IWorkoutsDataStore workoutsDataStore,
                                           IRegionManager regionManager)
        {
            this.workoutsDataStore = workoutsDataStore;
            this.regionManager = regionManager;

            this.WorkoutPlans = this.workoutsDataStore.GetWorkoutPlansAsync().GetAwaiter().GetResult().ToList();
            this.Workouts = this.workoutsDataStore.GetActiveWorkoutsAsync().GetAwaiter().GetResult();

            this.SelectedWorkoutPlan = this.WorkoutPlans.First(o => o.IsActive);

            this.ShowWorkoutExercisesCommand = new DelegateCommand<WorkoutPlan>(this.goToWorkoutExercisesView);
        }

        private void goToWorkoutExercisesView(WorkoutPlan workoutPlan)
        {
            //TODO: passare workoutplan alla region che visualizza gli esercizi
            this.regionManager.RequestNavigate(WorkoutAssistantModule.WORKOUT_ASSISTANT_MODULE_MAIN_REGION_NAME,
                                               nameof(Views.WorkoutExercisesView));
        }
    }
}
