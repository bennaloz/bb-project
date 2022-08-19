
using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
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

    }
    public class WorkoutEditorViewModel : BindableBase
    {
        private readonly IWorkoutsManagementService workoutDataStore;

        private Models.Enums.ViewState currentState = Models.Enums.ViewState.WorkoutPlan;

        private WorkoutEditorListItemViewModel parent;

        public ObservableCollection<WorkoutEditorListItemViewModel> Items { get; set; } = new ObservableCollection<WorkoutEditorListItemViewModel>();

        public ICommand NextStateCommand { get; set; }
        public ICommand PreviousStateCommand { get; set; }

        public WorkoutEditorViewModel(IWorkoutsManagementService workoutDataStore)
        {
            this.workoutDataStore = workoutDataStore;

            this.NextStateCommand = new DelegateCommand<WorkoutEditorListItemViewModel>(this.nextViewModel);
            this.PreviousStateCommand = new DelegateCommand(this.previousViewModel);
            var woPlans = this.workoutDataStore.GetWorkoutPlansAsync().GetAwaiter().GetResult();
            foreach (var wop in woPlans)
            {
                this.Items.Add(new WorkoutEditorListItemViewModel
                {
                    Id = wop.Id,
                    Name = wop.Name
                });
            }
        }

        private async void nextViewModel(WorkoutEditorListItemViewModel item)
        {
            this.Items.Clear();
            switch (this.currentState)
            {
                case Models.Enums.ViewState.WorkoutPlan:
                    await ShowWorkouts(item);
                    this.parent = item;

                    break;
                case Models.Enums.ViewState.Workout:
                case Models.Enums.ViewState.Exercises:
                    await ShowExercises(item);
                    break;
                default:
                    break;
            }
        }

        private async void previousViewModel()
        {
            this.Items.Clear();
            switch (this.currentState)
            {
                case Models.Enums.ViewState.WorkoutPlan:
                case Models.Enums.ViewState.Workout:
                    await ShowWorkoutPlans();
                    break;
                case Models.Enums.ViewState.Exercises:
                    await ShowWorkouts(parent);
                    break;
                default:
                    break;
            }
        }

        private async Task ShowWorkoutPlans()
        {
            var woPlans = await this.workoutDataStore.GetWorkoutPlansAsync();
            foreach (var wop in woPlans)
            {
                this.Items.Add(new WorkoutEditorListItemViewModel
                {
                    Id = wop.Id,
                    Name = wop.Name
                });
            }
            this.currentState = Models.Enums.ViewState.WorkoutPlan;
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
                    if(!name.Contains(serie.ExerciseDefinition.Name))
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

            this.currentState = Models.Enums.ViewState.Workout;
        }
    }
}
