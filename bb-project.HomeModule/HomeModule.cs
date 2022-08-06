using bb_project.DAL;
using bb_project.Infrastructure.BLL;
using bb_project.Modules.HomeModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace bb_project.Modules.HomeModule
{
    public class HomeModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IWorkoutsDataStore workoutDatStore;

        public  HomeModule(IRegionManager regionManager, IWorkoutsDataStore workoutDatStore)
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
