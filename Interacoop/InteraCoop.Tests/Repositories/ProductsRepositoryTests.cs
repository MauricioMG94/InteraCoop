using InteraCoop.Backend.Data;
using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Tests.Repositories
{
    [TestClass]
    public class ProductsRepositoryTests
    {
        private DataContext _context = null!;
        private ProductsRepository _repository = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new DataContext(options);
            _repository = new ProductsRepository(_context);
            _context.Products.AddRange(new List<Product>
            {
                new Product { Id = 1, ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                new Product { Id = 2, ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
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
        public async Task GetAllAsync_ReturnsAllProducts_OrderedByName()
        {
            // Act
            var result = await _repository.GetAllAsync();
            // Assert
            Assert.IsNotNull(result);
            var products = result.ToList();
            Assert.AreEqual(2, products.Count);
            Assert.AreEqual("Tarjeta Mastercard", products[0].Name);
            Assert.AreEqual("Tarjeta Visa", products[1].Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsProduct_WhenIdExists()
        {
            // Arrange
            int ProductId = 1;
            // Act
            var response = await _repository.GetAsync(ProductId);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Id);
            Assert.AreEqual("Tarjeta Visa", response.Result.Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllProductsOrderedByName()
        {
            // Act
            var response = await _repository.GetAsync();
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            var products = response.Result.ToList();
            Assert.AreEqual(2, products.Count);
            Assert.AreEqual("Tarjeta Mastercard", products[0].Name);
            Assert.AreEqual("Tarjeta Visa", products[1].Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsFilteredProducts()
        {
            // Arrange
            var pagination = new PaginationDTO { Filter = "Tarjeta Visa", RecordsNumber = 10, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var products = response.Result!.ToList();
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual("Tarjeta Visa", products.First().Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllProducts_WhenNoFilterIsProvided()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 10, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var products = response.Result!.ToList();
            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 2, Page = 1 };
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
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1, Filter = "Visa" };
            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }
    }
}
