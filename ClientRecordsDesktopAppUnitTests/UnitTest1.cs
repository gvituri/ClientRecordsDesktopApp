using ClientRecordsDesktopApp.Models;
using ClientRecordsDesktopApp.Services;

namespace ClientRecordsDesktopAppUnitTests {
    public class UnitTest1 {
        [Fact]
        public async Task Test1() {
            // Arrange
            string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "testClients.db3");
            var service = new ClientDatabaseService(dbPath);
            await service.InitializeAsync();
            var client = new Client { Name = " John ", LastName = " Doe ", Adress = " Street ", Age = 10 };

            // Act
            var insertionId = await service.CreateClientAsync(client);
            var savedClient = await service.GetClientAsync(insertionId);

            // Assert
            Assert.Equal("John", savedClient.Name);
            Assert.Equal("Doe", savedClient.LastName);
            Assert.Equal("Street", savedClient.Adress);
        }
    }
}
