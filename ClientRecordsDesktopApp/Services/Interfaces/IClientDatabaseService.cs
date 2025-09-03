using ClientRecordsDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Services.Interfaces {
    public interface IClientDatabaseService {
        Task InitializeAsync();
        Task<List<Client>> GetClientsAsync();
        Task<Client> GetClientAsync(int id);
        Task<int> CreateClientAsync(Client client);
        Task<int> CreateClientRangeAsync(List<Client> clients);
        Task<int> UpdateClientAsync(Client client);
        Task<int> DeleteClientAsync(Client client);
    }
}
