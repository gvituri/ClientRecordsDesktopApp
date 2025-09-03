using ClientRecordsDesktopApp.Models;
using ClientRecordsDesktopApp.Utils.Messages;
using ClientRecordsDesktopApp.Services;
using ClientRecordsDesktopApp.Services.Interfaces;
using ClientRecordsDesktopApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
        private readonly IDialogService _dialogService;

        public ClientsDashboardViewModel(IClientDatabaseService clientDb,
            IServiceProvider serviceProvider,
            IDialogService dialogService) {
            _clientDb = clientDb;
            _serviceProvider = serviceProvider;
            _dialogService = dialogService;
        }

        public async Task InitializeAsync() {
            await _clientDb.InitializeAsync();
            await LoadClientsAsync();
            await AddMockUpClients();
        }

        [ObservableProperty]
        private ObservableCollection<Client> clients = new();

        [ObservableProperty]
        Client selectedClient = null;

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
        public async Task OpenClientDetailAsync(Client selectedListItem) {
            if (selectedListItem == null)
                return;

            await OpenClientDetailPageAsync(selectedListItem.Id);
            SelectedClient = null;
        }

        public async Task AddMockUpClients() {
            if (Clients.Any()) return;

            if (!await _dialogService.ShowConfirmationAsync(
                App.Current!.Windows[0].Id,
                "Carregar Mockup",
                $"Deseja carregar mockup de dados de clientes?"
            )) return;

            var mockClients = new List<Client> {
                    new Client { Name = "João", LastName = "Silva", Age = 30, Adress = "Rua Roma, 123" },
                    new Client { Name = "Maria", LastName = "Oliveira", Age = 25, Adress = "Avenida das Flores, 456" },
                    new Client { Name = "Pedro", LastName = "Santos", Age = 40, Adress = "Rua Maringá, 789" },
                    new Client { Name = "Ana", LastName = "Costa", Age = 28, Adress = "Rua Abdala Jabur, 101" },
                    new Client { Name = "Lucas", LastName = "Ferreira", Age = 35, Adress = "Avenida Celso Garcia Sid, 202" }
                };
            foreach (var client in mockClients) {
                await _clientDb.CreateClientAsync(client);
            }
            await LoadClientsAsync();
        }

        private async Task OpenClientDetailPageAsync(int clientID) {
            var page = _serviceProvider.GetRequiredService<ClientDetailPage>();

            Window secondWindow = new Window(page) {
                Title = "Detalhes do Cliente",
            };

            if (page.BindingContext is ClientDetailViewModel vm) {
                await vm.InitializeAsync(clientID, secondWindow.Id);
            }

            WindowSizingHelper.SetWindowSizeAndPosition(secondWindow,
                WindowSizingHelper.WindowSize.ThreeQuartersScreen,
                WindowSizingHelper.WindowPosition.CenterScreen);

            App.Current?.OpenWindow(secondWindow);

            if (!WeakReferenceMessenger.Default.IsRegistered<ClientCollectionChangedMessage>(this)) {
                WeakReferenceMessenger.Default.Register<ClientCollectionChangedMessage>(this, async (r, m) =>{
                    await LoadClientsAsync();
                });
            }
        }
    }
}
