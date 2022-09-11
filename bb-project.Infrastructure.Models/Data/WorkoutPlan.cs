using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Infrastructure.Models.Data
{
    public class WorkoutPlan  : IEqualityComparer<WorkoutPlan>, IEquatable<WorkoutPlan>
    {
        public ulong Id { get; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public WorkoutPlan(ulong id)
        {
            this.Id = id;   
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as WorkoutPlan);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(this);
        }

        public bool Equals(WorkoutPlan other)
        {
            return this.Name == other.Name;
        }

        public bool Equals(WorkoutPlan x, WorkoutPlan y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(WorkoutPlan obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
