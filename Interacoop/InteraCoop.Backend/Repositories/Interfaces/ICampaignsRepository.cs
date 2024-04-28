using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface ICampaignsRepository
    {
        Task<ActionResponse<Campaign>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Campaign>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<IEnumerable<Campaign>>> GetAsync();

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Campaign>> DeleteAsync(int id);

        Task<ActionResponse<Campaign>> UpdateAsync(CampaignDto product);

        Task<ActionResponse<Campaign>> AddAsync(CampaignDto productDTO);

    }
}
