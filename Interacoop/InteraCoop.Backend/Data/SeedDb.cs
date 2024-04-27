using InteraCoop.Backend.Data;
using InteraCoop.Shared.Entities;

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
            await CheckOpportunitiesAsync();
        }

        private async Task CheckOpportunitiesAsync()
        {
            if (!_context.Opportunities.Any())
            {
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Abierta", OpportunityObservations = "Contactar el 30 de mayo.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 25) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Pendiente", OpportunityObservations = "Visitar en oficina el 20 de mayo.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 26) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Abierta", OpportunityObservations = "Cliente interesado en crédito hipotecario.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 27)});
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Cerrada", OpportunityObservations = "Cliente aceptó.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 28) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente no deacuerdo con oferta.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 29) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Abierta", OpportunityObservations = "Volver a llamar mañana.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 30) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Abierta", OpportunityObservations = "Confirmar intereses para cliente.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 05, 31) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Cerrada", OpportunityObservations = "Oportunidad vencida.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 01) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Concretada", OpportunityObservations = "Pendiente de firma de pagares.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 02) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Concretada", OpportunityObservations = "Agendar firma de documentos.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 03) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Abierta", OpportunityObservations = "Contactar el 01 de junio", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 04) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Abierta", OpportunityObservations = "Ver posibiidad de tarjeta de crédito.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 05) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Cerrada", OpportunityObservations = "No interesado.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 06) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Abierta", OpportunityObservations = "Ninguna.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 07) });
                _context.Opportunities.Add(new Shared.Entities.Opportunity { Status = "Desestimada", OpportunityObservations = "Cliente se cambió de país.", RecordDate = DateTime.Now, EstimatedAcquisitionDate = new DateTime(2024, 06, 08) });
                await _context.SaveChangesAsync();
            }
        }


    }
}
