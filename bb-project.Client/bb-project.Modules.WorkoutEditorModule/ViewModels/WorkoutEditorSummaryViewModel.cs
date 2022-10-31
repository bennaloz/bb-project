
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
using Prism.Regions.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutEditorListItemViewModel
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        public string Details { get; set; }

    }
    public class WorkoutEditorSummaryViewModel : BindableBase, IRegionAware
    {
        private readonly IWorkoutsManagementService workoutDataStore;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService service;
        private Infrastructure.Models.Enums.ViewState currentState = Infrastructure.Models.Enums.ViewState.WorkoutPlan;

        private WorkoutEditorListItemViewModel parent;

        public string Title { get; set; }

        public ObservableCollection<WorkoutEditorListItemViewModel> Items { get; set; } = new ObservableCollection<WorkoutEditorListItemViewModel>();

        public ICommand NextStateCommand { get; set; }
        public ICommand PreviousStateCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ICommand SelectionChanged { get; set; }

        private WorkoutEditorListItemViewModel selectedItem;

        public WorkoutEditorListItemViewModel SelectedItem 
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                SetProperty(ref this.selectedItem, value);  
            }
        }  

        public WorkoutEditorSummaryViewModel(IWorkoutsManagementService workoutDataStore, IRegionManager regionManager, IEventAggregator eventAggregator, INavigationService service)
        {
            this.workoutDataStore = workoutDataStore;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.service = service;
            this.NextStateCommand = new DelegateCommand(this.nextViewModel);
            this.PreviousStateCommand = new DelegateCommand(this.previousViewModel);
            this.EditCommand = new DelegateCommand(this.goToEditView);
            this.SelectedItem = new WorkoutEditorListItemViewModel();
            this.ShowWorkoutPlans();
        }

     

        private void goToEditView()
        {
            switch (this.currentState)
            {
                case Infrastructure.Models.Enums.ViewState.WorkoutPlan:
                    NavigationParameters pars = new NavigationParameters();
                    pars.Add("workoutPlanId", this.SelectedItem.Id);
                    pars.Add("workoutPlanName", this.SelectedItem.Name);
                    this.regionManager.RequestNavigate("EditorContentRegion", nameof(Views.WorkoutPlanEditorView), pars);
                    break;
                case Infrastructure.Models.Enums.ViewState.Workout:
                    break;
                case Infrastructure.Models.Enums.ViewState.Exercises:
                    break;
                default:
                    break;
            }
        }

        private void GoodNav(IRegionNavigationResult obj)
        {
        }

        private async void nextViewModel()
        {
            this.Items.Clear();
            switch (this.currentState)
            {
                case Infrastructure.Models.Enums.ViewState.WorkoutPlan:
                    this.Title = $"Allenamenti '{this.SelectedItem.Name}'";
                    await ShowWorkouts(this.SelectedItem);
                    this.parent = this.SelectedItem;

                    break;
                case Infrastructure.Models.Enums.ViewState.Workout:
                    this.Title = $"Esercizi '{this.SelectedItem.Name}'";
                    await ShowExercises(this.SelectedItem);
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
            this.currentState = Infrastructure.Models.Enums.ViewState.WorkoutPlan;

        }

        private async Task ShowExercises(WorkoutEditorListItemViewModel item)
        {
            var seriesGroup = await this.workoutDataStore.GetWorkoutExercisesGroupsAsync(item.Id, "Pigna");
            foreach (var sg in seriesGroup)
            {
                List<string> names = new List<string>();
                int numberOfSeries = 0;
                int differentExercises = 0;
                string name = string.Empty;
                //foreach (var serie in sg.Series)
                //{
                //    if (!name.Contains(serie.ExerciseDefinition.Name))
                //    {
                //        name += serie.ExerciseDefinition.Name + "\n";
                //        differentExercises++;
                //    }
                //}
                this.Items.Add(new WorkoutEditorListItemViewModel { Id = sg.Id, Name = name });

            }
            this.currentState = Infrastructure.Models.Enums.ViewState.Exercises;
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