using ClientRecordsDesktopApp.Services;
using ClientRecordsDesktopApp.ViewModels;

namespace ClientRecordsDesktopApp.Views;

public partial class ClientsDashboardPage : ContentPage {
    public ClientsDashboardPage(ClientsDashboardViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
        WindowSizingHelper.MaximizeWindow(App.Current!.Windows[0]);
        System.Threading.Tasks.Task.Factory.StartNew(() =>{
            MainThread.BeginInvokeOnMainThread(async () =>{
                await viewModel.InitializeAsync();
            });
        });
    }
}