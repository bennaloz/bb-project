using bb_project.DAL;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Modules.WorkoutAssistantModule.ViewModels
{
    public class WorkoutAssistantViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore workoutsDataStore;
        private readonly IRegionManager regionManager;

        public WorkoutAssistantViewModel(IWorkoutsDataStore workoutsDataStore,
                                         IRegionManager regionManager)
        {
            this.workoutsDataStore = workoutsDataStore;
            this.regionManager = regionManager;

            //TODO: mostrare tutti i workout disponibili evidenziando il successivo rispetto l'ultimo fatto
        }
    }
}
