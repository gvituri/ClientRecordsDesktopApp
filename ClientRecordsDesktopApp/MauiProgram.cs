using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using UraniumUI;
using ClientRecordsDesktopApp.ViewModels;
using ClientRecordsDesktopApp.Views;
using ClientRecordsDesktopApp.Services.Interfaces;
using ClientRecordsDesktopApp.Services;

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
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialSymbolsFonts();
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "clients.db3");

            builder.Services.AddSingleton<IClientDatabaseService>(s =>
                new ClientDatabaseService(dbPath));

            builder.Services.AddSingleton<IDialogService, DialogService>();

            builder.Services.AddTransient<ClientDetailPage>();
            builder.Services.AddTransient<ClientDetailViewModel>();
            builder.Services.AddTransient<ClientsDashboardPage>();
            builder.Services.AddTransient<ClientsDashboardViewModel>();

            return builder.Build();
        }
    }
}
