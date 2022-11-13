using bb_project.Client.Services;


using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bb_project.Client.Modules.WorkoutAssistantModule.ViewModels
{
    public class WorkoutAssistantViewModel : BindableBase, IRegionAware
    {
        private readonly IWorkoutsManagementService workoutsDataStore;
        private readonly IRegionManager regionManager;

        private WorkoutPlan selectedWorkoutPlan;
        public WorkoutPlan SelectedWorkoutPlan
        {
            get { return selectedWorkoutPlan; }
            set
            {
                SetProperty(ref selectedWorkoutPlan, value);
            }
        }
        public WorkoutAssistantViewModel(IWorkoutsManagementService workoutsDataStore,
                                         IRegionManager regionManager)
        {
            this.workoutsDataStore = workoutsDataStore;
            this.regionManager = regionManager;
        }

        public async void OnNavigatedTo(INavigationContext navigationContext)
        {
            navigationContext.Parameters.TryGetValue<string>("userId", out string userId);
            navigationContext.Parameters.TryGetValue<ulong>("workoutId", out ulong workoutId);
            ulong? currentSerieId = null;
            if (navigationContext.Parameters.TryGetValue<ulong>("currentSerieId", out ulong tmp))
                currentSerieId = tmp;

            var nextExercise = default(Exercise);
            var woExerciseGroups = await this.workoutsDataStore.GetWorkoutExercisesGroupsAsync(workoutId, userId);

            if(currentSerieId.HasValue)
            {
                bool serieFound = false;

                var allExercises = woExerciseGroups.SelectMany(g => g.Exercises.Values);
                var allSeries = allExercises.SelectMany(e => e.Series).ToList();
                var currentSerieIndex = allSeries.FindIndex(s => s.Id == currentSerieId);

                if((currentSerieIndex + 1) == allSeries.Count)
                {
                    //TODO: scheda finita?!?
                }
                else
                {
                    var nextSerie = allSeries.ElementAtOrDefault(currentSerieIndex + 1);
                    nextExercise = allExercises.First(e => e.Series.Any(s => s.Id == nextSerie.Id));
                }
            }
            else
                nextExercise = woExerciseGroups.First().Exercises.Values.First();
            
            
            switch (nextExercise.Type)
            {
                case Infrastructure.Models.Enums.ExerciseType.Cardio:
                    this.regionManager.RequestNavigate("MainWorkoutAssistantRegion", nameof(Views.CardioExerciseView));
                    break;
                case Infrastructure.Models.Enums.ExerciseType.Weights:
                    this.regionManager.RequestNavigate("MainWorkoutAssistantRegion", nameof(Views.WeightsExerciseView));
                    break;
            }

        }

        public bool IsNavigationTarget(INavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(INavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
