using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface IOpportunitiesRepository
    {
        Task<ActionResponse<Opportunity>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Opportunity>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<IEnumerable<Opportunity>>> GetAsync();
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<ActionResponse<Opportunity>> DeleteAsync(int id);
        Task<ActionResponse<Opportunity>> UpdateAsync(OpportunityDto opportunity);
        Task<ActionResponse<Opportunity>> AddAsync(OpportunityDto opportunityDTO);
    }
}
