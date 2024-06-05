using InteraCoop.Backend.Controllers;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InteraCoop.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private Mock<IGenericUnitOfWork<Product>> _mockGenericUnitOfWork = null!;
        private Mock<IProductsUnitOfWork> _mockProductsUnitOfWork = null!;
        private ProductsController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Product>>();
            _mockProductsUnitOfWork = new Mock<IProductsUnitOfWork>();
            _controller = new ProductsController(_mockGenericUnitOfWork.Object, _mockProductsUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkResult_WithProductsList()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Tarjeta de crédito", ProductType = "Pasivo", Quota = 0, Rate = 0, Term = "0", Value = 0 }
            };
            _mockProductsUnitOfWork.Setup(x => x.GetAllAsync()).ReturnsAsync(products);
            // Act
            var result = await _controller.GetComboAsync();
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult!.Value);
            var returnedProducts = okResult.Value as IEnumerable<Product>;
            Assert.IsNotNull(returnedProducts);
            Assert.AreEqual(products.Count, returnedProducts.Count());
            Assert.IsTrue(products.All(p => returnedProducts.Any(rp => rp.Id == p.Id && rp.Name == p.Name)));
            _mockProductsUnitOfWork.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOk()
        {
            // Arrange
            var products = new List<Product>
            { new Product { Id = 1, Name = "Tarjeta de crédito", ProductType = "Pasivo", Quota = 0, Rate = 0, Term = "0", Value = 0 }};
            var response = new ActionResponse<IEnumerable<Product>> { WasSuccess = true, Result = products };
            _mockProductsUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync();
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(products, okResult?.Value);
            _mockProductsUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnError()
        {
            // Arrange
            var response = new ActionResponse<IEnumerable<Product>> { WasSuccess = false };
            _mockProductsUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync();
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockProductsUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Product>> { WasSuccess = true, Result = new List<Product>() };
            _mockProductsUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);
            //Act
            var result = await _controller.GetAsync(pagination);
            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockProductsUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadRequest_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Product>> { WasSuccess = false };
            _mockProductsUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(response, badRequestResult!.Value);
            _mockProductsUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = true, Result = 5 };
            _mockProductsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);
            // Act
            var result = await _controller.GetPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(action.Result, okResult!.Value);
            _mockProductsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsBadRequestResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = false };
            _mockProductsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);
            // Act
            var result = await _controller.GetPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockProductsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }
    }
}