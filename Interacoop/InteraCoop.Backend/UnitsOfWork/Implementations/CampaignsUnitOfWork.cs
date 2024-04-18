using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Implementations
{
    public class CampaignsUnitOfWork : GenericUnitOfWork<Campaign>, ICampaignsUnitOfWork
    {

        private readonly ICampaignsRepository _campaignRepository;

        public CampaignsUnitOfWork(IGenericRepository<Campaign> repository, ICampaignsRepository campaignRepository) : base(repository)
        {
            _campaignRepository = campaignRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Campaign>>> GetAsync(PaginationDTO pagination) => await _campaignRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _campaignRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<Campaign>> GetAsync(int id) => await _campaignRepository.GetAsync(id);

        public async Task<ActionResponse<Campaign>> AddAsync(CampaignDto campaignDto) => await _campaignRepository.AddAsync(campaignDto);

        public async Task<ActionResponse<Campaign>> UpdateAsync(CampaignDto campaignDto) => await _campaignRepository.UpdateAsync(campaignDto);

        public override async Task<ActionResponse<Campaign>> DeleteAsync(int id) => await _campaignRepository.DeleteAsync(id);
    }
}
