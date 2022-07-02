using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.DAL.Models
{
    public class Workout
    {
        public long ID { get; }

        public string Name { get; set; }

        public IEnumerable<Serie> Serie { get; set; }
    }
}
