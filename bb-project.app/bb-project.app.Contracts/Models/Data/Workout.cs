using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.app.Contracts.Models.Data
{
    public class Workout
    {
        public ulong Id { get; }

        public string Name { get; set; }

        public int Order { get; set; }

        public Workout(ulong id)
        {
            Id = id;
        }
    }
}
