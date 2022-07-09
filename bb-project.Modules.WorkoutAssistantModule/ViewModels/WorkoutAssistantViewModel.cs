using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Modules.WorkoutAssistantModule.ViewModels
{
    public class WorkoutAssistantViewModel : BindableBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public WorkoutAssistantViewModel()
        {
            Title = "View A";
        }
    }
}
