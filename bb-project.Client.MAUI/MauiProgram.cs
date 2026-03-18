using Microsoft.Extensions.Logging;

namespace bb_project.Client.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(_ => { });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
