using InteraCoop.Backend.Data;
using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Tests.Repositories
{
    [TestClass]
    public class ClientsRepositoryTests
    {
        private DataContext _context = null!;
        private ClientsRepository _repository = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new DataContext(options);
            _repository = new ClientsRepository(_context);
            _context.Clients.AddRange(new List<Client>
            {
                new Client { Id = 1, Name = "Andres Castillo", DocumentType = "CC", Document = 123456789, Address = "Calle en Chiquinquirá", CityId = 1, City = new City { Id = 1, Name = "Chiquinquirá", StateId = 2 }, Telephone = 123456789, RegistryDate = DateTime.Now }
            }); ;
            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsClient_WhenIdExists()
        {
            // Arrange
            int ClientId = 1;
            // Act
            var response = await _repository.GetAsync(ClientId);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Id);
            Assert.AreEqual("Andres Castillo", response.Result.Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsError_WhenIdDoesNotExist()
        {
            // Arrange
            int ClientId = 2;
            // Act
            var response = await _repository.GetAsync(ClientId);
            // Assert
            Assert.IsFalse(response.WasSuccess);
            Assert.IsNull(response.Result);
            Assert.AreEqual("Cliente no existe", response.Message);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllClientsOrderedByName()
        {
            // Act
            var response = await _repository.GetAsync();
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            var clients = response.Result.ToList();
            Assert.AreEqual(1, clients.Count);
            Assert.AreEqual("Andres Castillo", clients[0].Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsFilteredClients()
        {
            // Arrange
            var pagination = new PaginationDTO { Filter = "Andres", RecordsNumber = 10, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var clients = response.Result!.ToList();
            Assert.AreEqual(1, clients.Count);
            Assert.AreEqual("Andres Castillo", clients.First().Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllClients_WhenNoFilterIsProvided()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 10, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var clients = response.Result!.ToList();
            Assert.AreEqual(1, clients.Count);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1 };
            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_WithFilter_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1, Filter = "And" };
            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }
    }
}

