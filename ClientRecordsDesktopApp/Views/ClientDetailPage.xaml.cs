using ClientRecordsDesktopApp.Services;
using ClientRecordsDesktopApp.ViewModels;

namespace ClientRecordsDesktopApp.Views;

public partial class ClientDetailPage : ContentPage {
    public ClientDetailPage(ClientDetailViewModel viewModel) {
        InitializeComponent();
        BindingContext = viewModel;
    }
}