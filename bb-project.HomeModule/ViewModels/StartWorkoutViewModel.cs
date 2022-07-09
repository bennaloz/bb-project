using bb_project.DAL;
using bb_project.DAL.Models;
using bb_project.Infrastructure.Models.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace bb_project.Modules.HomeModule.ViewModels
{
    internal class StartWorkoutViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        public ICommand StartWorkoutCommand { get; set; }

        public StartWorkoutViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.StartWorkoutCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<StartWorkoutEvent>().Publish();
            });
        }
    }
}
