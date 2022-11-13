using bb_project.Client.Modules.WorkoutAssistantModule.Views;
using bb_project.Infrastructure.Models.Events;
using Prism.Events;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static bb_project.Infrastructure.Models.Events.StartWorkoutEvent;

namespace bb_project.Client.ViewModels
{
    public class ContentMainPageViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public ContentMainPageViewModel(IEventAggregator eventAggregator,
                                        IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.eventAggregator.GetEvent<StartWorkoutEvent>().Subscribe(this.startWorkout);
        }

        private void startWorkout(StartWorkoutEventData startWorkoutData)
        {
            var navParams = new NavigationParameters();
            navParams.Add("userId", startWorkoutData.UserId);
            navParams.Add("workoutId", startWorkoutData.WorkoutId);

            this.regionManager.RequestNavigate("ContentMainPageRegion", nameof(WorkoutAssistantView), navParams);
        }
    }
}