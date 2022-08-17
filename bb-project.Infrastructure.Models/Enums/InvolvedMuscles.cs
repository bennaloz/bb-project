using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Enums
{
    public enum InvolvedMuscles
    {
        Pectorals   = 0x001,
        Back        = 0x002,
        Deltoids    = 0x004,
        Quadriceps  = 0x008,
        Hamstrings  = 0x010,
        Calf        = 0x020,
        Biceps      = 0x040,
        Triceps     = 0x080,
        Heart       = 0x100
    }
}
