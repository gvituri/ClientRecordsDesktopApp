using ClientRecordsDesktopApp.Models;
using ClientRecordsDesktopApp.Services;

namespace ClientRecordsDesktopAppUnitTests.Services {
    public class ClientDatabaseServiceTests {
        private async Task<(ClientDatabaseService service, string dbPath)> CreateServiceAsync() {
            string dbPath = Path.GetTempFileName();
            var service = new ClientDatabaseService(dbPath);
            await service.InitializeAsync();
            return (service, dbPath);
        }

        private async Task CleanupAsync(ClientDatabaseService service, string dbPath) {
            await service.DisposeAsync();
            if (File.Exists(dbPath))
                File.Delete(dbPath);
        }

        [Fact]
        public async Task CreateClientAsync_WithExtraSpaces_ShouldNormalizeValues() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = " John ", LastName = " Doe ", Adress = " Street ", Age = 10 };

            try {
                // Act
                var insertionId = await service.CreateClientAsync(client);
                var savedClient = await service.GetClientAsync(insertionId);

                // Assert
                Assert.Equal("John", savedClient.Name);
                Assert.Equal("Doe", savedClient.LastName);
                Assert.Equal("Street", savedClient.Adress);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task CreateClientAsync_WithValidClient_ShouldReturnInsertedId() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = "Jane", LastName = "Smith", Adress = "Main St", Age = 25 };

            try {
                // Act
                var insertionId = await service.CreateClientAsync(client);

                // Assert
                Assert.True(insertionId > 0);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task CreateClientAsync_WithNullValues_ShouldHandleGracefully() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = null, LastName = null, Adress = null, Age = null };

            try {
                // Act
                var insertionId = await service.CreateClientAsync(client);
                var savedClient = await service.GetClientAsync(insertionId);

                // Assert
                Assert.True(insertionId > 0);
                Assert.Null(savedClient.Name);
                Assert.Null(savedClient.LastName);
                Assert.Null(savedClient.Adress);
                Assert.Null(savedClient.Age);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task GetClientAsync_WithExistingId_ShouldReturnClient() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = "Test", LastName = "User", Adress = "Test St", Age = 30 };

            try {
                var insertionId = await service.CreateClientAsync(client);

                // Act
                var retrievedClient = await service.GetClientAsync(insertionId);

                // Assert
                Assert.NotNull(retrievedClient);
                Assert.Equal("Test", retrievedClient.Name);
                Assert.Equal("User", retrievedClient.LastName);
                Assert.Equal("Test St", retrievedClient.Adress);
                Assert.Equal(30, retrievedClient.Age);
                Assert.Equal(insertionId, retrievedClient.Id);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task GetClientAsync_WithNonExistingId_ShouldReturnNull() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();

            try {
                // Act
                var retrievedClient = await service.GetClientAsync(999);

                // Assert
                Assert.Null(retrievedClient);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task GetClientsAsync_WithEmptyDatabase_ShouldReturnEmptyList() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();

            try {
                // Act
                var clients = await service.GetClientsAsync();

                // Assert
                Assert.NotNull(clients);
                Assert.Empty(clients);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task GetClientsAsync_WithMultipleClients_ShouldReturnAllClients() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client1 = new Client { Name = "John", LastName = "Doe", Adress = "Street 1", Age = 25 };
            var client2 = new Client { Name = "Jane", LastName = "Smith", Adress = "Street 2", Age = 30 };
            var client3 = new Client { Name = "Bob", LastName = "Johnson", Adress = "Street 3", Age = 35 };

            try {
                await service.CreateClientAsync(client1);
                await service.CreateClientAsync(client2);
                await service.CreateClientAsync(client3);

                // Act
                var clients = await service.GetClientsAsync();

                // Assert
                Assert.Equal(3, clients.Count);
                Assert.Contains(clients, c => c.Name == "John" && c.LastName == "Doe");
                Assert.Contains(clients, c => c.Name == "Jane" && c.LastName == "Smith");
                Assert.Contains(clients, c => c.Name == "Bob" && c.LastName == "Johnson");
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task UpdateClientAsync_WithExistingClient_ShouldUpdateValues() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = "Original", LastName = "Name", Adress = "Original St", Age = 25 };

            try {
                var insertionId = await service.CreateClientAsync(client);
                var savedClient = await service.GetClientAsync(insertionId);

                // Modify the client
                savedClient.Name = "Updated";
                savedClient.LastName = "LastName";
                savedClient.Adress = "Updated St";
                savedClient.Age = 30;

                // Act
                var updateResult = await service.UpdateClientAsync(savedClient);
                var updatedClient = await service.GetClientAsync(insertionId);

                // Assert
                Assert.Equal(1, updateResult); // SQLite returns 1 for successful update
                Assert.Equal("Updated", updatedClient.Name);
                Assert.Equal("LastName", updatedClient.LastName);
                Assert.Equal("Updated St", updatedClient.Adress);
                Assert.Equal(30, updatedClient.Age);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task UpdateClientAsync_WithExtraSpaces_ShouldNormalizeValues() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = "Test", LastName = "User", Adress = "Test St", Age = 25 };

            try {
                var insertionId = await service.CreateClientAsync(client);
                var savedClient = await service.GetClientAsync(insertionId);

                // Modify with extra spaces
                savedClient.Name = " Updated ";
                savedClient.LastName = " Name ";
                savedClient.Adress = " Updated Street ";

                // Act
                await service.UpdateClientAsync(savedClient);
                var updatedClient = await service.GetClientAsync(insertionId);

                // Assert
                Assert.Equal("Updated", updatedClient.Name);
                Assert.Equal("Name", updatedClient.LastName);
                Assert.Equal("Updated Street", updatedClient.Adress);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task UpdateClientAsync_WithNonExistingClient_ShouldReturnZero() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var nonExistingClient = new Client { Id = 999, Name = "Ghost", LastName = "User", Adress = "Nowhere", Age = 0 };

            try {
                // Act
                var updateResult = await service.UpdateClientAsync(nonExistingClient);

                // Assert
                Assert.Equal(0, updateResult); // SQLite returns 0 when no rows are affected
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task DeleteClientAsync_WithExistingClient_ShouldRemoveClient() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = "ToDelete", LastName = "User", Adress = "Delete St", Age = 25 };

            try {
                var insertionId = await service.CreateClientAsync(client);
                var savedClient = await service.GetClientAsync(insertionId);

                // Act
                var deleteResult = await service.DeleteClientAsync(savedClient);
                var deletedClient = await service.GetClientAsync(insertionId);

                // Assert
                Assert.Equal(1, deleteResult); // SQLite returns 1 for successful delete
                Assert.Null(deletedClient);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task DeleteClientAsync_WithNonExistingClient_ShouldReturnZero() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var nonExistingClient = new Client { Id = 999, Name = "Ghost", LastName = "User", Adress = "Nowhere", Age = 0 };

            try {
                // Act
                var deleteResult = await service.DeleteClientAsync(nonExistingClient);

                // Assert
                Assert.Equal(0, deleteResult); // SQLite returns 0 when no rows are affected
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task InitializeAsync_CalledMultipleTimes_ShouldNotCreateMultipleConnections() {
            // Arrange
            string dbPath = Path.GetTempFileName();
            var service = new ClientDatabaseService(dbPath);

            try {
                // Act
                await service.InitializeAsync();
                await service.InitializeAsync(); // Call again
                await service.InitializeAsync(); // Call again

                // Assert - Should not throw exception and should work normally
                var client = new Client { Name = "Test", LastName = "User", Adress = "Test St", Age = 25 };
                var insertionId = await service.CreateClientAsync(client);
                var savedClient = await service.GetClientAsync(insertionId);

                Assert.NotNull(savedClient);
                Assert.Equal("Test", savedClient.Name);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }

        [Fact]
        public async Task CompleteWorkflow_CreateUpdateDelete_ShouldWorkCorrectly() {
            // Arrange
            var (service, dbPath) = await CreateServiceAsync();
            var client = new Client { Name = "Workflow", LastName = "Test", Adress = "Test St", Age = 25 };

            try {
                // Act & Assert - Create
                var insertionId = await service.CreateClientAsync(client);
                Assert.True(insertionId > 0);

                // Act & Assert - Read
                var savedClient = await service.GetClientAsync(insertionId);
                Assert.NotNull(savedClient);
                Assert.Equal("Workflow", savedClient.Name);

                // Act & Assert - Update
                savedClient.Name = "Updated Workflow";
                var updateResult = await service.UpdateClientAsync(savedClient);
                Assert.Equal(1, updateResult);

                var updatedClient = await service.GetClientAsync(insertionId);
                Assert.Equal("Updated Workflow", updatedClient.Name);

                // Act & Assert - Delete
                var deleteResult = await service.DeleteClientAsync(updatedClient);
                Assert.Equal(1, deleteResult);

                var deletedClient = await service.GetClientAsync(insertionId);
                Assert.Null(deletedClient);
            } finally {
                await CleanupAsync(service, dbPath);
            }
        }
    }
}
