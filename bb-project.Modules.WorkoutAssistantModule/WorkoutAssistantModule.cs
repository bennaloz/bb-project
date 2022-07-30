using bb_project.Modules.WorkoutAssistantModule.ViewModels;
using bb_project.Modules.WorkoutAssistantModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace bb_project.Modules.WorkoutAssistantModule
{

    //TODO: creare un servizio per memorizzare i dati dei workout offline e poi allineare sul db cloud quando c'è connessione

    public class WorkoutAssistantModule : IModule
    {
        public const string WORKOUT_ASSISTANT_MODULE_MAIN_REGION_NAME = "MainWorkoutAssistantRegion";

        private readonly IRegionManager regionManager;

        public WorkoutAssistantModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.regionManager.RegisterViewWithRegion(WORKOUT_ASSISTANT_MODULE_MAIN_REGION_NAME, typeof(WorkoutPlanSummaryView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForRegionNavigation<WorkoutAssistantView>(name: nameof(WorkoutAssistantView));
            containerRegistry.RegisterForRegionNavigation<WorkoutPlanSummaryView>(name: nameof(WorkoutPlanSummaryView));
            containerRegistry.RegisterForRegionNavigation<WorkoutExercisesView>(name: nameof(WorkoutExercisesView));

            containerRegistry.RegisterForRegionNavigation<CardioExerciseView>(name: nameof(CardioExerciseView));
            containerRegistry.RegisterForRegionNavigation<WeightsExerciseView>(name: nameof(WeightsExerciseView));
        }
    }
}
