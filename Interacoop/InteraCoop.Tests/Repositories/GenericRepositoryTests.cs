using InteraCoop.Backend.Data;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Tests.Repositories
{
    [TestClass]
    public class GenericRepositoryTests
    {
        private DataContext _context = null!;
        private DbContextOptions<DataContext> _options = null!;
        private GenericRepository<Campaign> _repository = null!;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(_options);
            _repository = new GenericRepository<Campaign>(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnEntity()
        {
            // Arrange
            var products1 = new List<Product>            {
                new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
            };
            var testEntity = new Campaign { Id = 1, CampaignIdentifier = "CP001", CampaignName = "Campaña de prueba", CampaignType = "Tipo 1", Description = "Descripción de prueba", Status = "Asignada", ProductsList = products1, StartDate = DateTime.Now, EndDate = DateTime.Now };
            await _context.Set<Campaign>().AddAsync(testEntity);
            await _context.SaveChangesAsync();
            // Act
            var response = await _repository.GetAsync(testEntity.Id);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual("Campaña de prueba", response.Result.CampaignName);
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnEntities()
        {
            // Arrange
            var products1 = new List<Product>            {
                new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
            };
            await _context.Set<Campaign>().AddRangeAsync(new List<Campaign>
            {
                new Campaign { Id = 1, CampaignIdentifier = "CP001", CampaignName = "Campaña de prueba", CampaignType = "Tipo 1", Description = "Descripción de prueba", Status = "Asignada", ProductsList = products1, StartDate = DateTime.Now, EndDate = DateTime.Now }
            });
            await _context.SaveChangesAsync();

            // Act
            var paginationDTO = new PaginationDTO { RecordsNumber = 1 };
            var response = await _repository.GetAsync(paginationDTO);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Count());
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ShouldReturnTotalPages()
        {
            // Arrange
            var products1 = new List<Product>            {
                new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
            };
            await _context.Set<Campaign>().AddRangeAsync(new List<Campaign>
            {
                new Campaign { Id = 1, CampaignIdentifier = "CP001", CampaignName = "Campaña de prueba", CampaignType = "Tipo 1", Description = "Descripción de prueba", Status = "Asignada", ProductsList = products1, StartDate = DateTime.Now, EndDate = DateTime.Now }
            });
            await _context.SaveChangesAsync();
            var paginationDTO = new PaginationDTO { RecordsNumber = 1 };
            // Act
            var response = await _repository.GetTotalPagesAsync(paginationDTO);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }

    }
}
