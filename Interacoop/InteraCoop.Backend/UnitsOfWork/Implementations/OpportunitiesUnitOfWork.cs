using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Implementations
{
    public class OpportunitiesUnitOfWork : GenericUnitOfWork<Opportunity>, IOpportunitiesUnitOfWork
    {
        private readonly IOpportunitiesRepository _opportunitiesRepository;

        public OpportunitiesUnitOfWork(IGenericRepository<Opportunity> repository, IOpportunitiesRepository opportunitiesRepository) : base(repository)
        {
            _opportunitiesRepository = opportunitiesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Opportunity>>> GetAsync(PaginationDTO pagination) => await _opportunitiesRepository.GetAsync(pagination);
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _opportunitiesRepository.GetTotalPagesAsync(pagination);
        public override async Task<ActionResponse<Opportunity>> GetAsync(int id) => await _opportunitiesRepository.GetAsync(id);
        public async Task<ActionResponse<Opportunity>> AddAsync(OpportunityDto opportunityDto) => await _opportunitiesRepository.AddAsync(opportunityDto);
        public async Task<ActionResponse<Opportunity>> UpdateAsync(OpportunityDto opportunityDto) => await _opportunitiesRepository.UpdateAsync(opportunityDto);
        public override async Task<ActionResponse<Opportunity>> DeleteAsync(int id) => await _opportunitiesRepository.DeleteAsync(id);
    }
}
