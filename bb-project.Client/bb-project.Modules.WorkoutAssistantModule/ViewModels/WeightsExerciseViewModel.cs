using bb_project.Client.Services;


using bb_project.Infrastructure.Models.Data;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bb_project.Client.Modules.WorkoutAssistantModule.ViewModels
{
    internal class WeightsExerciseViewModel : BindableBase
    {
        private readonly IWorkoutsManagementService dataStore;

        public string Reps
        {
            get { return ""; }
        }

        public string Rest
        {
            get
            {
               return "";
            }
        }

        public string Name
        {
            get { return ""; }
        }


        public WeightsExerciseViewModel(IWorkoutsManagementService dataStore)
        {
            this.dataStore = dataStore;

        }




    }
}
