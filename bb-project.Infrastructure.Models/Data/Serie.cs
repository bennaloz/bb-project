
using bb_project.Infrastructure.Models.Enums;
using System;

namespace bb_project.Infrastructure.Models.Data
{
    public class Serie
    {
        public ulong Id { get; }

        public int Reps { get; set; }

        public TimeSpan Rest { get; set; }

        public Serie()
            : this(0)
        {

        }

        public Serie(ulong id)
        {
            this.Id = id;
        }
    }
}