using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class SeriesGroup
    {
        public ulong Id { get; set; }

        public ExerciseMethodology ExerciseMethod { get; }

        public List<Serie> Series { get; } = new List<Serie>();

        public SeriesGroup(ExerciseMethodology exerciseMethod)
            : this(0, exerciseMethod)
        {

        }

        public SeriesGroup(ulong id, ExerciseMethodology exerciseMethod)
        {
            this.Id = id;
            ExerciseMethod = exerciseMethod;
        }
    }
}
