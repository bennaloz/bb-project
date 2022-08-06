using bb_project.Client.Services;
using bb_project.Infrastructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutsViewModel
    {
        public  List<Workout> Workouts { get; }
        private readonly IWorkoutsDataStore workoutsDataStore;

        public WorkoutsViewModel(long id ,IWorkoutsDataStore workoutsDataStore)
        {
            this.Workouts = workoutsDataStore.GetWorkoutsAsync(id).GetAwaiter().GetResult().ToList();
            this.workoutsDataStore = workoutsDataStore;
        }


    }
}
