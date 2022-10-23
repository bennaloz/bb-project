using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class Exercise
    {
        public ulong Id { get; }

        public string Name { get; }

        public List<Serie> Series { get; } = new List<Serie>();

        public Exercise(ExerciseDefinition definition)
        {
            this.Id = definition.Id;
            this.Name = definition.Name;
        }
    }
}
