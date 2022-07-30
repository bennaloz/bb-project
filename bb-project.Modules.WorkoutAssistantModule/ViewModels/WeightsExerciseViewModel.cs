using bb_project.DAL;
using bb_project.Infrastructure.BLL;
using bb_project.Infrastructure.Models.Data;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bb_project.Modules.WorkoutAssistantModule.ViewModels
{
    internal class WeightsExerciseViewModel : BindableBase
    {
        private readonly IWorkoutsDataStore dataStore;
        private readonly Serie serie;

        public string Reps
        {
            get { return serie.Reps.ToString(); }
        }

        public string Rest
        {
            get
            {
                TimeSpan t = TimeSpan.FromSeconds(serie.Rest);

                return string.Format("{0:D2}m:{1:D2}s",
                                t.Minutes,
                                t.Seconds);
            }
        }

        public string Name
        {
            get { return serie.OwnerExercise.Name; }
        }


        public WeightsExerciseViewModel(IWorkoutsDataStore dataStore)
        {
            this.dataStore = dataStore;

            serie = this.dataStore.GetWorkoutSeriesAsync(1,"a").GetAwaiter().GetResult().ToList().First();

        }




    }
}
