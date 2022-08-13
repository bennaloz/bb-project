using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutViewModel
    {
        private readonly IWorkoutsDataStore workoutsDataStore;
        public  Workout Workout { get; internal set; }

        public ICommand GetWorkoutExercises { get; set; }

        public string Name => this.Workout?.Name;

        public WorkoutViewModel(IWorkoutsDataStore workoutsDataStore)
        {
            this.workoutsDataStore = workoutsDataStore;

            this.GetWorkoutExercises = new DelegateCommand(this.loadExercises);
        }

        private async void loadExercises()
        {
            var workoutSeries = await this.workoutsDataStore.GetWorkoutSeriesAsync(this.Workout.Id, "1");

        }
    }
}
