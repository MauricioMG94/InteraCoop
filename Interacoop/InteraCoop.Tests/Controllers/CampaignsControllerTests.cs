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
    public class CampaignsControllerTests
    {
        private Mock<IGenericUnitOfWork<Campaign>> _mockGenericUnitOfWork = null!;
        private Mock<ICampaignsUnitOfWork> _mockCampaignsUnitOfWork = null!; 
        private CampaignsController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Campaign>>();
            _mockCampaignsUnitOfWork = new Mock<ICampaignsUnitOfWork>();
            _controller = new CampaignsController(_mockGenericUnitOfWork.Object, _mockCampaignsUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOk()
        {
            // Arrange
            var products1 = new List<Product>            {
                new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
            };
            var campaigns = new List<Campaign>
            { new Campaign { Id = 1, CampaignIdentifier = "CP001", CampaignName = "Campaña de prueba", CampaignType = "Tipo 1", Description = "Descripción de prueba", Status = "Asignada", ProductsList = products1, StartDate = DateTime.Now, EndDate = DateTime.Now }};            
            var response = new ActionResponse<IEnumerable<Campaign>> { WasSuccess = true, Result = campaigns };
            _mockCampaignsUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(campaigns, okResult?.Value);
           _mockCampaignsUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnError()
        {
            // Arrange
            var response = new ActionResponse<IEnumerable<Campaign>> { WasSuccess = false };
            _mockCampaignsUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync();
            // Assert

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCampaignsUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {   
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Campaign>> { WasSuccess = true, Result = new List<Campaign>() };
            _mockCampaignsUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAsync(pagination);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockCampaignsUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once()); 
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadRequestResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Campaign>> { WasSuccess = false };
            _mockCampaignsUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCampaignsUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = true, Result = 5 };
            _mockCampaignsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);
            
            // Act
            var result = await _controller.GetPagesAsync(pagination);
            
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(action.Result, okResult!.Value);
            _mockCampaignsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsBadRequestResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = false };
            _mockCampaignsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);
            // Act
            var result = await _controller.GetPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCampaignsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnOk()
        {
            // Arrange
            var campaignId = 1;
            var products1 = new List<Product>
            {
                new Product { ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                new Product { ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
            };
            var campaign = new Campaign { Id = campaignId, CampaignIdentifier = "CP001", CampaignName = "Campaña de prueba", CampaignType = "Tipo 1", Description = "Descripción de prueba", Status = "Asignada", ProductsList = products1, StartDate = DateTime.Now, EndDate = DateTime.Now };
            var response = new ActionResponse<Campaign> { WasSuccess = true, Result = campaign };
            _mockCampaignsUnitOfWork.Setup(x => x.GetAsync(campaignId)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(campaignId);
            // Assert

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(campaign, okResult?.Value);
            _mockCampaignsUnitOfWork.Verify(x => x.GetAsync(campaignId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnNotFound()
        {
            // Arrange
            var campaignId = 1;
            var response = new ActionResponse<Campaign> { WasSuccess = false, Message = "Not Found" };
            _mockCampaignsUnitOfWork.Setup(x => x.GetAsync(campaignId)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(campaignId);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("Not Found", notFoundResult?.Value);
            _mockCampaignsUnitOfWork.Verify(x => x.GetAsync(campaignId), Times.Once());
        }
    }
}
