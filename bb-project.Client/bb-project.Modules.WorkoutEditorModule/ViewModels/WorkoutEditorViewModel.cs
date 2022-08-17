
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
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutEditorListItemViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class WorkoutEditorViewModel :  BindableBase
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
                    Id = wop.ID,
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
                    var wo = await this.workoutDataStore.GetWorkoutsAsync(item.Id);
                    foreach (var w in wo)
                    {
                        this.Items.Add(new WorkoutEditorListItemViewModel
                        {
                            Id = w.Id,
                            Name = w.Name
                        });
                    }
                    this.parent = item;
                    this.currentState = Models.Enums.ViewState.Workout;
                    break;
                case Models.Enums.ViewState.Workout:
                    break;
                case Models.Enums.ViewState.Exercises:
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
                    break;
                case Models.Enums.ViewState.Workout:
                    var woPlans = this.workoutDataStore.GetWorkoutPlansAsync().GetAwaiter().GetResult();
                    foreach (var wop in woPlans)
                    {
                        this.Items.Add(new WorkoutEditorListItemViewModel
                        {
                            Id = wop.ID,
                            Name = wop.Name
                        });
                    }
                    break;
                case Models.Enums.ViewState.Exercises:
                    break;
                default:
                    break;
            }
        }
    }
}
