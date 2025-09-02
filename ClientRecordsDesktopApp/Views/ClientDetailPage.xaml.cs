using ClientRecordsDesktopApp.Services;
using ClientRecordsDesktopApp.ViewModels;

namespace ClientRecordsDesktopApp.Views;

public partial class ClientDetailPage : ContentPage {
    ClientDetailViewModel _viewModel;
    public ClientDetailPage(ClientDetailViewModel viewModel) {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
}