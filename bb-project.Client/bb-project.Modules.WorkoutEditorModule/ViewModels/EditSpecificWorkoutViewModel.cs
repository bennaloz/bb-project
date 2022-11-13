using bb_project.Client.Modules.WorkoutEditorModule.Views;
using bb_project.Client.Services;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class EditSpecificWorkoutViewModel : BindableBase, IRegionAware
    {
        private readonly IWorkoutsManagementService workoutsManagementService;
        private readonly IRegionManager regionManager;

        private WorkoutStructure workoutStructure;
        public WorkoutStructure WorkoutStructure
        {
            get
            {
                return workoutStructure;
            }
            set
            {
                SetProperty(ref workoutStructure, value);
            }
        }
        public EditSpecificWorkoutViewModel(IWorkoutsManagementService workoutsManagementService, IRegionManager regionManager)
        {
            this.workoutsManagementService = workoutsManagementService;
            this.regionManager = regionManager;
        }

        public bool IsNavigationTarget(INavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(INavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationContext navigationContext)
        {
            navigationContext.Parameters.TryGetValue("selected-workout", out WorkoutStructure workout);
            if (workout == null)
            {
                this.regionManager.RequestNavigate("EditorContentRegion", nameof(WorkoutEditorView));
                return;
            }

            this.WorkoutStructure = workout;
        }
    }
}
