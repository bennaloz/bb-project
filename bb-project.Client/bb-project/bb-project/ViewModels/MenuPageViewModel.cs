using bb_project.Client.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace bb_project.Client.ViewModels
{
    internal class MenuPageViewModel : BindableBase
    {
        enum Page
        {
            Home,
            Edit
        }

        private readonly INavigationService navigationService;
        private static Page currentPage = Page.Home;

        public ObservableCollection<string> MenuItems { get; set; }
        public string SelectedItem { get; set; }
        public ICommand NavigateCommand { get; set; }
        public MenuPageViewModel(INavigationService navigationService)
        {
            MenuItems = new ObservableCollection<string>() { "Home", "Edit workouts" };
            NavigateCommand = new DelegateCommand(Navigate);
            this.navigationService = navigationService;
        }

        private async void Navigate()
        {
            switch (this.SelectedItem)
            {
                case "Home":
                    {
                        if (currentPage != Page.Home)
                        {
                            await this.navigationService.NavigateAsync(nameof(MenuPage) + "/" + nameof(NavigationPage) + "/" + nameof(ContentMainPage));
                            currentPage = Page.Home;
                        }
                        break;
                    }
                case "Edit workouts":
                    {
                        if (currentPage != Page.Edit)
                        {
                            await this.navigationService.NavigateAsync(nameof(MenuPage) + "/" + nameof(NavigationPage) + "/" + nameof(EditContentMainPage));
                            currentPage = Page.Edit;
                        }
                        break;
                    }
            }
        }
    }
}