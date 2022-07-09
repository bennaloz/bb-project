using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Modules.WorkoutEditorModule.ViewModels
{
    public class WorkoutEditorViewModel : BindableBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public WorkoutEditorViewModel()
        {
            Title = "View A";
        }
    }
}
