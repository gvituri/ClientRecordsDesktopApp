using ClientRecordsDesktopApp.Models;
using ClientRecordsDesktopApp.Services;
using ClientRecordsDesktopApp.Services.Interfaces;
using ClientRecordsDesktopApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.ViewModels {
    public partial class ClientsDashboardViewModel : ObservableObject {
        private readonly IClientDatabaseService _clientDb;
        private readonly IServiceProvider _serviceProvider;

        public ClientsDashboardViewModel(IClientDatabaseService clientDb, IServiceProvider serviceProvider) {
            _clientDb = clientDb;
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeAsync() {
            await _clientDb.InitializeAsync();
            await LoadClientsAsync();
        }

        [ObservableProperty]
        private ObservableCollection<Client> clients = new();

        [RelayCommand]
        public async Task LoadClientsAsync() {
            var allClients = await _clientDb.GetClientsAsync();

            Clients.Clear();
            foreach (var c in allClients)
                Clients.Add(c);
        }

        [RelayCommand]
        public async Task CreateNewClientAsync() {
            await OpenClientDetailPageAsync(0);
        }

        [RelayCommand]
        public async Task OpenClientDetailAsync(Client selectedClient) {
            if (selectedClient == null)
                return;

            await OpenClientDetailPageAsync(selectedClient.Id);
        }

        private async Task OpenClientDetailPageAsync(int clientID) {
            var page = _serviceProvider.GetRequiredService<ClientDetailPage>();

            Window secondWindow = new Window(page) {
                Title = "Client Detail",
            };

            if (page.BindingContext is ClientDetailViewModel vm) {
                await vm.InitializeAsync(clientID, secondWindow.Id);
            }

            WindowSizingHelper.SetWindowSizeAndPosition(secondWindow,
                WindowSizingHelper.WindowSize.ThreeQuartersScreen,
                WindowSizingHelper.WindowPosition.CenterScreen);

            App.Current?.OpenWindow(secondWindow);
        }
    }
}
