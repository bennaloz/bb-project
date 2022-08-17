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
        private readonly Serie serie;

        public string Reps
        {
            get { return serie.Reps.ToString(); }
        }

        public string Rest
        {
            get
            {
                TimeSpan t = serie.Rest;

                return string.Format("{0:D2}m:{1:D2}s",
                                t.Minutes,
                                t.Seconds);
            }
        }

        public string Name
        {
            get { return serie.ExerciseDefinition.Name; }
        }


        public WeightsExerciseViewModel(IWorkoutsManagementService dataStore)
        {
            this.dataStore = dataStore;

            //serie = this.dataStore.GetWorkoutSeriesAsync(1,"a").GetAwaiter().GetResult().ToList().First();

        }




    }
}
