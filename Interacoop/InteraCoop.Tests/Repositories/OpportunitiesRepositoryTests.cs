using InteraCoop.Backend.Data;
using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Interaction = InteraCoop.Shared.Entities.Interaction;

namespace InteraCoop.Tests.Repositories
{
    [TestClass]
    public class OpportunitiesRepositoryTests
    {
        //private DataContext _context = null!;
        //private OpportunitiesRepository _repository = null!;

        //[TestInitialize]
        //public void Setup()
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;
        //    _context = new DataContext(options);

        //    SetUpUsers();
        //    SetUpClients();
        //    SetUpCities();
        //    SetUpInteractions();
        //    SetUpOpportunities();
        //}

        //private void SetUpUsers()
        //{
        //    _context.Users.AddRange(new List<User>
        //    {
        //        new User
        //        {
        //            Id = "1",
        //            Document = "123456789",
        //            FirstName = "Nombre",
        //            LastName = "Apellido",
        //            Address = "Dirección",
        //            UserType = UserType.Employee,
        //            CityId = 1
        //        }
        //    });
        //}

        //private void SetUpClients()
        //{
        //    _context.Clients.AddRange(new List<Client>
        //    {
        //        new Client
        //        {
        //            Id = 1,
        //            CityId = 1,
        //            Name = "Nombre Cliente",
        //            Document = 123456789,
        //            DocumentType = "Tipo Documento",
        //            Telephone = 123456789,
        //            Address = "Dirección Cliente",
        //            RegistryDate = DateTime.Now,
        //            AuditUpdate = DateTime.Now,
        //            AuditUser = "Usuario Auditoria"
        //        }
        //    });
        //}

        //private void SetUpCities()
        //{
        //    _context.Cities.AddRange(new List<City>
        //    {
        //        new City
        //        {
        //            Id = 1,
        //            Name = "Ciudad",
        //            StateId = 1
        //        }
        //    });
        //}

        //private void SetUpInteractions()
        //{
        //    _context.Interactions.AddRange(new List<Interaction>
        //    {
        //        new Interaction
        //        {
        //            Id = 1,
        //            InteractionType = "Tipo de interacción",
        //            InteractionCreationDate = DateTime.Now,
        //            StartDate = DateTime.Now,
        //            EndDate = DateTime.Now,
        //            Address = "Dirección",
        //            ObservationsInteraction = "Observaciones",
        //            Office = "Oficina",
        //            AuditDate = DateTime.Now,
        //            ClientId = 1,
        //            UserDocument = "123456789",
        //        }
        //    });
        //}

        //private void SetUpOpportunities()
        //{
        //    _context.Opportunities.AddRange(new List<Opportunity>
        //    {
        //        new Opportunity
        //        {
        //            Id = 1,
        //            Status = "Estado",
        //            OpportunityObservations = "Observaciones",
        //            RecordDate = DateTime.Now,
        //            EstimatedAcquisitionDate = DateTime.Now,
        //            CampaignId = 1,
        //            InteractionId = 1
        //        }
        //    });

        //    _context.SaveChanges();
        //}


        //[TestCleanup]
        //public void Cleanup()
        //{
        //    _context.Database.EnsureDeleted();
        //    _context.Dispose();
        //}

        //[TestMethod]
        //public async Task GetAsync_ReturnsOpportunity_WhenIdExists()
        //{
        //    // Arrange
        //    int opportunityId = 1;
        //    // Act
        //    var response = await _repository.GetAsync(opportunityId);
        //    // Assert
        //    Assert.IsTrue(response.WasSuccess);
        //    Assert.IsNotNull(response.Result);
        //    Assert.AreEqual(1, response.Result.Id);
        //    Assert.AreEqual("Estado", response.Result.Status);
        //}

        //[TestMethod]
        //public async Task GetAsync_ReturnsError_WhenIdDoesNotExist()
        //{
        //    // Arrange
        //    int opportunityId = 999;
        //    // Act
        //    var response = await _repository.GetAsync(opportunityId);
        //    // Assert
        //    Assert.IsFalse(response.WasSuccess);
        //    Assert.IsNull(response.Result);
        //    Assert.AreEqual("Oportunidad no existe", response.Message);
        //}

        //[TestMethod]
        //public async Task GetAsync_ReturnsAllOpportunitiesOrderedByStatus()
        //{
        //    // Act
        //    var response = await _repository.GetAsync();

        //    // Assert
        //    Assert.IsTrue(response.WasSuccess);
        //    Assert.IsNotNull(response.Result);
        //    var campaigns = response.Result.ToList();
        //    Assert.AreEqual(1, campaigns.Count);
        //    Assert.AreEqual("Estado", campaigns[0].Status);
        //}


        //[TestMethod]
        //public async Task GetAsync_ReturnsFilteredOpportunities()
        //{
        //    // Arrange
        //    var pagination = new PaginationDTO { Filter = "Estado", RecordsNumber = 10, Page = 1 };
        //    // Act
        //    var response = await _repository.GetAsync(pagination);
        //    // Assert
        //    Assert.IsTrue(response.WasSuccess);
        //    var campaigns = response.Result!.ToList();
        //    Assert.AreEqual(1, campaigns.Count);
        //    Assert.AreEqual("Estado", campaigns.First().Status);
        //}

        //[TestMethod]
        //public async Task GetAsync_ReturnsAllOpportunities_WhenNoFilterIsProvided()
        //{
        //    // Arrange
        //    var pagination = new PaginationDTO { RecordsNumber = 10, Page = 1 };
        //    // Act
        //    var response = await _repository.GetAsync(pagination);
        //    // Assert
        //    Assert.IsTrue(response.WasSuccess);
        //    var opportunities = response.Result!.ToList();
        //    Assert.AreEqual(1, opportunities.Count);
        //}

        //[TestMethod]
        //public async Task GetTotalPagesAsync_ReturnsCorrectNumberOfPages()
        //{
        //    // Arrange
        //    var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1 };
        //    // Act
        //    var response = await _repository.GetTotalPagesAsync(pagination);
        //    // Assert
        //    Assert.IsTrue(response.WasSuccess);
        //    Assert.AreEqual(1, response.Result);
        //}

        //[TestMethod]
        //public async Task GetTotalPagesAsync_WithFilter_ReturnsCorrectNumberOfPages()
        //{
        //    // Arrange
        //    var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1, Filter = "Est" };
        //    // Act
        //    var response = await _repository.GetTotalPagesAsync(pagination);
        //    // Assert
        //    Assert.IsTrue(response.WasSuccess);
        //    Assert.AreEqual(1, response.Result);
        //}
    }
}
