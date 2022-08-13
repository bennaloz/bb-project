using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class SeriesGroup
    {
        public long Id { get; set; }

        public ExerciseMethodology ExerciseMethod { get; }

        public List<Serie> Series { get; } = new List<Serie>();

        public SeriesGroup(long id, ExerciseMethodology exerciseMethod)
        {
            this.Id = id;
            ExerciseMethod = exerciseMethod;
        }
    }
}
