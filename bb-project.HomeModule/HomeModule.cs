using bb_project.HomeModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace bb_project.HomeModule
{
    public class HomeModule : IModule
    {
        private readonly IRegionManager regionManager;

        public  HomeModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion("ContentMainRegion", typeof(HomeView));
            regionManager.RegisterViewWithRegion("HomeRegion", typeof(StartWorkoutView));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
