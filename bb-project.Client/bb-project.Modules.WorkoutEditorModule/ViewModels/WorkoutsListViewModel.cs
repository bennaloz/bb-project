using bb_project.Infrastructure.Models.Data;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutsListViewModel : BindableBase
    { 
        public WorkoutsListViewModel(IEnumerable<Workout> workouts)
        {
            Workouts = workouts;
        }

        public IEnumerable<Workout> Workouts { get; }
    }
}
