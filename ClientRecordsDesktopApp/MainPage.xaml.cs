using ClientRecordsDesktopApp.Views;

namespace ClientRecordsDesktopApp {
    public partial class MainPage : ContentPage {
        private readonly IServiceProvider _serviceProvider;

        public MainPage(IServiceProvider serviceProvider) {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void OnCounterClicked(object sender, EventArgs e) {
            var page = _serviceProvider.GetRequiredService<ClientDetailPage>();
            Window secondWindow = new Window(page);
            App.Current?.OpenWindow(secondWindow);
        }
    }

}
