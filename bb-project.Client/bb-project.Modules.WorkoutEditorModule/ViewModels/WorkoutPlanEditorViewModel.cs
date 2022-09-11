using bb_project.Client.Modules.WorkoutEditorModule.Views;
using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;
using bb_project.Infrastructure.Models.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using Prism.Regions.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutItemViewModel : BindableBase
    {
        public long Id { get; set; }

        private string workoutName;
        public string WorkoutName
        {
            get
            {
                return workoutName;
            }
            set
            {
                SetProperty(ref workoutName, value);
            }

        }
        public struct Exercise
        {
            public string Name { get; set; }
            public ObservableCollection<int> Reps { get; set; }
        }

        public ObservableCollection<Exercise> Exercises { get; set; } = new ObservableCollection<Exercise>();
    }

    public class WorkoutPlanEditorViewModel : BindableBase, IRegionAware
    {
        private readonly IWorkoutsManagementService workoutDataStore;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        public ObservableCollection<WorkoutItemViewModel> Workouts { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public ICommand RemoveCommand { get; set; }

        private ViewState state;
        public WorkoutPlanEditorViewModel(IWorkoutsManagementService workoutDataStore, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.workoutDataStore = workoutDataStore;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.BackCommand = new DelegateCommand(this.goBackCommand);
            this.SaveCommand = new DelegateCommand(this.saveCommand);
            this.Workouts = new ObservableCollection<WorkoutItemViewModel>();
            this.RemoveCommand = new DelegateCommand<object>(this.removeWorkout);
            //this.eventAggregator.GetEvent<EditEvent>().Subscribe(this.loadItems);
        }

        private void removeWorkout(object id)
        {

            var workout = this.Workouts.First(o => o.Id == (long)id);
            if (workout != default)
            {
                this.Workouts.Remove(workout);
            }
        }

        private async void saveCommand()
        {
            //switch (this.state)
            //{
            //    case ViewState.WorkoutPlan:
            //        foreach (var item in Items)
            //        {
            //            await this.workoutDataStore.InsertWorkoutPlanAsync(item.Name, item.Id);
            //        }
            //        break;
            //    case ViewState.Workout:
            //        break;
            //    case ViewState.Exercises:
            //        break;
            //    default:
            //        break;
            //}
            //this.goBackCommand();
        }

        private void goBackCommand()
        {
            this.regionManager.RequestNavigate("EditorContentRegion", nameof(WorkoutEditorSummaryView));
        }

        //private async void loadItems(ViewState state)
        //{
        //    this.state = state;
        //    this.Items.Clear();
        //    switch (state)
        //    {
        //        case ViewState.WorkoutPlan:
        //            var wo = await this.workoutDataStore.GetWorkoutPlansAsync();
        //            foreach (var w in wo)
        //            {
        //                Items.Add(new EditorListItemViewModel() { Id = w.Id, Name = w.Name });
        //            }

        //            break;
        //        case ViewState.Workout:
        //            break;
        //        case ViewState.Exercises:
        //            break;
        //        default:
        //            break;
        //    }

        //}

        public string WorkoutPlanName { get; set; }


        public async void OnNavigatedTo(INavigationContext navigationContext)
        {
            navigationContext.Parameters.TryGetValue("workoutPlanId", out long planid);
            navigationContext.Parameters.TryGetValue("workoutPlanName", out string planName);

            this.WorkoutPlanName = planName;
            this.RaisePropertyChanged(nameof(this.WorkoutPlanName));

            var asyncWorkouts = await this.workoutDataStore.GetWorkoutsAsync(planid);

            
            foreach (var item in asyncWorkouts)
            {
                var series = await this.workoutDataStore.GetWorkoutSeriesGroupsAsync(item.Id, "Pigna");
                foreach (var seriesGroup in series)
                {
                    WorkoutItemViewModel.Exercise exercise = new WorkoutItemViewModel.Exercise();
                    exercise.Reps = new ObservableCollection<int>();
                    foreach (var serie in seriesGroup.Series)
                    {
                        exercise.Name = serie.ExerciseDefinition.Name;
                        exercise.Reps.Add(serie.Reps);
                    }
                }

                this.Workouts.Add(new WorkoutItemViewModel() { Id = item.Id, WorkoutName = item.Name });
            }
           
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
