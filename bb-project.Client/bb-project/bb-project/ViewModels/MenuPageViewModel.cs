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


        public struct MenuItem
        {
            public MenuItem(string title, string iconName)
            {
                this.Title = title;
                this.IconName = iconName;
            }
            public string Title { get; }
            public string IconName { get; }
        }

        private readonly INavigationService navigationService;
        private static Page currentPage = Page.Home;

        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public ICommand NavigateCommand { get; }
        public MenuPageViewModel(INavigationService navigationService)
        {
            MenuItems = new ObservableCollection<MenuItem>()
            {new MenuItem("Home", "home.png") ,new MenuItem("Edit workouts", "edit.png") };
            NavigateCommand = new DelegateCommand<object>(Navigate);
            this.navigationService = navigationService;
        }

        private async void Navigate(object page)
        {
            switch (page)
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