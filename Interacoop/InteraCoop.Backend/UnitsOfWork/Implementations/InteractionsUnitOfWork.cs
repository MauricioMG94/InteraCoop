using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Implementations
{
    public class InteractionsUnitOfWork : GenericUnitOfWork<Interaction>, IInteractionsUnitOfWork
    {
        private readonly IInteractionsRepository _interactionsRepository;

        public InteractionsUnitOfWork(IGenericRepository<Interaction> repository, IInteractionsRepository interactionsRepository) : base(repository)
        {
            _interactionsRepository = interactionsRepository;
        }
        public override async Task<ActionResponse<IEnumerable<Interaction>>> GetAsync(PaginationDTO pagination) => await _interactionsRepository.GetAsync(pagination);
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _interactionsRepository.GetTotalPagesAsync(pagination);
        public override async Task<ActionResponse<Interaction>> GetAsync(int id) => await _interactionsRepository.GetAsync(id);
        public async Task<ActionResponse<Interaction>> AddAsync(InteractionDto interactionDto) => await _interactionsRepository.AddAsync(interactionDto);
        public async Task<ActionResponse<Interaction>> UpdateAsync(InteractionDto interactionDto) => await _interactionsRepository.UpdateAsync(interactionDto);
        public override async Task<ActionResponse<Interaction>> DeleteAsync(int id) => await _interactionsRepository.DeleteAsync(id);
    }
}
