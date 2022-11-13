using bb_project.Infrastructure.Models.Data;
using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule
{
    public class SerieStructure
    {
        public int Reps { get; set; }
        public string TimeStamp { get; set; }
        public SerieStructure(Serie serie)
        {
            this.Reps = serie.Reps;
            this.TimeStamp = $"{(int)serie.Rest.TotalMinutes}.{serie.Rest.Seconds:00}";
        }
    }
    public class ExerciseStructure
    {
        public ExerciseStructure(Exercise exercise)
        {
            this.Name = exercise.Name;
            this.SerieStructures = new ObservableCollection<SerieStructure>();
            foreach (var item in exercise.Series)
            {
                this.SerieStructures.Add(new SerieStructure(item));
            }
        }

        public string Name { get; set; }
        public ObservableCollection<SerieStructure> SerieStructures { get; set; }

    }

    public class ExerciseGroupStructure
    {
        public ExerciseGroupStructure(ExerciseGroup exerciseGroup)
        {

            this.Exercises = new ObservableCollection<ExerciseStructure>();
            if (exerciseGroup != null)
            {
                this.ExMethod = exerciseGroup.ExerciseMethod;

                foreach (var ex in exerciseGroup.Exercises)
                {
                    this.Exercises.Add(new ExerciseStructure(ex.Value));
                }
            }
        }
        public ExerciseMethodology ExMethod { get; set; }
        public ObservableCollection<ExerciseStructure> Exercises { get; set; }

    }
    public class WorkoutStructure
    {
        public WorkoutStructure()
        {
            this.ExGroups = new ObservableCollection<ExerciseGroupStructure>();
        }
        public Workout Workout { get; set; }

        public ObservableCollection<ExerciseGroupStructure> ExGroups { get; set; }
    }
    public class PlanStructure
    {
        public PlanStructure()
        {
            this.Workouts = new ObservableCollection<WorkoutStructure>();

        }
        public WorkoutPlan WorkoutPlan { get; set; }
        public ObservableCollection<WorkoutStructure> Workouts { get; set; }
    }

}
