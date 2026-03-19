using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.app.Contracts.Models.Data
{
    public class WorkoutPlan  : IEqualityComparer<WorkoutPlan>, IEquatable<WorkoutPlan>
    {
        public ulong Id { get; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public WorkoutPlan(ulong id)
        {
            Id = id;   
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as WorkoutPlan);
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public bool Equals(WorkoutPlan? other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (this.Id != other.Id)
                return false;

            return Name == other.Name;
        }

        public bool Equals(WorkoutPlan? x, WorkoutPlan? y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false;
            if (ReferenceEquals(x, this))
                return false;
            if (ReferenceEquals(y, this))
                return true;
            return x.Id == y.Id;
        }

        public int GetHashCode(WorkoutPlan obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
