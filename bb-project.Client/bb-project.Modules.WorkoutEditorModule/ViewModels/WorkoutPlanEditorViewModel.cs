using bb_project.Client.Modules.WorkoutEditorModule.Views;
using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Enums;
using bb_project.Infrastructure.Models.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class EditorListItemViewModel : BindableBase
    {
        public ulong Id { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }

        }

        public class WorkoutPlanEditorViewModel : BindableBase, INavigationAware
        {
            private readonly IWorkoutsManagementService workoutDataStore;
            private readonly IRegionManager regionManager;
            private readonly IEventAggregator eventAggregator;
            public ObservableCollection<EditorListItemViewModel> Items { get; set; }
            public DelegateCommand BackCommand { get; set; }
            public DelegateCommand SaveCommand { get; set; }
            private ViewState state;
            public WorkoutPlanEditorViewModel( IWorkoutsManagementService workoutDataStore, IRegionManager regionManager, IEventAggregator eventAggregator)
            {
                this.workoutDataStore = workoutDataStore;
                this.regionManager = regionManager;
                this.eventAggregator = eventAggregator;
                this.BackCommand = new DelegateCommand(this.goBackCommand);
                this.SaveCommand = new DelegateCommand(this.saveCommand);
                this.Items = new ObservableCollection<EditorListItemViewModel>();
                this.eventAggregator.GetEvent<EditEvent>().Subscribe(this.loadItems);
            }

            private async void saveCommand()
            {
                switch (this.state)
                {
                    case ViewState.WorkoutPlan:
                        foreach (var item in Items)
                        {
                            await this.workoutDataStore.InsertWorkoutPlanAsync(item.Name, item.Id);
                        }
                        break;
                    case ViewState.Workout:
                        break;
                    case ViewState.Exercises:
                        break;
                    default:
                        break;
                }
                this.goBackCommand();
            }

            private void goBackCommand()
            {
                this.regionManager.RequestNavigate("EditorContentRegion", nameof(WorkoutEditorSummaryView));
            }

            private async void loadItems(ViewState state)
            {
                this.state = state;
                this.Items.Clear();
                switch (state)
                {
                    case ViewState.WorkoutPlan:
                        var wo = await this.workoutDataStore.GetWorkoutPlansAsync();
                        foreach (var w in wo)
                        {
                            Items.Add(new EditorListItemViewModel() { Id = w.Id, Name = w.Name });
                        }

                        break;
                    case ViewState.Workout:
                        break;
                    case ViewState.Exercises:
                        break;
                    default:
                        break;
                }

            }

            public void OnNavigatedFrom(INavigationParameters parameters)
            {
                if(!parameters.TryGetValue("workoutPlanId", out long workoutPlanId))
                {

                }
            }

            public void OnNavigatedTo(INavigationParameters parameters)
            {
                throw new NotImplementedException();
            }
        }
    }
}
