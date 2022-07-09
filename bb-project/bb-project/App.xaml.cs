using bb_project.DAL;
using bb_project.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bb_project
{
    public partial class App : PrismApplication
    {

        public App()
        {
            InitializeComponent();
        }

        protected override void OnInitialized()
        {
            var result = NavigationService.NavigateAsync("AppShell").Result;
            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AppShell>("AppShell");
            containerRegistry.RegisterSingleton<IWorkoutsDataStore, WorkoutsDataStore>();
            containerRegistry.RegisterRegionServices();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<bb_project.Modules.HomeModule.HomeModule>();
            moduleCatalog.AddModule<bb_project.Modules.WorkoutAssistantModule.WorkoutAssistantModuleModule>();
            moduleCatalog.AddModule<bb_project.Modules.WorkoutEditorModule.WorkoutEditorModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
