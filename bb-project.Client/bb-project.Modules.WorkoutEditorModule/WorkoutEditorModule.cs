using bb_project.Client.Modules.WorkoutEditorModule.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace bb_project.Client.Modules.WorkoutEditorModule
{
    public class WorkoutEditorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForRegionNavigation<WorkoutEditorView>(name: nameof(WorkoutEditorView));
            containerRegistry.RegisterForRegionNavigation<WorkoutPlansSummaryView>(name: nameof(WorkoutPlansSummaryView));
        }
    }
}
