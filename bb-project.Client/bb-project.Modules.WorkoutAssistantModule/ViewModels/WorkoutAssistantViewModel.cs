using bb_project.Client.Services;


using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bb_project.Client.Modules.WorkoutAssistantModule.ViewModels
{
    public class WorkoutAssistantViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore workoutsDataStore;
        private readonly IRegionManager regionManager;

        private WorkoutPlan selectedWorkoutPlan;
        public WorkoutPlan SelectedWorkoutPlan
        {
            get { return selectedWorkoutPlan; }
            set
            {
                SetProperty(ref selectedWorkoutPlan, value);
            }
        }
        public WorkoutAssistantViewModel(IWorkoutsDataStore workoutsDataStore,
                                         IRegionManager regionManager)
        {
            this.workoutsDataStore = workoutsDataStore;
            this.regionManager = regionManager;
        }
    }
}
