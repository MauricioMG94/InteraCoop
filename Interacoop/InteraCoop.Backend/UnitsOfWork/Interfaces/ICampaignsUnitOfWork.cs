using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Interfaces
{
    public interface ICampaignsUnitOfWork
    {
        Task<ActionResponse<Campaign>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Campaign>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Campaign>> AddAsync(CampaignDto campaign);

        Task<ActionResponse<Campaign>> UpdateAsync(CampaignDto campaign);

        Task<ActionResponse<Campaign>> DeleteAsync(int id);

    }
}
