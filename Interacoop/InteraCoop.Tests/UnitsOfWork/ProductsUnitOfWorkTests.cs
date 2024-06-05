using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using Moq;

namespace InteraCoop.Tests.UnitsOfWork
{
    [TestClass]
    public class ProductsUnitOfWorkTests
    {
        private Mock<IGenericRepository<Product>> _mockGenericRepository = null!;
        private Mock<IProductsRepository> _mockProductsRepository = null!;
        private ProductsUnitOfWork _unitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Product>>();
            _mockProductsRepository = new Mock<IProductsRepository>();
            _unitOfWork = new ProductsUnitOfWork(_mockGenericRepository.Object, _mockProductsRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_WithPagination_ShouldReturnData()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<IEnumerable<Product>> { WasSuccess = true };
            _mockProductsRepository.Setup(x => x.GetAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockProductsRepository.Verify(x => x.GetAsync(pagination), Times.Once);
        }


        [TestMethod]
        public async Task GetTotalPagesAsync_ShouldReturnTotalPages()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<int> { WasSuccess = true };
            _mockProductsRepository.Setup(x => x.GetTotalPagesAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetTotalPagesAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockProductsRepository.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once);
        }
    }
}
