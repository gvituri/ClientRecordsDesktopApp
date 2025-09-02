using ClientRecordsDesktopApp.Models;
using ClientRecordsDesktopApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.ViewModels {
    public partial  class ClientDetailViewModel : ObservableObject {
        private readonly IClientDatabaseService _clientDb;
        private readonly IDialogService _dialogService;

        public ClientDetailViewModel(IClientDatabaseService clientDb,
            IDialogService dialogService) {
            _clientDb = clientDb;
            _dialogService = dialogService;
        }

        [ObservableProperty]
        private Client client;

        [ObservableProperty]
        private bool isEditMode;

        public async Task InitializeAsync(int clientId) {
            await _clientDb.InitializeAsync();
            var clients = await _clientDb.GetClientsAsync();

            if (clientId > 0) {
                IsEditMode = true;

                Client = await _clientDb.GetClientAsync(clientId);

                if (Client == null) {
                    IsEditMode = false;
                    Client = new Client();
                }
            } else {
                IsEditMode = false;
                Client = new Client();
            }
        }

        [RelayCommand]
        public async Task SaveAsync() {
            if (Client.ID <= 0) {
                await _clientDb.CreateClientAsync(Client);
            } else {
                await _clientDb.UpdateClientAsync(Client);
            }
        }

        [RelayCommand]
        public async Task DeleteAsync() {
            if (Client == null || Client.ID <= 0)
                return;

            bool confirm = await _dialogService.ShowConfirmationAsync(
                "Confirm Deletion",
                $"Are you sure you want to delete client \"{Client.Name}\"?"
            );

            if (confirm) {
                await _clientDb.DeleteClientAsync(Client);
                Client = null;
                IsEditMode = false;

                await _dialogService.ShowMessageAsync("Deleted", "Client successfully deleted.");
            }
        }
    }
}
