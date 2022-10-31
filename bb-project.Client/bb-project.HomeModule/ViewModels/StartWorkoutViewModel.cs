using bb_project.Authentication;
using bb_project.Client.Services;

using bb_project.HomeModule.Models;

using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.Modules.HomeModule.ViewModels
{
    internal class ViewExercise
    {
        public ViewExercise()
        {
            this.ExerciseName = string.Empty;
            this.SeriesNumber = string.Empty;
            this.Reps = string.Empty;
            this.Rest = string.Empty;
        }

        public string ExerciseName { get; set; }

        public string Reps { get; set; }
        public string SeriesNumber { get; set; }
        public string Rest { get; set; }

    }
    internal class ViewGroupExercise
    {
        public ViewGroupExercise()
        {
            this.ViewExercises = new ObservableCollection<ViewExercise>();
        }

        public ObservableCollection<ViewExercise> ViewExercises { get; set; }

    }



    internal class StartWorkoutViewModel : BindableBase, IRegionAware
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWorkoutsManagementService workoutDataStore;
        private readonly IUserAuthenticatorService userAuthenticator;

        public ICommand StartWorkoutCommand { get; set; }

        public string WorkoutPlanName { get; set; }

        public string WorkoutName { get; set; }

        public ObservableCollection<ViewGroupExercise> Exercises { get; set; }

        public StartWorkoutViewModel(IEventAggregator eventAggregator,
                                     IWorkoutsManagementService workoutDataStore,
                                     IUserAuthenticatorService userAuthenticator)
        {
            this.eventAggregator = eventAggregator;
            this.workoutDataStore = workoutDataStore;
            this.userAuthenticator = userAuthenticator;

            this.StartWorkoutCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<StartWorkoutEvent>().Publish();
            });
            this.Exercises = new ObservableCollection<ViewGroupExercise>();

            InitializeProperties();

        }

        private async void InitializeProperties()
        {
            var hasActiveWorkoutPlan = await this.workoutDataStore.HasActiveWorkoutPlanAsync();
            if (hasActiveWorkoutPlan ?? false)
            {
                var workoutPlans = await this.workoutDataStore.GetWorkoutPlansAsync();
                var activeWOPlan = workoutPlans.FirstOrDefault(wop => wop.IsActive);
                WorkoutPlanName = activeWOPlan?.Name;
                var userId = this.userAuthenticator.UserId;
                //var userWorkoutsHistory = await this.workoutDataStore.GetWorkoutHistoryItems(userId, workoutPlanId: activeWOPlan?.Id, from: DateTime.Now.AddDays(-14));
                //var previousWorkoutId = (userWorkoutsHistory?.Count() ?? 0) > 0 ? userWorkoutsHistory.OrderBy(woh => woh.StartDate).Last().WorkoutId : 0;
                var nextWorkout = await this.workoutDataStore.GetNextWorkoutAsync(userId, activeWOPlan?.Id ?? 0);


                this.WorkoutName = nextWorkout.Name;

                var nextWorkoutSeries = await this.workoutDataStore.GetWorkoutExercisesGroupsAsync(nextWorkout.Id, userId);



                foreach (var item in nextWorkoutSeries)
                {
                    ViewGroupExercise viewGroup = new ViewGroupExercise();
                    foreach (var ex in item.Exercises)
                    {
                        ViewExercise viewExercise = new ViewExercise();
                        viewExercise.ExerciseName = ex.Value.Name;
                        int seriesCount = 0;
                        foreach (var serie in ex.Value.Series)
                        {
                            viewExercise.Reps = serie.Reps.ToString();
                            viewExercise.Rest = serie.Rest.ToString();
                            seriesCount++;
                        }
                        viewExercise.SeriesNumber = seriesCount.ToString();
                        viewGroup.ViewExercises.Add(viewExercise);
                    }

                    this.Exercises.Add(viewGroup);

                }

            }
        }

        public void OnNavigatedTo(INavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(INavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(INavigationContext navigationContext)
        {
           
        }
    }
}
