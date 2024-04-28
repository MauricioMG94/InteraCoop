using InteraCoop.Backend.Data;
using InteraCoop.Backend.Helpers;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                .Include(x => x.CampaingsList!)
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

        public override async Task<ActionResponse<IEnumerable<Opportunity>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Opportunities
                .Include(x => x.CampaingsList)
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
                var newOpportunity = new Opportunity
                {
                    Status = opportunityDto.Status,
                    OpportunityObservations = opportunityDto.OpportunityObservations,
                    RecordDate = opportunityDto.RecordDate,
                    EstimatedAcquisitionDate = opportunityDto.EstimatedAcquisitionDate,
                    CampaingsList = new List<Campaign>(),
                };
                foreach (var campaingId in opportunityDto.CampaingsIds!)
                {
                    var campaing = await _context.Campaigns.FirstOrDefaultAsync(x => x.Id == campaingId);
                    if (campaing != null)
                    {
                        newOpportunity.CampaingsList.Add(campaing);
                    }
                }
                _context.Add(newOpportunity);
                await _context.SaveChangesAsync();
                return new ActionResponse<Opportunity>
                {
                    WasSuccess = true,
                    Result = newOpportunity
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
                var opportunity = await _context.Opportunities
                    .Include(x => x.CampaingsList)
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
                opportunity.CampaingsList = new List<Campaign>();
                if (opportunityDto.CampaingsIds != null)
                {
                    foreach (var campaingId in opportunityDto.CampaingsIds)
                    {
                        var campaign = await _context.Campaigns.FindAsync(campaingId);
                        if (campaign != null)
                        {
                            opportunity.CampaingsList.Add(campaign);
                        }
                    }
                }
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
                .Include(x => x.CampaingsList)
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
                _context.Campaigns.RemoveRange(opportunity.CampaingsList!);
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