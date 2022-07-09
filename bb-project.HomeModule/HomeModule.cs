using bb_project.DAL;
using bb_project.DAL.Models;
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
            regionManager.RegisterViewWithRegion("ContentMainRegion", typeof(HomeView));

            //if ((await this.workoutDatStore?.HasActiveWorkoutPlanAsync()) ?? false)
            //{
            //    regionManager.RegisterViewWithRegion("HomeRegion", typeof(StartWorkoutView));

            //}

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
