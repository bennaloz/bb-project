using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class Workout
    {
        public ulong Id { get; }

        public string Name { get; set; }

        public int Order { get; set; }

        public Workout(ulong id)
        {
            this.Id = id;
        }
    }
}
