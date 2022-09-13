using bb_project.Client.Modules.WorkoutEditorModule.ViewModels;
using bb_project.Client.Modules.WorkoutEditorModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Plugin.Popups;
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
            this.regionManager.RegisterViewWithRegion("EditorMainPageRegion", nameof(EditorMainContentView));
            this.regionManager.RegisterViewWithRegion("EditorContentRegion", nameof(WorkoutEditorSummaryView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterPopupDialogService();
            containerRegistry.RegisterForRegionNavigation<EditorMainContentView>(name: nameof(EditorMainContentView));
            containerRegistry.RegisterForRegionNavigation<WorkoutEditorSummaryView>(name: nameof(WorkoutEditorSummaryView));
            containerRegistry.RegisterForRegionNavigation<WorkoutPlanEditorView, WorkoutPlanEditorViewModel>(name: nameof(WorkoutPlanEditorView));
            containerRegistry.RegisterForNavigation<PopupSummaryExerciseView, PopupSummaryExerciseViewModel>();
        }
    }
}
