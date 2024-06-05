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
    public class ClientsUnitOfWorkTests
    {
        private Mock<IGenericRepository<Client>> _mockGenericRepository = null!;
        private Mock<IClientsRepository> _mockClientsRepository = null!;
        private ClientsUnitOfWork _unitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Client>>();
            _mockClientsRepository = new Mock<IClientsRepository>();
            _unitOfWork = new ClientsUnitOfWork(_mockGenericRepository.Object, _mockClientsRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsClient_WhenIdExists()
        {
            // Arrange
            int clientId = 1;
            var client = new Client { Id = clientId, Name = "Test Client" };
            var response = new ActionResponse<Client> { WasSuccess = true, Result = client };
            _mockClientsRepository.Setup(repo => repo.GetAsync(clientId)).ReturnsAsync(response);
            // Act
            var result = await _unitOfWork.GetAsync(clientId);
            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(clientId, result.Result.Id);
            Assert.AreEqual("Test Client", result.Result.Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsClients_WhenPaginationIsValid()
        {
            // Arrange
            var pagination = new PaginationDTO { Page = 1, RecordsNumber = 10 };
            var clients = new List<Client>
            {
                new Client { Id = 1, Name = "Client 1" },
                new Client { Id = 2, Name = "Client 2" }
            };
            var response = new ActionResponse<IEnumerable<Client>> { WasSuccess = true, Result = clients };
            _mockClientsRepository.Setup(repo => repo.GetAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await (_unitOfWork as IClientsUnitOfWork).GetAsync(pagination);
            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(2, result.Result.Count());
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsTotalPages_WhenPaginationIsValid()
        {
            // Arrange
            var pagination = new PaginationDTO { Page = 1, RecordsNumber = 10 };
            var totalPages = 5;
            var response = new ActionResponse<int> { WasSuccess = true, Result = totalPages };
            _mockClientsRepository.Setup(repo => repo.GetTotalPagesAsync(pagination)).ReturnsAsync(response);
            // Act
            var result = await (_unitOfWork as IClientsUnitOfWork).GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(totalPages, result.Result);
        }
    }
}
