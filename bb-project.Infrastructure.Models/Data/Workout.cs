using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class Workout
    {
        public long ID { get; }

        public string Name { get; set; }

        public IEnumerable<Serie> Serie { get; set; }
    }
}
