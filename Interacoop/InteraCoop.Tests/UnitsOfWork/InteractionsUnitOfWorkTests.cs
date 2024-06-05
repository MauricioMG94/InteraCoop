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
    public class InteractionsUnitOfWorkTests
    {
        private Mock<IGenericRepository<Interaction>> _mockGenericRepository = null!;
        private Mock<IInteractionsRepository> _mockInteractionsRepository = null!;
        private InteractionsUnitOfWork _unitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Interaction>>();
            _mockInteractionsRepository = new Mock<IInteractionsRepository>();
            _unitOfWork = new InteractionsUnitOfWork(_mockGenericRepository.Object, _mockInteractionsRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_WithPagination_ShouldReturnData()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<IEnumerable<Interaction>> { WasSuccess = true };
            _mockInteractionsRepository.Setup(x => x.GetAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockInteractionsRepository.Verify(x => x.GetAsync(pagination), Times.Once);
        }


        [TestMethod]
        public async Task GetTotalPagesAsync_ShouldReturnTotalPages()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<int> { WasSuccess = true };
            _mockInteractionsRepository.Setup(x => x.GetTotalPagesAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetTotalPagesAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockInteractionsRepository.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_WithId_ShouldReturnData()
        {
            // Arrange
            int id = 1;
            var expectedResponse = new ActionResponse<Interaction> { WasSuccess = true };
            _mockInteractionsRepository.Setup(x => x.GetAsync(id))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetAsync(id);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockInteractionsRepository.Verify(x => x.GetAsync(id), Times.Once);
        }
    }
}
