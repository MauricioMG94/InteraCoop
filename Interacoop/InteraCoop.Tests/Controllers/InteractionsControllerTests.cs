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
    public class InteractionsControllerTests
    {
        private Mock<IGenericUnitOfWork<Interaction>> _mockGenericUnitOfWork = null!;
        private Mock<IInteractionsUnitOfWork> _mockInteractionsUnitOfWork = null!;
        private InteractionsController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Interaction>>();
            _mockInteractionsUnitOfWork = new Mock<IInteractionsUnitOfWork>();
            _controller = new InteractionsController(_mockGenericUnitOfWork.Object, _mockInteractionsUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnOk()
        {
            // Arrange
            var interactions = new List<Interaction>
            { new Interaction { Id = 1, ClientId = 2, Client = new Client { Id = 2, Name = "Andres" }, InteractionType = "Llamada entrante", Address = "Dirección de prueba", ObservationsInteraction = "Sin novedades.", Office = "OF001", UserDocument = "123456", User = new User { Document = "123456" }, InteractionCreationDate = DateTime.Today, EndDate = DateTime.Now, StartDate = DateTime.Today }};
            var response = new ActionResponse<IEnumerable<Interaction>> { WasSuccess = true, Result = interactions };
            _mockInteractionsUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync();
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(interactions, okResult?.Value);
            _mockInteractionsUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnError()
        {
            // Arrange
            var response = new ActionResponse<IEnumerable<Interaction>> { WasSuccess = false };
            _mockInteractionsUnitOfWork.Setup(x => x.GetAsync())
            .ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync();
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockInteractionsUnitOfWork.Verify(x => x.GetAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new ActionResponse<IEnumerable<Interaction>> { WasSuccess = true, Result = new List<Interaction>() };
            _mockInteractionsUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAsync(pagination);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockInteractionsUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = true, Result = 5 };
            _mockInteractionsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);
            // Act
            var result = await _controller.GetPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(action.Result, okResult!.Value);
            _mockInteractionsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsBadRequestResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new ActionResponse<int> { WasSuccess = false };
            _mockInteractionsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);
            // Act
            var result = await _controller.GetPagesAsync(pagination);
            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockInteractionsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnOk()
        {
            // Arrange
            var interactionId = 1;
            var interaction = new Interaction { Id = interactionId, ClientId = 2, Client = new Client { Id = 2, Name = "Andres" }, InteractionType = "Llamada entrante", Address = "Dirección de prueba", ObservationsInteraction = "Sin novedades.", Office = "OF001", UserDocument = "123456", User = new User { Document = "123456" }, InteractionCreationDate = DateTime.Today, EndDate = DateTime.Now, StartDate = DateTime.Today };
            var response = new ActionResponse<Interaction> { WasSuccess = true, Result = interaction };
            _mockInteractionsUnitOfWork.Setup(x => x.GetAsync(interactionId)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(interactionId);
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(interaction, okResult?.Value);
            _mockInteractionsUnitOfWork.Verify(x => x.GetAsync(interactionId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnNotFound()
        {
            // Arrange
            var interactionId = 1;
            var response = new ActionResponse<Interaction> { WasSuccess = false, Message = "Not Found" };
            _mockInteractionsUnitOfWork.Setup(x => x.GetAsync(interactionId)).ReturnsAsync(response);
            // Act
            var result = await _controller.GetAsync(interactionId);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("Not Found", notFoundResult?.Value);
            _mockInteractionsUnitOfWork.Verify(x => x.GetAsync(interactionId), Times.Once());
        }
    }
}
