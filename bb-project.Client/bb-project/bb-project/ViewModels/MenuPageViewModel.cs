using bb_project.Client.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace bb_project.Client.ViewModels
{
    internal class MenuPageViewModel : BindableBase
    {
        private readonly INavigationService navigationService;

        public ObservableCollection<string> MenuItems { get; set; }
        public ICommand NavigateCommand { get; set; }
        public MenuPageViewModel(INavigationService navigationService)
        {
            MenuItems = new ObservableCollection<string>() { "Home", "Workout", "Edit workouts" };
            NavigateCommand = new DelegateCommand<string>(Navigate);
            this.navigationService = navigationService;
        }

        private void Navigate(string menuItem)
        {
            switch (menuItem)
            {
                case "Home":
                    {
                        break;
                    }
                case "Workout":
                    {
                        break;
                    }
                case "Edit workouts":
                    {
                        this.navigationService.NavigateAsync(nameof(EditContentMainPage));
                        break;
                    }
            }
        }
    }
}
