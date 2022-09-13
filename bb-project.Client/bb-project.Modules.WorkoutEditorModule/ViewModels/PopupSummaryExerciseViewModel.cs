using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.Modules.WorkoutEditorModule.ViewModels
{
    internal class PopupSummaryExerciseViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;

        private Dictionary<string, string> exerciseInfo;
        public Dictionary<string, string> ExerciseInfo
        {
            get
            {
                return exerciseInfo;
            }
            set
            {
                SetProperty(ref exerciseInfo, value);
            }
        }
        public ICommand NavigateBackCommand { get; }

        public PopupSummaryExerciseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.NavigateBackCommand = new DelegateCommand(OnNavigateBack);
        }

        private async void OnNavigateBack()
        {
            await navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

            parameters.TryGetValue("items", out Dictionary<string, string> items);
            if (items == default)
            {
                return;
            }

            this.ExerciseInfo = items;

        }
    }
}
