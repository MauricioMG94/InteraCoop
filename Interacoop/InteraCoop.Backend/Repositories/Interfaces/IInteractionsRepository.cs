using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface IInteractionsRepository
    {
        Task<ActionResponse<Interaction>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Interaction>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<ActionResponse<Interaction>> DeleteAsync(int id);
        Task<ActionResponse<Interaction>> UpdateAsync(InteractionDto interaction);
        Task<ActionResponse<Interaction>> AddAsync(InteractionDto interactionDto);
    }

}
