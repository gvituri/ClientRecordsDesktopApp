using ClientRecordsDesktopApp.Views;

namespace ClientRecordsDesktopApp {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClientDetailPage), typeof(ClientDetailPage));
        }
    }
}
