using bb_project.Client.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Services
{
    internal class WorkoutAsssitantSessionDataManagerService : Interfaces.IWorkoutAssistantSessionDataManagerService
    {
        private BBProjectDatabase bbProjectDb = new BBProjectDatabase();

        public BBProjectDatabase Database => this.bbProjectDb;
    }
}
