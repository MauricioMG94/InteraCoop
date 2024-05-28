using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InteraCoop.Backend.Repositories.Implementations
{
    public class OpportunitiesRepository : GenericRepository<Opportunity>, IOpportunitiesRepository
    {
        private readonly DataContext _context;

        public OpportunitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Opportunity>> GetAsync(int id)
        {
            var opportunity = await _context.Opportunities
                .Include(x => x.Campaign!)
                .Include(x => x.Interaction!)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (opportunity == null)
            {
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = false,
                    Message = "Oportunidad no existe"
                };
            }
            return new ActionResponse<Opportunity>
            {
                WasSuccess = true,
                Result = opportunity
            };
        }

        public async override Task<ActionResponse<IEnumerable<Opportunity>>> GetAsync()
        {
            var opportunities = await _context.Opportunities
                .OrderBy(c => c.Status)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Opportunity>>
            {
                WasSuccess = true,
                Result = opportunities
            };
        }

        public override async Task<ActionResponse<IEnumerable<Opportunity>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Opportunities
                .Include(x => x.Campaign)
                .Include(x => x.Interaction)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Status.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Opportunity>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.RecordDate).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Opportunities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Status.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public async Task<ActionResponse<Opportunity>> AddAsync(OpportunityDto opportunityDto)
        {
            try
            {
                var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == opportunityDto.CampaignId);
                var interaction = await _context.Interactions.FirstOrDefaultAsync(i => i.Id == opportunityDto.InteractionId);

                var newOpportunity = new Opportunity
                {
                    Status = opportunityDto.Status,
                    OpportunityObservations = opportunityDto.OpportunityObservations,
                    RecordDate = opportunityDto.RecordDate,
                    EstimatedAcquisitionDate = opportunityDto.EstimatedAcquisitionDate,
                    CampaignId = opportunityDto.CampaignId,
                    Campaign = campaign,
                    InteractionId = opportunityDto.InteractionId,
                    Interaction = interaction
                };

                _context.Add(newOpportunity);
                await _context.SaveChangesAsync();

                var loadedOpportunity = await _context.Opportunities
                    .Include(o => o.Campaign)
                    .Include(o => o.Interaction)
                    .FirstOrDefaultAsync(o => o.Id == newOpportunity.Id);

                return new ActionResponse<Opportunity>
                {
                    WasSuccess = true,
                    Result = loadedOpportunity
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = false,
                    Message = "Ya existe una oportunidad con el mismo Id."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = false,
                    Message = exception.Message,
                };
            }
        }

        public async Task<ActionResponse<Opportunity>> UpdateAsync(OpportunityDto opportunityDto)
        {
            try
            {
                var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == opportunityDto.CampaignId);
                var interaction = await _context.Interactions.FirstOrDefaultAsync(i => i.Id == opportunityDto.InteractionId);

                var opportunity = await _context.Opportunities
                    .Include(x => x.Campaign)
                    .Include(o => o.Interaction)
                    .FirstOrDefaultAsync(x => x.Id == opportunityDto.Id);
                if (opportunity == null)
                {
                    return new ActionResponse<Opportunity>
                    {
                        WasSuccess = false,
                        Message = "Oportunidad no existe"
                    };
                }

                opportunity.Status = opportunityDto.Status;
                opportunity.OpportunityObservations = opportunityDto.OpportunityObservations;
                opportunity.RecordDate = opportunityDto.RecordDate;
                opportunity.EstimatedAcquisitionDate = opportunityDto.EstimatedAcquisitionDate;
                opportunity.CampaignId = opportunityDto.CampaignId;
                opportunity.Campaign = campaign;
                opportunity.InteractionId = opportunityDto.InteractionId;
                opportunity.Interaction = interaction;

                _context.Update(opportunity);
                await _context.SaveChangesAsync();
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = true,
                    Result = opportunity
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = false,
                    Message = "Ya existe una oportunidad con el mismo id."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public override async Task<ActionResponse<Opportunity>> DeleteAsync(int id)
        {
            var opportunity = await _context.Opportunities
                .Include(x => x.Campaign)
                .Include(x => x.Interaction)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (opportunity == null)
            {
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = false,
                    Message = "Oportunidad no encontrada"
                };
            }
            try
            {
                _context.Campaigns.RemoveRange(opportunity.Campaign!);
                _context.Opportunities.Remove(opportunity);
                await _context.SaveChangesAsync();
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = true,
                };

            }
            catch
            {
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar la oportunidad, porque tiene registros relacionados"
                };
            }
        }

    }
}