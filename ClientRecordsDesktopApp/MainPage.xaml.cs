using ClientRecordsDesktopApp.Services;
using ClientRecordsDesktopApp.ViewModels;
using ClientRecordsDesktopApp.Views;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp {
    public partial class MainPage : ContentPage {
        private readonly IServiceProvider _serviceProvider;

        public MainPage(IServiceProvider serviceProvider) {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            WindowSizingHelper.MaximizeWindow(App.Current!.Windows[0]);
        }

        private void CounterBtn_Clicked(object sender, EventArgs e) {
            var page = _serviceProvider.GetRequiredService<ClientDetailPage>();


            Window secondWindow = new Window(page) {
                Title = "Client Detail",
            };

            if (page.BindingContext is ClientDetailViewModel vm) {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    MainThread.BeginInvokeOnMainThread( async() =>
                    {
                        await vm.InitializeAsync(0, secondWindow.Id);
                    });
                });
            }

            WindowSizingHelper.SetWindowSizeAndPosition(secondWindow,
                WindowSizingHelper.WindowSize.ThreeQuartersScreen,
                WindowSizingHelper.WindowPosition.CenterScreen);

            App.Current?.OpenWindow(secondWindow);
        }

        private void OpenExistingClient_Clicked(object sender, EventArgs e) {
            var page = _serviceProvider.GetRequiredService<ClientDetailPage>();

            Window secondWindow = new Window(page) {
                Title = "Client Detail",
            };

            if (page.BindingContext is ClientDetailViewModel vm) {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await vm.InitializeAsync(1, secondWindow.Id);
                    });
                });
            }

            WindowSizingHelper.SetWindowSizeAndPosition(secondWindow,
                WindowSizingHelper.WindowSize.ThreeQuartersScreen,
                WindowSizingHelper.WindowPosition.CenterScreen);

            App.Current?.OpenWindow(secondWindow);
        }
    }

}
