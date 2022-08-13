using bb_project.Client.Modules.HomeModule.Views;
using bb_project.Client.Services;


using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace bb_project.Client.Modules.HomeModule
{
    public class HomeModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IWorkoutsManagementService workoutDatStore;

        public  HomeModule(IRegionManager regionManager, IWorkoutsManagementService workoutDatStore)
        {
            this.regionManager = regionManager;
            this.workoutDatStore = workoutDatStore;
        }
        public async void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion("ContentMainPageRegion", typeof(HomeView));

            if ((await this.workoutDatStore?.HasActiveWorkoutPlanAsync()) ?? false)
            {
                regionManager.RegisterViewWithRegion("HomeRegion", typeof(StartWorkoutView));
            }
            else
                regionManager.RegisterViewWithRegion("HomeRegion", typeof(CreateWorkoutView));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
