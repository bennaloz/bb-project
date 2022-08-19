using bb_project.Client.Modules.WorkoutEditorModule.Views;
using bb_project.Infrastructure.Models.Enums;
using bb_project.Infrastructure.Models.Events;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    public class EditorMainContentViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public EditorMainContentViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
        }

  
    }
}
