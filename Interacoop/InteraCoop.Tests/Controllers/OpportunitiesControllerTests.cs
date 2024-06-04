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
    public class OpportunitiesControllerTests
    {
        private Mock<IGenericUnitOfWork<Opportunity>> _mockGenericUnitOfWork = null!;
        private Mock<IOpportunitiesUnitOfWork> _mockOpportunitiesUnitOfWork = null!;
        private OpportunitiesController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Opportunity>>();
            _mockOpportunitiesUnitOfWork = new Mock<IOpportunitiesUnitOfWork>();
            _controller = new OpportunitiesController(_mockGenericUnitOfWork.Object, _mockOpportunitiesUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOk()
        {
            // Arrange
            int campaignID = 1;
            int InteractionID = 1;
            var opportunities = new List<Opportunity>
            { new Opportunity { Id = 1, Status = "Desestimada", OpportunityObservations = "Observaciones de prueba.", EstimatedAcquisitionDate = DateTime.Now, RecordDate = DateTime.Now, CampaignId = campaignID, InteractionId = InteractionID }};
            var response = new ActionResponse<IEnumerable<Opportunity>> { WasSuccess = true, Result = opportunities};
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(opportunities, okResult?.Value);
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnError()
        {
            // Arrange
            var response = new ActionResponse<IEnumerable<Opportunity>> { WasSuccess = false };
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync();
            // Assert

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Opportunity>> { WasSuccess = true, Result = new List<Opportunity>() };
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAsync(pagination);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadRequestObjectResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Opportunity>> { WasSuccess = false };
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult), "Expected a BadRequestObjectResult");
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual(response, badRequestResult.Value, "The returned value is not as expected");
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once, "Expected GetAsync to be called once");
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = true, Result = 5 };
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(action.Result, okResult!.Value);
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsBadRequestResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = false };
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);
            // Act
            var result = await _controller.GetPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnOk()
        {
            // Arrange
            int opportunityId = 1;
            int campaignID = 1;
            int InteractionID = 1;
            var opportunity = new Opportunity { Id = opportunityId, Status = "Desestimada", OpportunityObservations = "Observaciones de prueba.", EstimatedAcquisitionDate = DateTime.Now, RecordDate = DateTime.Now, CampaignId = campaignID, InteractionId = InteractionID };
            var response = new ActionResponse<Opportunity> { WasSuccess = true, Result = opportunity };
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetAsync(opportunityId)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(opportunityId);
            // Assert

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(opportunity, okResult?.Value);
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetAsync(opportunityId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnNotFound()
        {
            // Arrange
            var opportnityId = 1;
            var response = new ActionResponse<Opportunity> { WasSuccess = false, Message = "Not Found" };
            _mockOpportunitiesUnitOfWork.Setup(x => x.GetAsync(opportnityId)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(opportnityId);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("Not Found", notFoundResult?.Value);
            _mockOpportunitiesUnitOfWork.Verify(x => x.GetAsync(opportnityId), Times.Once());
        }
    }
}
