using InteraCoop.Backend.Data;
using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Tests.Repositories
{
    [TestClass]
    public class CampaignsRepositoryTests
    {
        private DataContext _context = null!;
        private CampaignsRepository _repository = null!;
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new DataContext(options);
            _repository = new CampaignsRepository(_context);
            var products1 = new List<Product>            {
                new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
            };
            _context.Campaigns.AddRange(new List<Campaign>
            {
                new Campaign { Id = 1, CampaignIdentifier = "CP001", CampaignName = "Campaña de prueba", CampaignType = "Tipo 1", Description = "Descripción de prueba", Status = "Asignada", ProductsList = products1, StartDate = DateTime.Now, EndDate = DateTime.Now }
            });
            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsCampaign_WhenIdExists()
        {            
            // Arrange
            int CampaignId = 1;
            // Act
            var response = await _repository.GetAsync(CampaignId);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Id);
            Assert.AreEqual("Campaña de prueba", response.Result.CampaignName);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsError_WhenIdDoesNotExist()
        {
            // Arrange
            int CampaignId = 2;
            // Act
            var response = await _repository.GetAsync(CampaignId);
            // Assert
            Assert.IsFalse(response.WasSuccess);
            Assert.IsNull(response.Result);
            Assert.AreEqual("Campaña no existe", response.Message);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllCampaignsOrderedByName()
        {
            // Act
            var response = await _repository.GetAsync();

            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            var campaigns = response.Result.ToList();
            Assert.AreEqual(1, campaigns.Count);
            Assert.AreEqual("Campaña de prueba", campaigns[0].CampaignName);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsFilteredCampaigns()
        {
            // Arrange
            var pagination = new PaginationDTO { Filter = "Campaña de prueba", RecordsNumber = 10, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var categories = response.Result!.ToList();
            Assert.AreEqual(1, categories.Count);
            Assert.AreEqual("Campaña de prueba", categories.First().CampaignName);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllCampaigns_WhenNoFilterIsProvided()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 10, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var campaigns = response.Result!.ToList();
            Assert.AreEqual(1, campaigns.Count);
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
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1, Filter = "Cam" };
            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }
    }
}
