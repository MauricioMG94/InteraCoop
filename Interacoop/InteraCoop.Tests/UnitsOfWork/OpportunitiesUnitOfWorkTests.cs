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
    public class OpportunitiesUnitOfWorkTests
    {
        private Mock<IGenericRepository<Opportunity>> _mockGenericRepository = null!;
        private Mock<IOpportunitiesRepository> _mockOpportunitiesRepository = null!;
        private OpportunitiesUnitOfWork _unitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Opportunity>>();
            _mockOpportunitiesRepository = new Mock<IOpportunitiesRepository>();
            _unitOfWork = new OpportunitiesUnitOfWork(_mockGenericRepository.Object, _mockOpportunitiesRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_WithPagination_ShouldReturnData()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<IEnumerable<Opportunity>> { WasSuccess = true };
            _mockOpportunitiesRepository.Setup(x => x.GetAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockOpportunitiesRepository.Verify(x => x.GetAsync(pagination), Times.Once);
        }


        [TestMethod]
        public async Task GetTotalPagesAsync_ShouldReturnTotalPages()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<int> { WasSuccess = true };
            _mockOpportunitiesRepository.Setup(x => x.GetTotalPagesAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetTotalPagesAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockOpportunitiesRepository.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_WithId_ShouldReturnData()
        {
            // Arrange
            int id = 1;
            var expectedResponse = new ActionResponse<Opportunity> { WasSuccess = true };
            _mockOpportunitiesRepository.Setup(x => x.GetAsync(id))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetAsync(id);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockOpportunitiesRepository.Verify(x => x.GetAsync(id), Times.Once);
        }
    }
}
