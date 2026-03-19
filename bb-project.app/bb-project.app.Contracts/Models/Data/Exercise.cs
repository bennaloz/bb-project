using bb_project.app.Contracts.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.app.Contracts.Models.Data
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
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
