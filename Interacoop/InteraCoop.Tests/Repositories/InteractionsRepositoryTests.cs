using InteraCoop.Backend.Data;
using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Tests.Repositories
{
    [TestClass]
    public class InteractionsRepositoryTests
    {
        private DataContext _context = null!;
        private InteractionsRepository _repository = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new DataContext(options);
            _repository = new InteractionsRepository(_context);
            _context.Interactions.AddRange(new List<Interaction>
            {
                new Interaction { Id = 1, InteractionType = "Llamada entrante", Address = "Calle principal", Office = "OF101", StartDate = DateTime.Now, EndDate = DateTime.Now, ObservationsInteraction = "Sin novedades.", ClientId = 1, Client = new Client { Id = 1, Name = "Andres Castillo", DocumentType = "CC", Document = 123456789, Address = "Calle en Chiquinquirá", CityId = 1, City = new City { Id = 1, Name = "Chiquinquirá", StateId = 2 }, Telephone = 123456789, RegistryDate = DateTime.Now }, UserDocument = "123456", User = new User { Id ="af5", FirstName = "Andres", LastName = "Castillo", Address = "Calle de Chiquinquirá", Document = "123456", CityId = 2, City = new City { Id = 2, Name = "Ubaté", StateId = 2 }, Email = "andres@yopmail.com", EmailConfirmed = true, UserType = UserType.Employee }, InteractionCreationDate = DateTime.Now }
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
        public async Task GetAsync_ReturnsInteraction_WhenInteractionExists()
        {
            var interactionId = 1;
            // Act
            var response = await _repository.GetAsync(interactionId);
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Id);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsError_WhenInteractionDoesNotExist()
        {
            // Act
            var response = await _repository.GetAsync(999); // Id that does not exist
            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.WasSuccess);
            Assert.AreEqual("La Interacción no existe", response.Message);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllInteractionsOrderedByType()
        {
            // Act
            var response = await _repository.GetAsync();
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            var interactions = response.Result.ToList();
            Assert.AreEqual(1, interactions.Count);
            Assert.AreEqual("Llamada entrante", interactions[0].InteractionType);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsFilteredInteractions()
        {
            // Arrange
            var pagination = new PaginationDTO { Filter = "Llamada entrante", RecordsNumber = 1, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var interactions = response.Result!.ToList();
            Assert.AreEqual(1, interactions.Count);
            Assert.AreEqual("Llamada entrante", interactions.First().InteractionType);
            Assert.AreEqual("Andres Castillo", interactions.First().Client.Name);
            Assert.AreEqual("Andres", interactions.First().User.FirstName);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllInteractions_WhenNoFilterIsProvided()
        {
            // Arrange
            var pagination = new PaginationDTO { Page = 1, RecordsNumber = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Count());
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
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1, Filter = "Llamada" };
            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }
    }
}

