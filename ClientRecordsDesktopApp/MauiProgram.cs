using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using UraniumUI;
using UraniumUI.Material;
using InputKit;
using ClientRecordsDesktopApp.ViewModels;
using ClientRecordsDesktopApp.Views;
using CommunityToolkit.Maui;

namespace ClientRecordsDesktopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddInputKitHandlers();
                })
                .UseUraniumUI()
                .UseMauiCommunityToolkit()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<ClientDetailPage>();
            builder.Services.AddTransient<ClientDetailViewModel>();

            return builder.Build();
        }
    }
}
