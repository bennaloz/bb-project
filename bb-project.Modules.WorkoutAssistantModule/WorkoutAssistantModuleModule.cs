using bb_project.Modules.WorkoutAssistantModule.ViewModels;
using bb_project.Modules.WorkoutAssistantModule.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace bb_project.Modules.WorkoutAssistantModule
{
    public class WorkoutAssistantModuleModule : IModule
    {
        public WorkoutAssistantModuleModule()
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForRegionNavigation<WorkoutAssistantView>(name: "WorkoutAssistantView");
        }
    }
}
