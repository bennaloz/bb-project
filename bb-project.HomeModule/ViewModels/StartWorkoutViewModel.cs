using bb_project.DAL;
using bb_project.DAL.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace bb_project.HomeModule.ViewModels
{
    internal class StartWorkoutViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore workoutsDataStore;

        public string WorkoutPlanName { get; set; }

        public string WorkoutName { get; set; }

        public ICommand StartWorkoutCommand { get; set; }

        public StartWorkoutViewModel(IWorkoutsDataStore workoutsDataStore)
        {
            this.workoutsDataStore = workoutsDataStore;

            WorkoutPlan activeWorkoutPlan = this.workoutsDataStore.GetActiveWorkoutPlanAsync().GetAwaiter().GetResult();
            this.WorkoutPlanName = activeWorkoutPlan.Name;

            //TODO: Get today's workout

            this.StartWorkoutCommand = new DelegateCommand(() => { /*TODO: handle start workout command*/ });
        }
    }
}
