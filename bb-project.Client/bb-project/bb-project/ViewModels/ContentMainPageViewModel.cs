using bb_project.Client.Modules.WorkoutAssistantModule.Views;
using bb_project.Infrastructure.Models.Events;
using Prism.Events;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace bb_project.ViewModels
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

        private void startWorkout()
        {
            this.regionManager.RequestNavigate("ContentMainPageRegion", nameof(WorkoutAssistantView));
        }
    }
}