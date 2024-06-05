using InteraCoop.Backend.Data;
using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Tests.Repositories
{
    [TestClass]
    public class OpportunitiesRepositoryTests
    {
        private DataContext _context = null!;
        private OpportunitiesRepository _repository = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new DataContext(options);
            _repository = new OpportunitiesRepository(_context);

            var products1 = new List<Product>
        {
            new Product { Id = 1, ProductType = "Activo", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { Id = 2, ProductType = "Activo", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
        };

            var campaign = new Campaign
            {
                Id = 1,
                CampaignIdentifier = "CP001",
                CampaignName = "Campaña de prueba",
                CampaignType = "Tipo 1",
                Description = "Descripción de prueba",
                Status = "Asignada",
                ProductsList = products1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var interaction = new Interaction
            {
                Id = 1,
                InteractionType = "Llamada entrante",
                Address = "Calle principal",
                Office = "OF101",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ObservationsInteraction = "Sin novedades.",
                ClientId = 1,
                Client = new Client
                {
                    Id = 1,
                    Name = "Andres Castillo",
                    DocumentType = "CC",
                    Document = 123456789,
                    Address = "Calle en Chiquinquirá",
                    CityId = 1,
                    City = new City { Id = 1, Name = "Chiquinquirá", StateId = 2 },
                    Telephone = 123456789,
                    RegistryDate = DateTime.Now
                },
                UserDocument = "123456",
                User = new User
                {
                    Id = "af5",
                    FirstName = "Andres",
                    LastName = "Castillo",
                    Address = "Calle de Chiquinquirá",
                    Document = "123456",
                    CityId = 2,
                    City = new City { Id = 2, Name = "Ubaté", StateId = 2 },
                    Email = "andres@yopmail.com",
                    EmailConfirmed = true,
                    UserType = UserType.Employee
                }
            };

            _context.Opportunities.AddRange(new List<Opportunity>
        {
            new Opportunity
            {
                Id = 1,
                CampaignId = 1,
                Campaign = campaign,
                Status = "Desestimada",
                RecordDate = DateTime.Now,
                EstimatedAcquisitionDate = DateTime.Now,
                OpportunityObservations = "Sin novedades.",
                InteractionId = 1,
                Interaction = interaction
            }
        });

            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOpportunity_WhenIdExists()
        {
            // Arrange
            int opportunityId = 1;
            // Act
            var response = await _repository.GetAsync(opportunityId);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Id);
            Assert.AreEqual("Desestimada", response.Result.Status);
            Assert.AreEqual("CP001", response.Result.Campaign.CampaignIdentifier);
            Assert.AreEqual("Llamada entrante", response.Result.Interaction.InteractionType);
            Assert.AreEqual("Andres Castillo", response.Result.Interaction.Client.Name);
            Assert.AreEqual("Andres", response.Result.Interaction.User.FirstName);
            Assert.AreEqual("Tarjeta Visa", response.Result.Campaign.ProductsList.FirstOrDefault().Name);

        }

        [TestMethod]
        public async Task GetAsync_ReturnsError_WhenInteractionDoesNotExist()
        {
            //Arrange
            var OpportunityId = 999;
            // Act
            var response = await _repository.GetAsync(OpportunityId);
            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.WasSuccess);
            Assert.AreEqual("Oportunidad no existe", response.Message);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllOpportunities()
        {
            // Act
            var response = await _repository.GetAsync();
            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.WasSuccess);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(1, response.Result.Count());
            var opportunity = response.Result.First();
            Assert.AreEqual(1, opportunity.Id);
            Assert.AreEqual("Desestimada", opportunity.Status);
            Assert.IsNotNull(opportunity.Campaign);
            Assert.AreEqual(1, opportunity.Campaign.Id);
            Assert.AreEqual("CP001", opportunity.Campaign.CampaignIdentifier);
        }


        [TestMethod]
        public async Task GetAsync_ReturnsFilteredOpportunities()
        {
            // Arrange
            var pagination = new PaginationDTO { Filter = "Desestimada", RecordsNumber = 10, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var campaigns = response.Result!.ToList();
            Assert.AreEqual(1, campaigns.Count);
            Assert.AreEqual("Desestimada", campaigns.First().Status);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllOpportunities_WhenNoFilterIsProvided()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1 };
            // Act
            var response = await _repository.GetAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            var opportunities = response.Result!.ToList();
            Assert.AreEqual(1, opportunities.Count);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1 };
            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_WithFilter_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 1, Page = 1, Filter = "Desestimada" };
            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);
            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }
    }
}
