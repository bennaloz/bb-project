

using bb_project.Authentication;
using bb_project.Client.Services;
using bb_project.Client.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bb_project.Client
{
    public partial class App : PrismApplication
    {

        public App()
        {
            InitializeComponent();
        }

        protected override void OnInitialized()
        {
            var result = NavigationService.NavigateAsync(nameof(MenuPage) + "/" + nameof(NavigationPage) + "/" + nameof(ContentMainPage));
            if (!result.GetAwaiter().GetResult().Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AppShell>("AppShell");

            containerRegistry.RegisterForNavigation<ContentMainPage>(nameof(ContentMainPage));
            containerRegistry.RegisterForNavigation<EditContentMainPage>(nameof(EditContentMainPage));
            containerRegistry.RegisterForNavigation<MenuPage>(nameof(MenuPage));
            containerRegistry.RegisterForNavigation<NavigationPage>();


            containerRegistry.RegisterInstance<IWorkoutsManagementService>(new WorkoutsManagementService("https://192.168.0.11:7030"));
            containerRegistry.RegisterSingleton<IUserAuthenticatorService, UserAuthenticatorService>();
            containerRegistry.RegisterRegionServices();
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<bb_project.Client.Modules.HomeModule.HomeModule>();
            moduleCatalog.AddModule<bb_project.Client.Modules.WorkoutAssistantModule.WorkoutAssistantModule>();
            moduleCatalog.AddModule<bb_project.Client.Modules.WorkoutEditorModule.WorkoutEditorModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
