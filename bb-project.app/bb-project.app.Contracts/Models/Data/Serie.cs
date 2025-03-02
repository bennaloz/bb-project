namespace bb_project.app.Contracts.Models.Data
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
            Id = id;
        }
    }
}