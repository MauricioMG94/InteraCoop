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
    public class CampaignsUnitOfWorkTests
    {
        private Mock<IGenericRepository<Campaign>> _mockGenericRepository = null!;
        private Mock<ICampaignsRepository> _mockCampaignsRepository = null!;
        private CampaignsUnitOfWork _unitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Campaign>>();
            _mockCampaignsRepository = new Mock<ICampaignsRepository>();
            _unitOfWork = new CampaignsUnitOfWork(_mockGenericRepository.Object, _mockCampaignsRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_WithPagination_ShouldReturnData()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<IEnumerable<Campaign>> { WasSuccess = true };
            _mockCampaignsRepository.Setup(x => x.GetAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockCampaignsRepository.Verify(x => x.GetAsync(pagination), Times.Once);
        }


        [TestMethod]
        public async Task GetTotalPagesAsync_ShouldReturnTotalPages()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new ActionResponse<int> { WasSuccess = true };
            _mockCampaignsRepository.Setup(x => x.GetTotalPagesAsync(pagination))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetTotalPagesAsync(pagination);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockCampaignsRepository.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_WithId_ShouldReturnData()
        {
            // Arrange
            int id = 1;
            var expectedResponse = new ActionResponse<Campaign> { WasSuccess = true };
            _mockCampaignsRepository.Setup(x => x.GetAsync(id))
            .ReturnsAsync(expectedResponse);
            // Act
            var result = await _unitOfWork.GetAsync(id);
            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockCampaignsRepository.Verify(x => x.GetAsync(id), Times.Once);
        }


    }
}
