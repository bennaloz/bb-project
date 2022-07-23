using bb_project.DAL;
using bb_project.Infrastructure.Models.Data;
using bb_project.Modules.WorkoutAssistantModule.Views;
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

    internal class WorkoutExercisesViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IWorkoutsDataStore workoutsDataStore;
        private readonly Workout currentWorkout;

        public ICommand StartWorkoutCommand { get; set; }

        public WorkoutExercisesViewModel(IRegionManager regionManager,
                                         IWorkoutsDataStore workoutsDataStore)
        {
            this.regionManager = regionManager;
            this.workoutsDataStore = workoutsDataStore;

            this.StartWorkoutCommand = new DelegateCommand(this.startWorkoutCommand);
        }

        private async void startWorkoutCommand()
        {
            var firstExerciseType = (await this.workoutsDataStore.GetWorkoutSeriesAsync(1,"a")).First().OwnerExercise.Type;
            switch (firstExerciseType)
            {
                case Infrastructure.Models.Enums.ExerciseType.Cardio:
                    this.regionManager.RequestNavigate(WorkoutAssistantModule.WORKOUT_ASSISTANT_MODULE_MAIN_REGION_NAME, nameof(CardioExerciseView));
                    break;
                case Infrastructure.Models.Enums.ExerciseType.Weights:
                    this.regionManager.RequestNavigate(WorkoutAssistantModule.WORKOUT_ASSISTANT_MODULE_MAIN_REGION_NAME, nameof(WeightsExerciseView));
                    break;
            }


        }
    }
}
