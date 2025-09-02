using ClientRecordsDesktopApp.Models;
using ClientRecordsDesktopApp.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Services {
    public class ClientDatabaseService : IClientDatabaseService {
        private SQLiteAsyncConnection _db;
        private readonly string _dbPath;

        public ClientDatabaseService(string dbPath) {
            _dbPath = dbPath;
        }

        public async Task InitializeAsync() {
            if (_db != null)
                return;

            _db = new SQLiteAsyncConnection(_dbPath);
            await _db.CreateTableAsync<Client>();
        }

        public Task<List<Client>> GetClientsAsync()
            => _db.Table<Client>().ToListAsync();

        public Task<Client> GetClientAsync(int id)
            => _db.FindAsync<Client>(id);
        public Task<int> CreateClientAsync(Client client)
            => _db.InsertAsync(client);

        public Task<int> UpdateClientAsync(Client client)
            => _db.UpdateAsync(client);

        public Task<int> DeleteClientAsync(Client client)
            => _db.DeleteAsync(client);
    }
}
