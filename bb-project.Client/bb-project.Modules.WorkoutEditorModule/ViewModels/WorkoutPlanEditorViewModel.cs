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
using System.Threading.Tasks;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutItemViewModel : BindableBase
    {

        public WorkoutItemViewModel()
        {
            this.ExercisesInfo= new Dictionary<string, string>();
            
        }

        public ulong Id { get; set; }

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

        //private string exerciseInfos;

        //public string ExerciseInfos
        //{
        //    get
        //    {
        //        return this.exerciseInfos;
        //    }
        //    set
        //    {
        //        SetProperty(ref this.exerciseInfos, value);
        //    }
        //}


        private Dictionary<string, string> exercisesInfo;
        public Dictionary<string, string> ExercisesInfo
        {
            get
            {
                return exercisesInfo;
            }
            set
            {
                SetProperty(ref exercisesInfo, value);
            }
        }

    }

    public class WorkoutPlanEditorViewModel : BindableBase, IRegionAware
    {
        private readonly IWorkoutsManagementService workoutDataStore;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;

        public ObservableCollection<WorkoutItemViewModel> Workouts { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public ICommand RemoveCommand { get; set; }

        public ICommand OpenPopupCommand { get; }

        private ViewState state;
        public WorkoutPlanEditorViewModel(IWorkoutsManagementService workoutDataStore, IRegionManager regionManager, IEventAggregator eventAggregator, INavigationService navigationService)
        {
            this.workoutDataStore = workoutDataStore;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;
            this.BackCommand = new DelegateCommand(this.goBackCommand);
            this.SaveCommand = new DelegateCommand(this.saveCommand);
            this.Workouts = new ObservableCollection<WorkoutItemViewModel>();
            this.RemoveCommand = new DelegateCommand<object>(this.removeWorkout);
            this.OpenPopupCommand = new DelegateCommand<object>(this.onOpenPopupSummaryCommand);
            //this.eventAggregator.GetEvent<EditEvent>().Subscribe(this.loadItems);
        }

        private void onOpenPopupSummaryCommand(object items)
        {
            if(!(items is Dictionary<string, string> exercises))
            {
                return;
            }

            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("items", exercises);
            this.navigationService.NavigateAsync(nameof(PopupSummaryExerciseView), parameters);
        }

        private void removeWorkout(object id)
        {

            var workout = this.Workouts.First(o => o.Id == (ulong)id);
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


        public string WorkoutPlanName { get; set; }


        public async void OnNavigatedTo(INavigationContext navigationContext)
        {
            this.Workouts.Clear();
            navigationContext.Parameters.TryGetValue("workoutPlanId", out ulong planid);
            navigationContext.Parameters.TryGetValue("workoutPlanName", out string planName);
            await Load(planid, planName);
        }

        private async Task Load(ulong planid, string planName)
        {

            this.WorkoutPlanName = planName;
            this.RaisePropertyChanged(nameof(this.WorkoutPlanName));

            var asyncWorkouts = await this.workoutDataStore.GetWorkoutsAsync(planid);


            foreach (var workout in asyncWorkouts)
            {
                var seriesGroups = await this.workoutDataStore.GetWorkoutExercisesGroupsAsync(workout.Id, "Pigna");


                var tmp = new WorkoutItemViewModel
                {
                    Id = workout.Id,
                    WorkoutName = workout.Name
                };
                foreach (var serieGroup in seriesGroups)
                {
                    foreach (var exInfo in GetExerciseFormat(serieGroup))
                    {
                        tmp.ExercisesInfo.Add(exInfo.Key,exInfo.Value);
                    }

                }
                this.Workouts.Add(tmp);
            }
        }




        private Dictionary<string, string> GetExerciseFormat(SeriesGroup serieGroup)
        {
            Dictionary<string, string> exercisesInfo = new Dictionary<string, string>();
            string result= string.Empty;
            if(serieGroup.ExerciseMethod!= ExerciseMethodology.Single)
            {
                result += $"{serieGroup.ExerciseMethod}\n";

            }
            foreach (var serie in serieGroup.Series)
            {


                if(!exercisesInfo.ContainsKey(serie.ExerciseDefinition.Name))
                {
                    exercisesInfo.Add(serie.ExerciseDefinition.Name, string.Empty);
                }
                string detail = string.Empty;

                switch (serie.ExerciseDefinition.Type)
                {
                    case ExerciseType.Cardio:
                        detail = $"{serie.Rest.Minutes:00}:{serie.Rest.Seconds:00}  ";
                        break;
                    case ExerciseType.Weights:
                        detail = $"{serie.Reps}  ";
                        break;
                    default:
                        break;
                }


                exercisesInfo[serie.ExerciseDefinition.Name] += $"{detail}";
            }


            foreach (var info in exercisesInfo)
            {
                result += $"{info.Key.ToUpper()}\t\t" +
                    $"{info.Value}\n";
            }
            result += "\n";

            return exercisesInfo;
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
