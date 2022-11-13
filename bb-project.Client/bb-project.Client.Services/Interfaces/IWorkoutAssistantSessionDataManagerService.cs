using bb_project.Client.Data;

namespace bb_project.Client.Services.Interfaces
{
    public interface IWorkoutAssistantSessionDataManagerService
    {
        //https://learn.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases
        public BBProjectDatabase Database { get; }
    }
}