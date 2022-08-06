using bb_project.Infrastructure.Models.Data;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutPlanViewModel: BindableBase
    {
        public WorkoutPlanViewModel(WorkoutPlan workoutPlan)
        {
            WorkoutPlan = workoutPlan;
        }

        public WorkoutPlan WorkoutPlan { get; }
    }
}
