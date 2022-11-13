using bb_project.Client.Modules.WorkoutEditorModule.Views;
using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{





    internal class WorkoutEditorViewModel : BindableBase
    {
        public ObservableCollection<PlanStructure> PlanStructures { get; set; }



        private readonly IWorkoutsManagementService workoutDataStore;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService service;
        public ICommand EditWorkoutCommand { get; set; }


        public WorkoutEditorViewModel(
            IWorkoutsManagementService workoutDataStore,
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            INavigationService service)
        {
            this.workoutDataStore = workoutDataStore;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.service = service;
            this.EditWorkoutCommand = new DelegateCommand<WorkoutStructure>(editSpecificWorkoutCommand);
            Load();
        }

        private void editSpecificWorkoutCommand(WorkoutStructure selectedWorkout)
        {
            INavigationParameters parameters = new NavigationParameters();
            if(selectedWorkout != null)
            {
                parameters.Add("selected-workout", selectedWorkout);
            }

            this.regionManager.RequestNavigate("EditorContentRegion", nameof(EditSpecificWorkoutView),parameters);
        }

        async void Load()
        {
            this.PlanStructures = new ObservableCollection<PlanStructure>();

            var workoutPlans = await this.workoutDataStore.GetWorkoutPlansAsync();


            foreach (var workoutPlan in workoutPlans)
            {
                PlanStructure planStructure = new PlanStructure();
                planStructure.WorkoutPlan = workoutPlan;

                var workouts = await this.workoutDataStore.GetWorkoutsAsync(workoutPlan.Id);

                foreach (var workout in workouts)
                {
                    WorkoutStructure workoutStructure = new WorkoutStructure();
                    workoutStructure.Workout = workout;

                    var exerciseGroups = await this.workoutDataStore.GetWorkoutExercisesGroupsAsync(workout.Id, "Pigna");

                    foreach (var exGroup in exerciseGroups)
                    {
                        ExerciseGroupStructure exStruct = new ExerciseGroupStructure(exGroup);
                        workoutStructure.ExGroups.Add(exStruct);
                    }

                    planStructure.Workouts.Add(workoutStructure);
                }
                this.PlanStructures.Add(planStructure);

            }







        }


    }
}
