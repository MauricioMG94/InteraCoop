using InteraCoop.Backend.Controllers;
using InteraCoop.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCampaignProducts();
            await CheckProductsAsync();
            await CheckOpportunitiesAsync();
        }

        private async Task CheckCampaignProducts()
        {
            if (!_context.Campaigns.Any())
            {
                var products1 = new List<Product>
        {
            new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
        };
                var products2 = new List<Product>
        {
            new Product { ProductType = "Crédito hipotecario", Name = "Crédito Hipotecario Banco X", Quota = 0, Term = "20 años", Value = 5000000, Rate = 5.5 },
            new Product { ProductType = "Crédito hipotecario", Name = "Crédito Hipotecario Banco Y", Quota = 0, Term = "25 años", Value = 6000000, Rate = 6.0 }
        };
                var products3 = new List<Product>
        {
            new Product { ProductType = "Leasing habitacional", Name = "Leasing Habitacional Banco Z", Quota = 0, Term = "15 años", Value = 7000000, Rate = 6.5 },
            new Product { ProductType = "Leasing habitacional", Name = "Leasing Habitacional Banco W", Quota = 0, Term = "10 años", Value = 8000000, Rate = 7.0 }
        };
                var products4 = new List<Product>
        {
            new Product { ProductType = "Cuenta de ahorros", Name = "Cuenta de Ahorros Banco A", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Fondo de inversión", Name = "Fondo de Inversión Banco B", Quota = 0, Term = "5 años", Value = 10000000, Rate = 8.0 }
        };
                var products5 = new List<Product>
        {
            new Product { ProductType = "Seguro de vida", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
            new Product { ProductType = "Seguro vehicular", Name = "Seguro Vehicular DEF", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
        };

                var products6 = new List<Product>
        {
            new Product { ProductType = "Depósito a plazo", Name = "Depósito a Plazo Banco C", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 },
            new Product { ProductType = "Fondo mutuo", Name = "Fondo Mutuo Banco D", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 }
        };
                var product7 = new Product { ProductType = "Cuenta de ahorros", Name = "Cuenta de Ahorros Banco A", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 };
                var product8 = new Product { ProductType = "Seguro de vida", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 };
                var product9 = new Product { ProductType = "Depósito a plazo", Name = "Depósito a Plazo Banco C", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 };
                var product10 = new Product { ProductType = "Fondo mutuo", Name = "Fondo Mutuo Banco D", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 };
                AddCampaigns("CAM001", "Campaña de Tarjetas de Crédito", "Activa", "Promoción", "¡Solicita tu tarjeta de crédito con beneficios exclusivos!", DateTime.Now, DateTime.Now.AddDays(30), products1);
                AddCampaigns("CAM002", "Campaña de Créditos Hipotecarios", "Activa", "Promoción", "¡Adquiere tu casa propia con nuestras opciones de crédito hipotecario!", DateTime.Now.AddDays(35), DateTime.Now.AddDays(65), products2);
                AddCampaigns("CAM003", "Campaña de Leasing Habitacional", "Activa", "Promoción", "¡Haz realidad el sueño de tu hogar con nuestro leasing habitacional!", DateTime.Now.AddDays(70), DateTime.Now.AddDays(100), products3);
                AddCampaigns("CAM007", "Campaña de Productos de Ahorro e Inversión", "Activa", "Promoción", "¡Descubre nuestras opciones para hacer crecer tu dinero!", DateTime.Now.AddDays(105), DateTime.Now.AddDays(135), products4);
                AddCampaigns("CAM008", "Campaña de Seguros", "Activa", "Promoción", "¡Protege lo que más importa con nuestros seguros!", DateTime.Now.AddDays(140), DateTime.Now.AddDays(170), products5);
                AddCampaigns("CAM009", "Campaña de Inversiones", "Activa", "Promoción", "¡Haz crecer tu dinero con nuestras opciones de inversión!", DateTime.Now.AddDays(175), DateTime.Now.AddDays(205), products6);
                AddCampaigns("CAM004", "Campaña de Cuenta de Ahorros", "Activa", "Promoción", "¡Abre una cuenta de ahorros y empieza a ahorrar hoy mismo!", DateTime.Now, DateTime.Now.AddDays(30), new List<Product> { product7 });
                AddCampaigns("CAM005", "Campaña de Seguro de Vida", "Activa", "Promoción", "¡Protege a tus seres queridos con nuestro seguro de vida!", DateTime.Now.AddDays(35), DateTime.Now.AddDays(65), new List<Product> { product8 });
                AddCampaigns("CAM006", "Campaña de Depósito a Plazo", "Activa", "Promoción", "¡Haz crecer tu dinero con nuestro depósito a plazo!", DateTime.Now.AddDays(70), DateTime.Now.AddDays(100), new List<Product> { product9 });
                AddCampaigns("CAM007", "Campaña de Fondo Mutuo", "Activa", "Promoción", "¡Invierte en nuestro fondo mutuo y alcanza tus metas financieras!", DateTime.Now.AddDays(105), DateTime.Now.AddDays(135), new List<Product> { product10 });
                await _context.SaveChangesAsync();
            }
        }

        private void AddCampaigns(string campaignId, string name, string status, string campaignType, string description, DateTime startDate, DateTime endDate, List<Product> products)
        {
            Campaign campaign = new Campaign
            {
                CampaignId = campaignId,
                CampaignName = name,
                Status = status,
                CampaignType = campaignType,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                ProductsList = new List<Product>()
            };
            foreach (var product in products)
            {
                campaign.ProductsList.Add(product);
            }
            _context.Campaigns.Add(campaign);
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Crédito hipotecario", Name = "Crédito Hipotecario Banco X", Quota = 0, Term = "20 años", Value = 5000000, Rate = 5.5 });
                _context.Products.Add(new Product { ProductType = "Leasing habitacional", Name = "Leasing Habitacional Banco Y", Quota = 0, Term = "10 años", Value = 7000000, Rate = 6.0 });
                _context.Products.Add(new Product { ProductType = "Cuenta de ahorros", Name = "Cuenta de Ahorros Banco Z", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Fondo de inversión", Name = "Fondo de Inversión XYZ", Quota = 0, Term = "5 años", Value = 10000000, Rate = 8.0 });
                _context.Products.Add(new Product { ProductType = "Seguro de vida", Name = "Seguro de Vida ABC", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Seguro vehicular", Name = "Seguro Vehicular DEF", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Fondo de pensiones", Name = "Fondo de Pensiones GHI", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Tarjeta de débito", Name = "Tarjeta Débito Banco W", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Crédito de consumo", Name = "Crédito de Consumo Banco V", Quota = 0, Term = "5 años", Value = 2000000, Rate = 9.0 });
                _context.Products.Add(new Product { ProductType = "Cuenta corriente", Name = "Cuenta Corriente Banco U", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Depósito a plazo", Name = "Depósito a Plazo Banco T", Quota = 0, Term = "1 año", Value = 5000000, Rate = 4.5 });
                _context.Products.Add(new Product { ProductType = "Seguro médico", Name = "Seguro Médico Empresa S", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Fondo mutuo", Name = "Fondo Mutuo Empresa R", Quota = 0, Term = "3 años", Value = 15000000, Rate = 7.0 });
                _context.Products.Add(new Product { ProductType = "Tarjeta prepagada", Name = "Tarjeta Prepagada Banco Q", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 });
                _context.Products.Add(new Product { ProductType = "Crédito automotriz", Name = "Crédito Automotriz Banco P", Quota = 0, Term = "5 años", Value = 4000000, Rate = 6.5 });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckOpportunitiesAsync()
        {
            if (!_context.Opportunities.Any())
            {
                var products1 = new List<Product>
                {
                    new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Visa", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 },
                    new Product { ProductType = "Tarjeta de crédito", Name = "Tarjeta Mastercard", Quota = 0, Term = "N/A", Value = 0.0, Rate = 0.0 }
                };

                var campaign1 = new List<Campaign>
                {
                     new Campaign { CampaignId= "CAM001", CampaignName = "Campaña de Tarjetas de Crédito", Status = "Activa", CampaignType = "Promoción", Description = "¡Solicita tu tarjeta de crédito con beneficios exclusivos!", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), ProductsList = products1 }
                };

                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Contactar el 30 de mayo.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 25), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Pendiente", OpportunityObservations = "Visitar en oficina el 20 de mayo.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 26), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Cliente interesado en crédito hipotecario.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 27), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Cerrada", OpportunityObservations = "Cliente aceptó.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 28), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente no deacuerdo con oferta.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 29), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Volver a llamar mañana.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 30), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Confirmar intereses para cliente.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 31), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Cerrada", OpportunityObservations = "Oportunidad vencida.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 01), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Concretada", OpportunityObservations = "Pendiente de firma de pagares.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 02), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Concretada", OpportunityObservations = "Agendar firma de documentos.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 03), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Contactar el 01 de junio", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 04) });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Ver posibiidad de tarjeta de crédito.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 05), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Cerrada", OpportunityObservations = "No interesado.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 06), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Abierta", OpportunityObservations = "Ninguna.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 07), CampaingsList = campaign1 });
                _context.Opportunities.Add(new Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente se cambió de país.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 08), CampaingsList = campaign1 });
                await _context.SaveChangesAsync();
            }
        }
    }
}