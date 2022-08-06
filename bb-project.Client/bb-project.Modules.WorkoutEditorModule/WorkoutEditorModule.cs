using bb_project.Client.Modules.WorkoutEditorModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace bb_project.Client.Modules.WorkoutEditorModule
{
    public class WorkoutEditorModule : IModule
    {
        private readonly IRegionManager regionManager;

        public WorkoutEditorModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.regionManager.RegisterViewWithRegion("EditMainPageRegion", nameof(WorkoutEditorView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForRegionNavigation<WorkoutEditorView>(name: nameof(WorkoutEditorView));
            containerRegistry.RegisterForRegionNavigation<WorkoutPlansSummaryView>(name: nameof(WorkoutPlansSummaryView));
        }
    }
}
