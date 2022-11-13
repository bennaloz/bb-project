using bb_project.Client.Modules.WorkoutAssistantModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace bb_project.Client.Modules.WorkoutAssistantModule
{

    //TODO: creare un servizio per memorizzare i dati dei workout offline e poi allineare sul db cloud quando c'è connessione

    public class WorkoutAssistantModule : IModule
    {
        internal const string WORKOUT_ASSISTANT_MODULE_MAIN_REGION_NAME = "MainWorkoutAssistantRegion";

        private readonly IRegionManager regionManager;

        public WorkoutAssistantModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForRegionNavigation<WorkoutAssistantView>(name: nameof(WorkoutAssistantView));

            containerRegistry.RegisterForRegionNavigation<CardioExerciseView>(name: nameof(CardioExerciseView));
            containerRegistry.RegisterForRegionNavigation<WeightsExerciseView>(name: nameof(WeightsExerciseView));
        }
    }
}
