using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Interfaces
{
    public interface IOpportunitiesUnitOfWork
    {
        Task<ActionResponse<Opportunity>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Opportunity>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<ActionResponse<Opportunity>> AddAsync(OpportunityDto opportunity);
        Task<ActionResponse<Opportunity>> UpdateAsync(OpportunityDto opportunity);
        Task<ActionResponse<Opportunity>> DeleteAsync(int id);

    }
}
