using bb_project.DAL;
using bb_project.DAL.Models;
using bb_project.Infrastructure.Models.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace bb_project.Modules.HomeModule.ViewModels
{
    internal class StartWorkoutViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore workoutsDataStore;
        private readonly IEventAggregator eventAggregator;

        public string WorkoutPlanName { get; set; }

        public string WorkoutName { get; set; }

        public ICommand StartWorkoutCommand { get; set; }

        public StartWorkoutViewModel(IWorkoutsDataStore workoutsDataStore,
                                     IEventAggregator eventAggregator)
        {
            this.workoutsDataStore = workoutsDataStore;
            this.eventAggregator = eventAggregator;

            //WorkoutPlan activeWorkoutPlan = this.workoutsDataStore.GetActiveWorkoutPlanAsync().GetAwaiter().GetResult();
            //this.WorkoutPlanName = activeWorkoutPlan.Name;

            //TODO: Get today's workout

            this.StartWorkoutCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<StartWorkoutEvent>().Publish();
            });
        }
    }
}
