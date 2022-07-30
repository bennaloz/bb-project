using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class Workout
    {
        public long Id { get; }

        public string Name { get; set; }

        public int Order { get; set; }

        public Workout(long id)
        {
            this.Id = id;
        }
    }
}
