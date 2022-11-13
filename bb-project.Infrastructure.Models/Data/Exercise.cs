using bb_project.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class Exercise
    {
        public ulong Id { get; }

        public string Name { get; }

        public ExerciseType Type {get;}

        public List<Serie> Series { get; } = new List<Serie>();

        public Exercise(ExerciseDefinition definition)
            : this(definition.Id, definition.Name, definition.Type)
        {
        }

        public Exercise(ulong id, string name, ExerciseType type)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
        }
    }
}
