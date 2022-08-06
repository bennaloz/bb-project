using bb_project.Authentication;
using bb_project.Client.Services;

using bb_project.HomeModule.Models;

using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.Modules.HomeModule.ViewModels
{
    internal class StartWorkoutViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWorkoutsDataStore workoutDataStore;
        private readonly IUserAuthenticatorService userAuthenticator;

        public ICommand StartWorkoutCommand { get; set; }

        public string WorkoutPlanName { get; set; }

        public string WorkoutName { get; set; }

        private IEnumerable<WorkoutExercise> Exercises { get; set; }

        public StartWorkoutViewModel(IEventAggregator eventAggregator,
                                     IWorkoutsDataStore workoutDataStore,
                                     IUserAuthenticatorService userAuthenticator)
        {
            this.eventAggregator = eventAggregator;
            this.workoutDataStore = workoutDataStore;
            this.userAuthenticator = userAuthenticator;

            this.StartWorkoutCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<StartWorkoutEvent>().Publish();
            });
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {
            var hasActiveWorkoutPlan = await this.workoutDataStore.HasActiveWorkoutPlanAsync();
            if (hasActiveWorkoutPlan ?? false)
            {
                var workoutPlans = await this.workoutDataStore.GetWorkoutPlansAsync();
                var activeWOPlan = workoutPlans.FirstOrDefault(wop => wop.IsActive);
                this.WorkoutPlanName = activeWOPlan?.Name;
                var userId = this.userAuthenticator.UserId;
                var userWorkoutsHistory = await this.workoutDataStore.GetWorkoutHistoryItems(userId, workoutPlanId: activeWOPlan.ID, from: DateTime.Now.AddDays(-14));
                var previousWorkoutId = (userWorkoutsHistory?.Count() ?? 0) > 0 ? userWorkoutsHistory.OrderBy(woh => woh.StartDate).Last().WorkoutId
                                                                            : 0;
                var nextWorkout = await this.workoutDataStore.GetNextWorkoutAsync(userId, activeWOPlan.ID);

                this.WorkoutName = nextWorkout.Name;

                var nextWorkoutSeries = await this.workoutDataStore.GetWorkoutSeriesAsync(nextWorkout.Id, userId);
                this.Exercises = nextWorkoutSeries.Select(ex =>
                {
                    return new WorkoutExercise
                    {

                    };
                });
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
