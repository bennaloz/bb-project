
using bb_project.Client.Modules.WorkoutEditorModule.Views;
using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutEditorListItemViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Details { get; set; }

    }
    public class WorkoutEditorSummaryViewModel : BindableBase
    {
        private readonly IWorkoutsManagementService workoutDataStore;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private Infrastructure.Models.Enums.ViewState currentState = Infrastructure.Models.Enums.ViewState.WorkoutPlan;

        private WorkoutEditorListItemViewModel parent;

        public string Title { get; set; }

        public ObservableCollection<WorkoutEditorListItemViewModel> Items { get; set; } = new ObservableCollection<WorkoutEditorListItemViewModel>();

        public ICommand NextStateCommand { get; set; }
        public ICommand PreviousStateCommand { get; set; }
        public ICommand EditCommand { get; set; }


        public WorkoutEditorSummaryViewModel(IWorkoutsManagementService workoutDataStore, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.workoutDataStore = workoutDataStore;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.NextStateCommand = new DelegateCommand<WorkoutEditorListItemViewModel>(this.nextViewModel);
            this.PreviousStateCommand = new DelegateCommand(this.previousViewModel);
            this.EditCommand = new DelegateCommand<WorkoutEditorListItemViewModel>(this.goToEditView);

            this.ShowWorkoutPlans();
        }

        private void goToEditView(WorkoutEditorListItemViewModel item)
        {
            switch (this.currentState)
            {
                case Infrastructure.Models.Enums.ViewState.WorkoutPlan:
                    this.regionManager.RequestNavigate("EditorContentRegion", nameof(Views.WorkoutPlanEditorView), new NavigationParameters($"workoutPlanId={item.Id}"));
                    break;
                case Infrastructure.Models.Enums.ViewState.Workout:
                    break;
                case Infrastructure.Models.Enums.ViewState.Exercises:
                    break;
                default:
                    break;
            }
        }

        private async void nextViewModel(WorkoutEditorListItemViewModel item)
        {
            this.Items.Clear();
            switch (this.currentState)
            {
                case Infrastructure.Models.Enums.ViewState.WorkoutPlan:
                    this.Title = $"Allenamenti '{item.Name}'";
                    await ShowWorkouts(item);
                    this.parent = item;

                    break;
                case Infrastructure.Models.Enums.ViewState.Workout:
                    this.Title = $"Esercizi '{item.Name}'";
                    await ShowExercises(item);
                    break;
                default:
                    break;
            }
            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(this.Title)));
        }

        private async void previousViewModel()
        {
            this.Items.Clear();
            switch (this.currentState)
            {
                case Infrastructure.Models.Enums.ViewState.WorkoutPlan:
                case Infrastructure.Models.Enums.ViewState.Workout:
                    this.Title = $"Piani di allenamento";
                    await ShowWorkoutPlans();
                    break;
                case Infrastructure.Models.Enums.ViewState.Exercises:
                    this.Title = $"Allenamenti '{parent.Name}'";
                    await ShowWorkouts(parent);
                    break;
                default:
                    break;
            }
            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(this.Title)));
        }

        private async Task ShowWorkoutPlans()
        {
            this.Title = "Piani di allenamento";
            var woPlans = await this.workoutDataStore.GetWorkoutPlansAsync();
            foreach (var wop in woPlans)
            {
                var workouts = await this.workoutDataStore.GetWorkoutsAsync(wop.Id);
                this.Items.Add(new WorkoutEditorListItemViewModel
                {
                    Id = wop.Id,
                    Name = wop.Name,
                    Details = $"Workouts: {workouts.Count()}"
                });
            }
        }

        private async Task ShowExercises(WorkoutEditorListItemViewModel item)
        {
            var seriesGroup = await this.workoutDataStore.GetWorkoutSeriesGroupsAsync(item.Id, "Pigna");
            foreach (var sg in seriesGroup)
            {
                List<string> names = new List<string>();
                int numberOfSeries = 0;
                int differentExercises = 0;
                string name = string.Empty;
                foreach (var serie in sg.Series)
                {
                    if (!name.Contains(serie.ExerciseDefinition.Name))
                    {
                        name += serie.ExerciseDefinition.Name + "\n";
                        differentExercises++;
                    }
                }
                this.Items.Add(new WorkoutEditorListItemViewModel { Id = sg.Id, Name = name });

            }
        }

        private async Task ShowWorkouts(WorkoutEditorListItemViewModel item)
        {
            var wo = await this.workoutDataStore.GetWorkoutsAsync(item.Id);
            foreach (var w in wo)
            {
                this.Items.Add(new WorkoutEditorListItemViewModel
                {
                    Id = w.Id,
                    Name = w.Name
                });
            }

            this.currentState = Infrastructure.Models.Enums.ViewState.Workout;
        }



    }
}
